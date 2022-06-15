using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Promotions
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:150)]
        public string Image { get; set; }
        [StringLength(maximumLength: 150)]
        public string imgUrl { get; set; }
    }
}
