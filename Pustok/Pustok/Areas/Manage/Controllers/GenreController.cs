using Microsoft.AspNetCore.Mvc;
using Pustok.Areas.Manage.ViewModel;
using Pustok.DAL;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class GenreController : Controller
    {
        private readonly AppDbContext _context;

        public GenreController(AppDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            GenreViewModel genreVW = new GenreViewModel
            {
                Genres = _context.Genres.ToList(),
            
            };
            return View(genreVW);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Genres genre) 
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Add(genre);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int Id) 
        {
            Genres isExists = _context.Genres.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return RedirectToAction ("error", "home");
            }

            return View(isExists);
        }

        [HttpPost]
        public IActionResult Edit(Genres genre) 
        {
            Genres isExists = _context.Genres.FirstOrDefault(x => x.Id == genre.Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "home");
            }
            isExists.Name = genre.Name;
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Delete(int Id) 
        {
            Genres isExists = _context.Genres.FirstOrDefault(x => x.Id == Id);
            if (isExists == null)
            {
                return NotFound();
            }
            _context.Genres.Remove(isExists);
            _context.SaveChanges(); 
            return RedirectToAction ("index");
        }

    }
}
