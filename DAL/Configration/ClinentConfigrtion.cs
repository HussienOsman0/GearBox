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
    public class ClinentConfigrtion : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> c)
        {
            c.HasKey(c => c.Id);
            c.Property(c => c.FullName)
                .IsRequired()
                .HasColumnType(DBTypes.NvarChar)
                .HasMaxLength(100);
            c.HasOne(c => c.user)
                .WithOne(u => u.client)
                .HasForeignKey<Client>(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
