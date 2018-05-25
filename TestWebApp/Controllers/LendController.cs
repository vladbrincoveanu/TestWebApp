using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.BL;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;
using TestWebApp.ViewModel;

namespace TestWebApp.Controllers
{
    [Authorize]
    public class LendController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IPublisher _lendDataProvider;
        private readonly IFavoriteRepository _favoriteRepository;
        private GameHistory _gameHistory;
      

        public LendController(IGameRepository gameRepository, 
                              ICustomerRepository customerRepository,
                              IPublisher publisher,
                              IFavoriteRepository favoriteRepository,
                              GameHistory gameHistory)
        {
            _gameRepository = gameRepository;
            _customerRepository = customerRepository;
            _lendDataProvider = publisher;
            _favoriteRepository = favoriteRepository;
            _gameHistory = gameHistory;
        }

        public Customer FindCustomerByName(List<Customer> list)
        {
            foreach (Customer c in list)
            {
                if (c.Name.Equals(HttpContext.User.Identity.Name))
                {
                    return c;
                }
            }
            return null;
        }

        [Route("Lend")]
        public IActionResult List()
        {
            var allGames = _gameRepository.FindWithAuthor(x=>true);

            TempData["fav"] = _gameHistory.List;

            if (allGames.Count() == 0)
            {
                return View("Empty");
            }
            else
            {
                return View(allGames);
            }
        }

        public IActionResult LendGame(int gameId)
        {

            Customer c = FindCustomerByName(_customerRepository.GetAll().ToList());

            var lendVM = new LendViewModel()
            {
                Game = _gameRepository.getById(gameId),
                Customers = c,
                Date = DateTime.Now
            };

            return View(lendVM);
        }

        [HttpPost]
        public IActionResult LendGame(LendViewModel lendViewModel)
        {
            var game = _gameRepository.getById(lendViewModel.Game.GameId);
            Customer c = FindCustomerByName(_customerRepository.GetAll().ToList());
            var sum = c.Money - game.Price;

            if(sum <= 0)
            {
                return View("Error");
            }
            else
            {
                c.Money = sum;

                game.Customer = c;

                _gameRepository.Update(game);

                _customerRepository.Update(c);

                var model = new LendViewModel
                {
                    Customers = c,
                    Game = game,
                    Date = DateTime.Now
                };

                _lendDataProvider.AddData(model);

                return RedirectToAction("List");
            }

          
        }

      
        public IActionResult AddToFavorite(int id)
        {
            var game = _gameRepository.getById(id);
            if (!ModelState.IsValid)
            {
                return RedirectToAction("List");
            }

            _gameHistory.List = _favoriteRepository.GetAllChangesForGame(id).ToList();

           

            return RedirectToAction("List");
        }
    }
}
