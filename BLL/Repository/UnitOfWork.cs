using BLL.InterFaces;
using DAL.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<Client> Clients { get; private set; }
        
        public IGenericRepository<Provider> Providers { get; private set; }
        public IGenericRepository<Vehicle> Vehicles { get; private set; }
        public IGenericRepository<Service> Services { get; private set; }
        public IGenericRepository<Booking> Bookings { get; private set; }


        public UnitOfWork(AppDbContext context)
        {
            _context = context;

            Clients = new GenericRepository<Client>(_context);
            
            Providers = new GenericRepository<Provider>(_context);
            Vehicles = new GenericRepository<Vehicle>(_context);
            Services = new GenericRepository<Service>(_context);
            Bookings = new GenericRepository<Booking>(_context);
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
