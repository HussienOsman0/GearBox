using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.View_models
{
    public class BookingVM
    {
        [Required]
        [Display(Name = "Booking Date")]
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; } = DateTime.Now.AddDays(1);

        [Required]
        [Display(Name = "Select Your Vehicle")]
        public int VehicleId { get; set; } // العربية اللي هيتعمل لها صيانة

        public int ProviderId { get; set; } // الـ ID بتاع الميكانيكي/الونش (Hidden)

        public string ProviderName { get; set; } // عشان نعرض اسم الراجل اللي بنحجز معاه
    }
}
