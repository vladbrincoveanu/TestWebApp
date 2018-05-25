using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestWebApp.BL;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;
using TestWebApp.ViewModel;

namespace TestWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private List<LendViewModel> model = new List<LendViewModel>();
        private readonly IGameRepository _gameRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAuthorRepository _authorRepository;
        private ISubscriber _subscriber;
        private IDocument _documentGenerator;

        public HomeController(IGameRepository gameRepository,
                              ICustomerRepository customerRepository,
                              IAuthorRepository authorRepository,
                              ISubscriber subscriber,
                              IDocument document)
        {
            _gameRepository = gameRepository;
            _customerRepository = customerRepository;
            _authorRepository = authorRepository;
            _subscriber = subscriber;
            _documentGenerator = document;

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

        [HttpGet]
        public IActionResult Index()
        {

            //var loggedInUser = HttpContext.User;
            //var loggedInUserName = loggedInUser.Identity.Name; // This is our username we set earlier in the claims. 
            //var loggedInUserName2 = loggedInUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value; //Another way to get the name or any other cl

            Customer c = FindCustomerByName(_customerRepository.GetAll().ToList());


            var homeVM = new HomeViewModel()
            {
                AuthorCount = _authorRepository.Count(x => true),
                CustomerCount = _customerRepository.Count(x => true),
                GameCount = _gameRepository.Count(x => true),
                LendCount = _gameRepository.Count(x => x.Customer != null),
                Customer = c,
            };


            return View("Index", homeVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            Customer c = FindCustomerByName(_customerRepository.GetAll().ToList());
            c.LoggedIn = false;
            _customerRepository.Update(c);
            await HttpContext.SignOutAsync();
            return RedirectToAction("LogIn", "Account");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ViewLends()
        {
            var model = _subscriber.GetList();
            if (model.Count() != 0)
            {
                return View(model);
            }
            else
            {
                return View("Error");
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GenerateReport()
        {

            //_documentGenerator.CreateDocument("pdf");
            _documentGenerator.CreateDocument("txt").Create(_subscriber.GetList());

            return RedirectToAction("Index");
        }
    }
}
