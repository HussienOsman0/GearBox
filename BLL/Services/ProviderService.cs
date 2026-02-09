using BLL.InterFaces;
using BLL.View_models;
using DAL.Enums;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProviderService : IProviderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProviderService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }


        private async Task<Provider> GetProviderByUserId(string userId)
        {
            
            var providers = await _unitOfWork.Providers.FindAsync(p => p.UserId == userId);
            return providers.FirstOrDefault();
        }

        
        public async Task AddServiceAsync(ServiceVM model, string userId)
        {
            var provider = await GetProviderByUserId(userId);
            if (provider != null)
            {
                var service = new Service
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    ProviderId = provider.Id
                };

                await _unitOfWork.Services.AddAsync(service);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<IEnumerable<Service>> GetMyServicesAsync(string userId)
        {
            var provider = await GetProviderByUserId(userId);
            if (provider == null) return new List<Service>();

            return await _unitOfWork.Services.FindAsync(s => s.ProviderId == provider.Id);
        }


        public async Task<IEnumerable<Booking>> GetIncomingBookingsAsync(string userId)
        {
            // 1. هات البروفيدر بناءً على اليوزر اللي عامل لوجين
            // تأكد إن FindAsync بترجع List وإننا بناخد FirstOrDefault
            var providers = await _unitOfWork.Providers.FindAsync(p => p.UserId == userId);
            var provider = providers.FirstOrDefault();

            if (provider == null)
            {
                // لو دخل هنا يبقى اليوزر ده مش متسجل كـ Provider في الداتابيز
                return new List<Booking>();
            }

            // 2. هات الحجوزات الخاصة بالـ ID ده
            return await _unitOfWork.Bookings.FindAsync(
                b => b.ProviderId == provider.Id,
                new[] { "Client", "Vehicle" }
            );
        }

        public async Task<bool> ChangeBookingStatusAsync(int bookingId, BookingStatus status)
        {
            var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
            if (booking != null)
            {
                booking.Status = status;
                _unitOfWork.Bookings.Update(booking); 
                await _unitOfWork.CompleteAsync();
                return true;
            }
            return false;
        }


        public async Task<ProviderProfileVM> GetProfileAsync(string userId)
        {
            var provider = await GetProviderByUserId(userId);
            var user = await _userManager.FindByIdAsync(userId);

            if (provider == null) return null;

            return new ProviderProfileVM
            {
                Id = provider.Id,
                FullName = provider.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Type = provider.Type
            };
        }

        public async Task<bool> UpdateProfileAsync(ProviderProfileVM model, string userId)
        {
            var provider = await GetProviderByUserId(userId);
            var user = await _userManager.FindByIdAsync(userId);

            if (provider == null) return false;

            
            provider.FullName = model.FullName;
            provider.Type = model.Type; 
            _unitOfWork.Providers.Update(provider);

            
            user.PhoneNumber = model.PhoneNumber;
            await _userManager.UpdateAsync(user);

            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
