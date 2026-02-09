using BLL.View_models;
using DAL.Enums;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.InterFaces
{
    public interface IProviderService
    {
        
        Task AddServiceAsync(ServiceVM model, string userId);
        Task<IEnumerable<Service>> GetMyServicesAsync(string userId);

        Task<ProviderProfileVM> GetProfileAsync(string userId);
        Task<bool> UpdateProfileAsync(ProviderProfileVM model, string userId);
        Task<IEnumerable<Booking>> GetIncomingBookingsAsync(string userId);
        Task<bool> ChangeBookingStatusAsync(int bookingId, BookingStatus status);
    }
}
