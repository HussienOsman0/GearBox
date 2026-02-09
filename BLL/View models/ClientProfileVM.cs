using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.View_models
{
    public class ClientProfileVM
    {
        public int Id { get; set; } // Client Id

        [Required, Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; } // للعرض فقط (Read-Only)

        [Phone, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } // موجود في جدول Users
    }
}
