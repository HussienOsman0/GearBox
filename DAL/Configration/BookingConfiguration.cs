using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Configration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            

            builder.HasKey(b => b.Id);

            builder.Property(b => b.BookingDate)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(b => b.Status)
                   .IsRequired()
                   .HasConversion<int>(); 

            
            builder.HasOne(b => b.Provider)
                   .WithMany(p => p.Bookings)
                   .HasForeignKey(b => b.ProviderId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(b => b.Client)
                   .WithMany(c => c.Bookings)
                   .HasForeignKey(b => b.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            
            builder.HasOne(b => b.Vehicle)
                   .WithOne(v => v.booking)
                   .HasForeignKey<Booking>(b=>b.VehicleId)
                   .OnDelete(DeleteBehavior.Cascade);

            
            builder.HasIndex(b => b.ProviderId);
            builder.HasIndex(b => b.ClientId);
            builder.HasIndex(b => b.VehicleId);
        }
    }
}
