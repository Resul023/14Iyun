using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok.Areas.Manage.ViewModel;
using Pustok.DAL;
using Pustok.Helper;
using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Areas.Manage.Controllers
{
    [Area("manage")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(AppDbContext context,IWebHostEnvironment env)
        {
            this._context = context;
            this._env = env;
        }
        public IActionResult Index()
        {
            BookViewModel bookVW = new BookViewModel
            {
                Books = _context.Book.Include(x => x.Author).Include(x=>x.Genre).ToList(),

            };
            return View(bookVW);
        }
        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View();

        }
        [HttpPost]
        public IActionResult Create(Books book)
        {

            if (!_context.Authors.Any(x=>x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "This author is not exists");
            }
            if (!_context.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("AuthorId", "This author is not exists");
            }

            CheckCreatePosterFile(book);
            CheckCreateHoverFile(book);
            CheckImageFile(book);
            CheckTags(book);   

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                ViewBag.Tags = _context.Tags.ToList();
                return View();
            }
            BookImage bookPosterImg = new BookImage
            {
                Name = FileManager.Save(_env.WebRootPath, "upload/book", book.PosterFile),
                PosterStatus = true
            };
           
            BookImage bookHoverImg = new BookImage
            {
                Name = FileManager.Save(_env.WebRootPath, "upload/book", book.HoverFile),
                PosterStatus = false,
            };

            book.BookImages.Add(bookPosterImg);
            book.BookImages.Add(bookHoverImg);
            AddImageFiles(book,book.ImageFiles);

            if (book.TagIds!=null)
            {
                foreach (var tagId in book.TagIds)
                {
                    BookTag bkTag = new BookTag
                    {
                        TagId = tagId,
                    };
                    book.BookTags.Add(bkTag);
                }
            }
            _context.Book.Add(book);
            _context.SaveChanges();
            return RedirectToAction("index");
        }

        public IActionResult Edit(int Id) 
        {
            Books isExists = _context.Book.Include(x=>x.BookImages).FirstOrDefault(x => x.Id == Id);

            if (isExists == null)
            {
                return RedirectToAction("error", "home");
            }
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();
            return View(isExists);
        }
        [HttpPost]
        public IActionResult Edit(Books book)
        {
            Books isExists = _context.Book.Include(x => x.BookImages).FirstOrDefault(x => x.Id == book.Id);
            if (isExists == null)
            {
                return RedirectToAction("error", "home");
            }
            if (book.AuthorId != isExists.AuthorId && !_context.Authors.Any(x=>x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "This author is not exists");
            }
            if (book.GenreId != isExists.GenreId && !_context.Authors.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "This genre is not exists");
            }
            if (book.PosterFile!=null)
            {
                CheckPosterFile(book);
            }
            if (book.HoverFile!=null)
            {
                CheckHoverFile(book);
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                ViewBag.Tags = _context.Tags.ToList();
                return View();
            }
            List<string> deletedFiles = new List<string>();
            if (book.PosterFile != null)
            {
                BookImage poster= isExists.BookImages.FirstOrDefault(x=>x.PosterStatus == true);
                deletedFiles.Add(poster.Name);
                poster.Name = FileManager.Save(_env.WebRootPath, "upload/book", book.PosterFile);
            }

            if (book.HoverFile != null)
            {
                BookImage hover = isExists.BookImages.FirstOrDefault(x => x.PosterStatus == false);
                deletedFiles.Add(hover.Name);
                hover.Name = FileManager.Save(_env.WebRootPath, "upload/book",book.HoverFile);
            }

            AddImageFiles(isExists, book.ImageFiles);

            isExists.AuthorId = book.AuthorId;
            isExists.CostPrice = book.CostPrice;
            isExists.Desc = book.Desc;
            isExists.DiscountPercent = book.DiscountPercent;
            isExists.GenreId = book.GenreId;
            isExists.IsAvailable = book.IsAvailable;
            isExists.Name = book.Name;
            isExists.PageSize = book.PageSize;
            isExists.Rate = book.Rate;
            isExists.SalePrice = book.SalePrice;
            isExists.SubDesc = book.SubDesc;
            

            _context.SaveChanges();
            FileManager.DeleteAll(_env.WebRootPath, "upload/book", deletedFiles);
            return RedirectToAction("index");


        }
        private void CheckCreatePosterFile(Books book) 
        {
            if (book.PosterFile == null)
            {
                ModelState.AddModelError("PosterFile", "Post image is required");
            }
            else
            {

                CheckPosterFile(book);
            }

        }
        private void CheckPosterFile(Books book) 
        {

            if (book.PosterFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFiles", "File size must be less than 2MB");
            }
            if (book.PosterFile.ContentType != "image/png" && book.PosterFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFiles", "File format must be image/png or image/jpeg");
            }
        }
        private void CheckCreateHoverFile(Books book) 
        {

            if (book.HoverFile == null)
            {
                ModelState.AddModelError("HoverFile", "Hover image is required");
            }
            else
            {
                CheckHoverFile(book);
            }
        }
        private void CheckHoverFile(Books book) 
        {
            if (book.HoverFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFiles", "File size must be less than 2MB");
            }
            if (book.HoverFile.ContentType != "image/png" && book.PosterFile.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("ImageFiles", "File format must be image/png or image/jpeg");
            }
        }
        private void CheckImageFile(Books book) 
        {
            if (book.ImageFiles!= null)
            {
                foreach (var file in book.ImageFiles)
                {
                    if (file.Length > 2097152)
                    {
                        ModelState.AddModelError("ImageFiles", "File size must be less than 2MB");
                    }
                    if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFiles", "File format must be image/png or image/jpeg");
                    }

                }
            }
        }
        private void CheckTags(Books book) 
        {
            if (book.TagIds!= null)
            {
                foreach (var id in book.TagIds)
                {
                    if (!_context.Tags.Any(x=>x.Id == id))
                    {
                        ModelState.AddModelError("TagIds", "This tag is not exists");
                        return;
                    }
                }
            }
        
        }
        private void AddImageFiles(Books book,List<IFormFile> images) 
        {
            if (images != null)
            {
                foreach (var file in images)
                {
                    BookImage bookImg = new BookImage
                    {
                        Name = FileManager.Save(_env.WebRootPath, "upload/book", file),
                        PosterStatus = null
                    };
                    book.BookImages.Add(bookImg);
                }
            }
        
        }
        
    }
}
