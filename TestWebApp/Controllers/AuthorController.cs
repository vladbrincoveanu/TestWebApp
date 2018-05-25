using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.Data.Interfaces;
using TestWebApp.Data.Model;

namespace TestWebApp.Controllers
{

    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        { 
            _authorRepository = authorRepository;
          
        }

        [Route("Author")]
        public IActionResult List()
        {
            var authors = _authorRepository.GetAllWithGames();

            if (authors.Count() == 0) return View("Empty");

            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Author author)
        {
            if(!ModelState.IsValid)
            {
                return View(author);
            }

            _authorRepository.Create(author);

            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var author = _authorRepository.getById(id);

            _authorRepository.Delete(author);

            return RedirectToAction("List");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            var author = _authorRepository.getById(id);

            if (author == null) return NotFound();

            return View(author);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Update(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            _authorRepository.Update(author);

            return RedirectToAction("List");
        }
    }
}
