using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;

namespace SteamRipOff.Controllers
{
    [Authorize]
    public class ReturnController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReturnController(IGameRepository gameRepository,ICustomerRepository customerRepository)
        {
            _gameRepository = gameRepository;
            _customerRepository = customerRepository;
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

        [Route("Return")]
        public IActionResult List()
        {

            Customer c = FindCustomerByName(_customerRepository.GetAll().ToList());

            var borrowedGames = _gameRepository.FindWithAuthorAndBorrower(x => x.CustomerID == c.CustomerId );

            if(borrowedGames == null || borrowedGames.ToList().Count() == 0)
            {
                return View("Empty");
            }

            return View(borrowedGames);
        }

        public IActionResult ReturnGame(int gameId)
        {
            var game = _gameRepository.getById(gameId);

            game.Customer = null;
            game.CustomerID = 0;

            _gameRepository.Update(game);

            return RedirectToAction("List");
        }
    }
}