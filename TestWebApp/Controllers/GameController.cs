using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IAuthorRepository _authorRepository;

        public GameController(IGameRepository gameRepository, IAuthorRepository authorRepository)
        {
            _gameRepository = gameRepository;
            _authorRepository = authorRepository;
        }

        [Route("Game")]
        public IActionResult List(int? authorId, int? borrowedId)
        {
            if (authorId == null && borrowedId == null)
            {
                var games = _gameRepository.GetAllWithAuthor();

                return checkGames(games);

            }
            else if (authorId != null)
            {
                var author = _authorRepository.GetWithGames((int)authorId);

                if (author.Game.Count() == 0)
                {
                    return View("EmptyAuthor", author);
                }
                else
                {
                    return View(author.Game);
                }

            }
            else if (borrowedId != null)
            {
                var games = _gameRepository.FindWithAuthorAndBorrower(game => game.CustomerID == borrowedId);

                return checkGames(games);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public IActionResult checkGames(IEnumerable<Game> games)
        {
            if (games.Count() == 0)
            {
                return View("Empty");
            }
            else
            {
                return View(games);
            }
        }

        public IActionResult Create()
        {
            var gameVM = new GameViewModel()
            {
                Authors = _authorRepository.GetAll()
            };

            return View(gameVM);
        }

        [HttpPost]
        public IActionResult Update(GameViewModel gameViewModel)
        {
            if (!ModelState.IsValid)
            {
                gameViewModel.Authors = _authorRepository.GetAll();
                return View(gameViewModel);
            }

            _gameRepository.Update(gameViewModel.Game);

            return RedirectToAction("List");
        }

        public IActionResult Update(int id)
        {
            var gameVM = new GameViewModel()
            {
                Game = _gameRepository.getById(id),
                Authors = _authorRepository.GetAll()
            };

            return View(gameVM);
        }

        [HttpPost]
        public IActionResult Create(GameViewModel gameViewModel)
        {
            if (!ModelState.IsValid)
            {
                gameViewModel.Authors = _authorRepository.GetAll();
                return View(gameViewModel);
            }

            _gameRepository.Create(gameViewModel.Game);

            return RedirectToAction("List");
        }

        public IActionResult Delete(int id)
        {
            var game = _gameRepository.getById(id);

            _gameRepository.Delete(game);

            return RedirectToAction("List");
        }

    }
}
