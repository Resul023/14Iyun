using Pustok.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.ViewModels
{
    public class ShopViewModel
    {
        public List<Genres> Genre { get; set; }
        public List<Books> book { get; set; }
    }
}
