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
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("Services");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("nvarchar(100)");

            builder.Property(s => s.Description)
                   .HasMaxLength(500)
                   .HasColumnType("nvarchar(500)");

            builder.Property(s => s.Price)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            
            builder.HasOne(s => s.provider)
                   .WithMany(p => p.Services)
                   .HasForeignKey(s => s.ProviderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

