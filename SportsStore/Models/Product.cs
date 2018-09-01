using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public class Product
    {

        public int ProductID { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę produktu")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Proszę podać opis produktu")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Proszę podać prawidłową cenę")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Proszę podać kategorię produktu")]
        public string Category { get; set; }
    }
}
