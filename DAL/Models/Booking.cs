using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime BookingDate { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public int ProviderId { get; set; }
        public Provider Provider { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }


    }
}
