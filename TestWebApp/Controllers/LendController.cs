using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;
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

        public Customer findCustomerByName(List<Customer> list)
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

            Customer c = findCustomerByName(_customerRepository.GetAll().ToList());

            var lendVM = new LendViewModel()
            {
                Game = _gameRepository.getById(gameId),
                Customers = c
            };

            return View(lendVM);
        }

        [HttpPost]
        public IActionResult LendGame(LendViewModel lendViewModel)
        {
            var game = _gameRepository.getById(lendViewModel.Game.GameId);
            Customer c = findCustomerByName(_customerRepository.GetAll().ToList());
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

                return RedirectToAction("List");
            }

          
        }
    }
}
