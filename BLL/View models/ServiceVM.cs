using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.View_models
{
    public class ServiceVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Service Name is required")]
        [Display(Name = "Service Name")]
        public string Name { get; set; } 

        [Required]
        [Range(1, 100000, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }
    }
}
