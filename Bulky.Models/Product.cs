using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models
{
    internal class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title {  get; set; }

        public string Description { get; set; }

        [Required]
        public string ISBN { get; set; }

        [Required]
        public string Author { get; set; }
        [Required]
        [Display(Name = "Price for 1 - 50")]
        [Range(1,1000)]
        public double ListPrice {  get; set; }

        [Required]
        [Display(Name = "Price for 50 - 100")]
        [Range(1, 10000)]
        public double ListPrice50 { get; set; }

        [Required]
        [Display(Name = "Price above 1000")]
        [Range(1, 1000000)]
        public double ListPrice100 { get; set; }

    }
}
