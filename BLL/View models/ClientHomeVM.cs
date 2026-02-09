using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.View_models
{
    public class ClientHomeVM
    {
        public IEnumerable<Booking> Bookings { get; set; }
        public IEnumerable<Vehicle> Vehicles { get; set; }
        public string ClientName { get; set; }
        public IEnumerable<Provider> Providers { get; set; }
    }
}
