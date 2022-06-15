using Microsoft.AspNetCore.Mvc;
using Pustok.Areas.Manage.ViewModel;
using Pustok.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pustok.Models;
using Microsoft.EntityFrameworkCore;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class AuthorController : Controller
    {
        private readonly AppDbContext _context;

        public AuthorController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            AuthorViewModel authorVW = new AuthorViewModel
            {
                Authors = _context.Authors.ToList(),
            };
            return View(authorVW);
        }
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Authors author)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            _context.Authors.Add(author);
            _context.SaveChanges(); 

            return RedirectToAction("index");
        }

        public IActionResult Edit(int id) 
        {
            
            Authors authors = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (authors == null)
            {
                return RedirectToAction("error","home");
            }
            return View(authors);
        }

        [HttpPost]
        public IActionResult Edit(Authors authors)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Authors isExist = _context.Authors.FirstOrDefault(x => x.Id == authors.Id);
            if (isExist == null)
            {
                return RedirectToAction("error", "home");
            }
            

            isExist.FullName = authors.FullName;
            isExist.Age = authors.Age;
            _context.SaveChanges();
            

            return RedirectToAction("index");

        }

        public IActionResult Delete(int id) 
        {

            Authors authors = _context.Authors.Include(x=>x.books).FirstOrDefault(x => x.Id == id);
            if (authors == null)
            {
                return RedirectToAction("error", "home");
            }
            return View(authors);
        }

        [HttpPost]
        public IActionResult Delete(Authors author) 
        {
            Authors authors = _context.Authors.Include(x => x.books).FirstOrDefault(x => x.Id == author.Id);
            if (authors == null)
            {
                return RedirectToAction("error", "home");
            }
            _context.Remove(authors);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
