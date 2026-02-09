using BLL.InterFaces;
using BLL.View_models;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ClientService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        private async Task<Client> GetClientByUserId(string userId)
        {
            var clinet = await _unitOfWork.Clients.FindAsync(c => c.UserID == userId);
            return clinet.FirstOrDefault();
        }

        public async Task AddVehicleAsync(VehicleVM model, string userId)
        {
            var client = await GetClientByUserId(userId);
            if (client != null)
            {
                var vehicle = new Vehicle
                {
                    make = model.Make,
                    ClientID = client.Id // ربطنا العربية بالعميل
                };
                await _unitOfWork.Vehicles.AddAsync(vehicle);
                await _unitOfWork.CompleteAsync();

            }

        }

        public async Task<IEnumerable<Vehicle>> GetMyVehiclesAsync(string userId)
        {
            var client = await GetClientByUserId(userId);
            if (client == null) return new List<Vehicle>();

            return await _unitOfWork.Vehicles.FindAsync(v => v.ClientID == client.Id);
        }

        public async Task<bool> CreateBookingAsync(BookingVM model, string userId)
        {
            var client = await GetClientByUserId(userId);

            if (client != null)
            {
                var booking = new Booking
                {
                    BookingDate = model.BookingDate,
                    Status = DAL.Enums.BookingStatus.Pending, // الحالة المبدئية
                    ProviderId = model.ProviderId,
                    ClientId = client.Id,
                    VehicleId = model.VehicleId
                };

                await _unitOfWork.Bookings.AddAsync(booking);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }
        public async Task<IEnumerable<Booking>> GetMyBookingsAsync(string userId)
        {
            var client = await GetClientByUserId(userId);
            if (client == null) return new List<Booking>();

            // بنجيب الحجوزات ونعمل Include للبروفيدر عشان نعرض اسمه، والعربية
            return await _unitOfWork.Bookings.FindAsync(
                b => b.ClientId == client.Id,
                new[] { "Provider", "Vehicle" }
            );
        }

        public async Task<ClientProfileVM> GetProfileAsync(string userId)
        {
            var client = await GetClientByUserId(userId);
            var user = await _userManager.FindByIdAsync(userId);

            if (client == null || user == null) return null;

            return new ClientProfileVM
            {
                Id = client.Id,
                FullName = client.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
        }

        // 2. Update Profile
        public async Task<bool> UpdateProfileAsync(ClientProfileVM model, string userId)
        {
            var client = await GetClientByUserId(userId);
            var user = await _userManager.FindByIdAsync(userId);

            if (client == null || user == null) return false;

            // تحديث جدول Clients
            client.FullName = model.FullName;
            _unitOfWork.Clients.Update(client);

            // تحديث جدول AspNetUsers
            user.PhoneNumber = model.PhoneNumber;
            await _userManager.UpdateAsync(user);

            await _unitOfWork.CompleteAsync();
            return true;
        }

    }
}
