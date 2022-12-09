using HospitalLibrary.Consiliums.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Consiliums.DbConfiguration
{
    public class ConsiliumConfiguration: IEntityTypeConfiguration<Consilium>
    {
       
        public void Configure(EntityTypeBuilder<Consilium> builder)
        {
            _ = builder.HasMany(consilium => consilium.Doctors)
                .WithMany(doctor=> doctor.Consiliums);
        }
    }
}