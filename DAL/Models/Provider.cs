using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserId { get; set; }
        public ProviderType Type { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<Booking> Bookings { get; set; }

    }
}
