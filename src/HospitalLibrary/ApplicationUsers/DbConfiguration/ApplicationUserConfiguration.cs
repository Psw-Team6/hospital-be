using HospitalLibrary.ApplicationUsers.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.ApplicationUsers.DbConfiguration
{
    public class ApplicationUserConfiguration:IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            _ = builder.HasKey(x => x.Id);
            
            _ = builder.Property(x => x.Username)
                .IsRequired();
            _ = builder.HasIndex(x => x.Username)
                .IsUnique();// _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Email)
                .IsRequired();
            
            // _ = builder.Property(x => x.Jmbg)
            //     .HasColumnType("jsonb");
            // _ = builder.Property(x => x.Phone)
            //     .HasColumnType("jsonb");
            

        }
    }
}