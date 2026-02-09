using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.View_models
{
    public class VehicleVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select your car make")]
        [Display(Name = "Car Make")]
        public Make Make { get; set; } // الـ Enum اللي عندك
    }
}
