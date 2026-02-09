using DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.View_models
{
    public class ProviderProfileVM
    {
        public int Id { get; set; } // Provider Id

        [Required, Display(Name = "Business/Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; } // Read-Only

        [Phone, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required, Display(Name = "Service Type")]
        public ProviderType Type { get; set; } // يقدر يغير تخصصه
    }
}
