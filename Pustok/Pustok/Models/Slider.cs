using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Slider
    {
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Desc { get; set; }
        [MaxLength(150)]
        public string btnText { get; set; }
        [MaxLength(170)]
        public string btnUrl { get; set; }
        [MaxLength(70)]
        public string Title1 { get; set; }
        [MaxLength(80)]
        public string Title2 { get; set; }
        public int Order { get; set;}
        public string SliderImage { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }



    }
}
