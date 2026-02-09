using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterFaces
{
    public interface IUnitOfWork :IDisposable
    {
        public IGenericRepository<Vehicle> Vehicles { get;  }
        public IGenericRepository<Service> Services { get; }
        public IGenericRepository<Booking> Bookings { get;  }
        
        public IGenericRepository<Client> Clients { get; }
        public IGenericRepository<Provider> Providers { get; }

        Task<int> CompleteAsync();
    }
}
