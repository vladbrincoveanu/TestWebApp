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
    [Authorize]
    public class GameController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IFavoriteRepository _favoriteRepository;

        public GameController(IGameRepository gameRepository, 
                              IAuthorRepository authorRepository,
                              IFavoriteRepository favoriteRepository)
        {
            _gameRepository = gameRepository;
            _authorRepository = authorRepository;
            _favoriteRepository = favoriteRepository;
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var gameVM = new GameViewModel()
            {
                Authors = _authorRepository.GetAll()
            };

            return View(gameVM);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(GameViewModel gameViewModel)
        {
            if (!ModelState.IsValid)
            {
                gameViewModel.Authors = _authorRepository.GetAll();
                return View(gameViewModel);
            }

            var game = gameViewModel.Game;

            var game2 = _favoriteRepository.GetAllChangesForGame(game.GameId);

            if(game2.Count() == 0)
            {
                _favoriteRepository.Create(new Favorites
                {
                    NewGame = new NamePrice
                    {
                        Name = gameViewModel.Game.Name,
                        Price = gameViewModel.Game.Price,
                        GameID = gameViewModel.Game.GameId
                    },

                    OldGame = new NamePrice { Name = "UNregistered", Price = 0 },
                    Date = DateTime.Now
                });
            }
            else
            {
                var game1 = game2.First();

                var fav = new Favorites
                {
                    OldGame = new NamePrice
                    {
                        Name = game1.NewGame.Name,
                        Price = game1.NewGame.Price,
                        GameID = game1.NewGame.GameID
                    },
                    NewGame = new NamePrice
                    {
                        Name = gameViewModel.Game.Name,
                        Price = gameViewModel.Game.Price,
                        GameID = gameViewModel.Game.GameId
                    },
                    Date = DateTime.Now
                };

                _favoriteRepository.Create(fav);
            }

            _gameRepository.Update(gameViewModel.Game);

            return RedirectToAction("List");
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var gameVM = new GameViewModel()
            {
                Game = _gameRepository.getById(id),
                Authors = _authorRepository.GetAll()
            };

            return View(gameVM);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(GameViewModel gameViewModel)
        {
            if (!ModelState.IsValid)
            {
                gameViewModel.Authors = _authorRepository.GetAll();
                return View(gameViewModel);
            }

            _gameRepository.Create(gameViewModel.Game);
            _favoriteRepository.Create(new Favorites { NewGame = new NamePrice {Name = gameViewModel.Game.Name,
                                                                                Price =gameViewModel.Game.Price,
                                                                                GameID = gameViewModel.Game.GameId},

                                                      OldGame = new NamePrice { Name = "UNregistered",Price = 0},
                                                      Date = DateTime.Now});
            var a = _favoriteRepository.GetAllChangesForGame(gameViewModel.Game.GameId);
            return RedirectToAction("List");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var game = _gameRepository.getById(id);

            _gameRepository.Delete(game);

            return RedirectToAction("List");
        }


       

    }
}
