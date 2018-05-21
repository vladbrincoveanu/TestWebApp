using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;
using TestWebApp.ViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebApp.Controllers
{
    public class AccountController: Controller
    {
       
        public static Customer instaceOfCustomer;
        private readonly ICustomerRepository _customerRepository;

        public AccountController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
  
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View("Create");
        }

        [HttpPost]
        public IActionResult Create([Bind] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerRepository.Create(customer);
            }
            return View("LogIn");
        }


        [HttpGet]
        public IActionResult LogIn()
        {

            return View("LogIn");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn([Bind]Customer customer)
        {

            var customers = _customerRepository.GetAll();
            foreach (Customer c in customers)
            {
                if (c.Name.Equals(customer.Name) && !c.LoggedIn && c.Password.Equals(customer.Password))
                {
                    instaceOfCustomer = c;
                    c.LoggedIn = true;
                    _customerRepository.Update(c);
                    var customerss = _customerRepository.GetAll();
                    String role;
                    if (c.IsAdmin)
                    {
                        role = "Admin";
                    }
                    else
                    {
                        role = "customer";
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, c.Name),
                        new Claim("Name", c.Name),
                        new Claim(ClaimTypes.Role, role),
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties { };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["UserLoginFailed"] = "Login Failed.Please enter correct credentials";
        

            return View("LogIn");
        }

      
    }
}
