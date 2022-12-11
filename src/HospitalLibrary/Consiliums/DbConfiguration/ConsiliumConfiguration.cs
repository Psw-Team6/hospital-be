using HospitalLibrary.Consiliums.Model;
using HospitalLibrary.Doctors.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Consiliums.DbConfiguration
{
    public class ConsiliumConfiguration: IEntityTypeConfiguration<Consilium>
    {
       
        public void Configure(EntityTypeBuilder<Consilium> builder)
        {
            builder.HasMany(c => c.Doctors)
                .WithMany(doctor => doctor.Consiliums);
        }
    }
}