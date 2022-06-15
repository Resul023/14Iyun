using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Genres
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:60)]
        public string Name { get; set; }

        public List<Books> books { get; set; }
    }
}
