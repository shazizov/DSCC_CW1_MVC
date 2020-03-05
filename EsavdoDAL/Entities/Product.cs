using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsavdoDAL.Resources;


namespace EsavdoDAL.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [Display(ResourceType = typeof(HomeTexts))]
        public string Name { get; set; }

        [Required]
        [Display(ResourceType = typeof(HomeTexts))]
        public decimal Price { get; set; }

        [Required]
        [Display(ResourceType = typeof(HomeTexts))]
        public bool Available { get; set; }

        [Required]
        [Display(ResourceType = typeof(HomeTexts))]
        public string Category { get; set; }
    }
}
