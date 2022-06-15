using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Books
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength: 60)]
        public string Name { get; set; }
        public string Desc { get; set; }
        public int GenreId { get; set; }
        public Genres Genre { get; set; }
        public int AuthorId { get; set; }
        public Authors Author { get; set; }
        public  double SalePrice{ get; set; }
        public double DiscountPercent { get; set; }
        public int PageSize { get; set; }
        public double CostPrice { get; set; }
        public bool IsAvailable { get; set; }
        public string SubDesc { get; set; }
        public int Rate { get; set; }

        public List<BookImage> BookImages { get; set; } = new List<BookImage>();

        [NotMapped]
        public List<IFormFile> ImageFiles { get; set; }
        [NotMapped]
        public IFormFile PosterFile { get; set; }
        [NotMapped]
        public IFormFile HoverFile { get; set; }

        [NotMapped]
        public List<int> ImageIds { get; set; }

        public List<BookTag> BookTags { get; set; } = new List<BookTag>();
        [NotMapped]
        public List<int> TagIds { get; set; }

    }
}
