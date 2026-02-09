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
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> p)
        {
            p.HasKey(c => c.Id);


            p.Property(c => c.FullName)
                   .IsRequired()
                   .HasColumnType(DBTypes.NvarChar)
                   .HasMaxLength(100);

                   


            p.HasOne(p => p.User)
                   .WithOne(u => u.Provider)
                   .HasForeignKey<Provider>(p=>p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
