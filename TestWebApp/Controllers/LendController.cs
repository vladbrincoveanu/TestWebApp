using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Interfaces;
using TestWebApp.ViewModel;

namespace TestWebApp.Controllers
{
    public class LendController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICustomerRepository _customerRepository;

        public LendController(IGameRepository gameRepository, ICustomerRepository customerRepository)
        {
            _gameRepository = gameRepository;
            _customerRepository = customerRepository;
        }

        [Route("Lend")]
        public IActionResult List()
        {
            var allGames = _gameRepository.FindWithAuthor(x=>true);

            if(allGames.Count() == 0)
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
            var lendVM = new LendViewModel()
            {
                Game = _gameRepository.getById(gameId),
                Customers = _customerRepository.GetAll()
            };

            return View(lendVM);
        }

        [HttpPost]
        public IActionResult LendGame(LendViewModel lendViewModel)
        {
            var game = _gameRepository.getById(lendViewModel.Game.GameId);
            var customer = _customerRepository.getById(lendViewModel.Game.CustomerID);
            var sum = customer.Money - game.Price;

            if(sum <= 0)
            {
                return View("Error");
            }
            else
            {
                customer.Money = sum;

                game.Customer = customer;

                _gameRepository.Update(game);

                _customerRepository.Update(customer);

                return RedirectToAction("List");
            }

          
        }
    }
}
