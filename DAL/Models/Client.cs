using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserID { get; set; }
        public ApplicationUser user { get; set; } 
        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<Booking> Bookings {  get; set; }
    }
}
