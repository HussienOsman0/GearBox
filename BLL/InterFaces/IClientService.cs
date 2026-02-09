using BLL.View_models;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterFaces
{
    public interface IClientService
    {
        Task<IEnumerable<Booking>> GetMyBookingsAsync(string userId);
        Task AddVehicleAsync(VehicleVM model, string userId);
        Task<IEnumerable<Vehicle>> GetMyVehiclesAsync(string userId);

        
        Task<ClientProfileVM> GetProfileAsync(string userId);
        Task<bool> UpdateProfileAsync(ClientProfileVM model, string userId);
        Task<bool> CreateBookingAsync(BookingVM model, string userId);
    }
}
