using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class BookTag
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public Books Books { get; set; }
        public int TagId { get; set; }
        public Tag Tags { get; set; }
    }
}
