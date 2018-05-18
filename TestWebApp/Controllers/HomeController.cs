using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestWebApp.Data.Interfaces;
using TestWebApp.Models;
using TestWebApp.ViewModel;

namespace TestWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGameRepository _gameRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAuthorRepository _authorRepository;

        public HomeController(IGameRepository gameRepository,
                              ICustomerRepository customerRepository,
                              IAuthorRepository authorRepository)   
        {
            _gameRepository = gameRepository;
            _customerRepository = customerRepository;
            _authorRepository = authorRepository;
        }

        public IActionResult Index()
        {
            var homeVM = new HomeViewModel()
            {
                AuthorCount = _authorRepository.Count(x => true),
                CustomerCount = _customerRepository.Count(x => true),
                GameCount = _gameRepository.Count(x => true),
                LendCount = _gameRepository.Count(x => x.Customer != null)
            };


            return View(homeVM);
        }
    }
}
