using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebApp.Data;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;

namespace SteamRipOff.Controllers
{
    
    public class InfoController : Controller
    {
        private ICustomerRepository _customerRepository;

        public InfoController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [Route("List")]
        public IActionResult List()
        {
            var customers = _customerRepository.GetAll();

            return View("index");
        }
       
    }
}