using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebApp.Data.Interfaces;

namespace SteamRipOff.Controllers
{
    public class ReturnController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICustomerRepository _customerRepository;

        public ReturnController(IGameRepository gameRepository,ICustomerRepository customerRepository)
        {
            _gameRepository = gameRepository;
            _customerRepository = customerRepository;
        }

        [Route("Return")]
        public IActionResult List()
        {
            var borrowedGames = _gameRepository.FindWithAuthorAndBorrower(x => x.CustomerID != 0);

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