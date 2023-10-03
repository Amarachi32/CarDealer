using Car.DLL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Car.DLL.Models
{
    public class UserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(d => d.Email).HasMaxLength(50).HasAnnotation("ErrorMessage", "Email field is required").IsRequired();
            builder.HasIndex(d => d.Email, "IX_UniqueEmail").IsUnique();
        }
    }
}
