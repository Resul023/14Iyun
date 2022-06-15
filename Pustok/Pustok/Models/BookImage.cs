using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class BookImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BookId { get; set; }
        public bool? PosterStatus { get; set; }

        public Books Book { get; set; }
    }
}
