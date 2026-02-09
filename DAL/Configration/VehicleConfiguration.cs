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
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
           

            builder.HasKey(v => v.Id);

            
            builder.Property(v => v.make)
                   .IsRequired()
                   .HasConversion<int>();
            

            
            builder.HasOne(v => v.Client)
                   .WithMany(c => c.Vehicles)
                   .HasForeignKey(v => v.ClientID)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
