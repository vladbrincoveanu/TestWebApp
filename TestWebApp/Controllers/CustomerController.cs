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
    public class CustomerController :Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IGameRepository _gameRepository;

        public CustomerController(ICustomerRepository customerRepository,IGameRepository gameRepository)
        {
            _customerRepository = customerRepository;
            _gameRepository = gameRepository;
        }


        [Route("Customer")]
        public IActionResult List()
        {
            var customerVM = new List<CustomerViewModel>();

            var customers = _customerRepository.GetAll();

            if(customers.Count() == 0)
            {
                return View("Empty");
            }

            var games = _gameRepository.GetAll();

            foreach(var customer in customers)
            {
                customerVM.Add(new CustomerViewModel
                {
                    Customer = customer,
                    GameCount = _gameRepository.Count(x => x.CustomerID == customer.CustomerId)
                });
            }

            return View(customerVM);
        }

        public IActionResult Delete(int id)
        {
            var customer = _customerRepository.getById(id);

            _customerRepository.Delete(customer);

            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            _customerRepository.Create(customer);

            return RedirectToAction("List");
        }

        public IActionResult Update(int id)
        {
            var customer = _customerRepository.getById(id);

            return View(customer);
        }

        [HttpPost]
        public IActionResult Update(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            _customerRepository.Update(customer);

            return RedirectToAction("List");
        }
    }
}
