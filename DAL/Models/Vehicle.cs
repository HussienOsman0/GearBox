using DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public Make make { get; set; }
        
        public int ClientID {  get; set; }
        public Client Client {  get; set; }
        public Booking booking { get; set; }
    }
}
