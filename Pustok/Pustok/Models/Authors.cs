using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pustok.Models
{
    public class Authors
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:100)]
        public string FullName { get; set; }
        public int Age { get; set; }

        public List<Books> books { get; set; }
    }
}
