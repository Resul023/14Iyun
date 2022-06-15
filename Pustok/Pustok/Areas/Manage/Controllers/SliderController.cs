using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Pustok.Areas.Manage.ViewModel;
using Pustok.DAL;
using Pustok.Helper;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class SliderController : Controller
    {
        

        
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderController(AppDbContext context,IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }
        public IActionResult Index()
        {
            //SliderViewModel sliderVW = new SliderViewModel
            //{
            //    Sliders = _context.Slider.ToList(),

            //};

            var model = _context.Slider.ToList();
            return View(model);
        }
        public IActionResult Create() 
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Slider slider) 
        {
           
            if (slider.ImageFile != null)
            {
                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File format must be only jpeg or png");
                }

                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size must be less 2MB");
                }
                
            }
            else
            {
                ModelState.AddModelError("ImageFile","Image is required!!!");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            slider.SliderImage = FileManager.Save(_env.WebRootPath,"upload/slider",slider.ImageFile);
            
            _context.Slider.Add(slider);
            _context.SaveChanges();
            return RedirectToAction("index");


        }
        public IActionResult Delete(int id) 
        {
           Slider slider = _context.Slider.FirstOrDefault(x => x.Id == id);
            if (slider == null)
            {
                return NotFound();
            }

            FileManager.Delete(_env.WebRootPath, "upload/slider", slider.SliderImage);

            _context.Slider.Remove(slider);
            _context.SaveChanges();
            return RedirectToAction("index");
        }
        public IActionResult Edit(int id) 
        {
            Slider slider = _context.Slider.FirstOrDefault(x => x.Id == id);
            if (slider == null)
            {
                return RedirectToAction("error", "home");
            }
            return View(slider);
        }
        [HttpPost]
        public IActionResult Edit(Slider slider) 
        {
            Slider isExists = _context.Slider.FirstOrDefault(x => x.Id == slider.Id);
            if (slider == null)
            {
                return RedirectToAction("error", "home");
            }

            if (slider.ImageFile != null)
            {

                if (slider.ImageFile.ContentType != "image/png" && slider.ImageFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("ImageFile", "File format must be only jpeg or png");
                }

                if (slider.ImageFile.Length > 2097152)
                {
                    ModelState.AddModelError("ImageFile", "File size must be less 2MB");
                }
                
                if (!ModelState.IsValid)
                    return View();


                string newFileName = FileManager.Save(_env.WebRootPath, "upload/slider", slider.ImageFile);
                FileManager.Delete(_env.WebRootPath, "upload/slider", isExists.SliderImage);
                isExists.SliderImage = newFileName;
            }

            isExists.Title1 = slider.Title1;
            isExists.Title2 = slider.Title2;
            isExists.Order = slider.Order;
            
            _context.SaveChanges();
            return RedirectToAction("index");

        }
    }
}
