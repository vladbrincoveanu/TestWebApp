using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;
using TestWebApp.Models;
using TestWebApp.ViewModel;

namespace TestWebApp.Controllers
{
    [Authorize]
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

        public Customer findCustomerByName(List<Customer> list)
        {
            foreach(Customer c in list)
            {
                if (c.Name.Equals(HttpContext.User.Identity.Name))
                {
                    return c;
                }
            }
            return null;
        }

      
        public IActionResult Index()
        {

            var loggedInUser = HttpContext.User;
            var loggedInUserName = loggedInUser.Identity.Name; // This is our username we set earlier in the claims. 
            var loggedInUserName2 = loggedInUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value; //Another way to get the name or any other cl

            Customer c = findCustomerByName(_customerRepository.GetAll().ToList());

            var homeVM = new HomeViewModel()
            {
                AuthorCount = _authorRepository.Count(x => true),
                CustomerCount = _customerRepository.Count(x => true),
                GameCount = _gameRepository.Count(x => true),
                LendCount = _gameRepository.Count(x => x.Customer != null),
                Customer = c
            };


            return View("Index",homeVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            Customer c = findCustomerByName(_customerRepository.GetAll().ToList());
            c.LoggedIn = false;
            await HttpContext.SignOutAsync();
            return RedirectToAction("LogIn", "Account");
        }

    }
}
