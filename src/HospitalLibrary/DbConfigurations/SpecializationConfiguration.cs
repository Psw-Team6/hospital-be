using HospitalLibrary.Doctors.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.DbConfigurations
{
    public class SpecializationConfiguration:IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            _ = builder.HasKey(x => x.Id);

            _ = builder.Property(x => x.Name)
                .IsRequired();
            // _ = builder.HasMany(specialization => specialization.Doctors)
            //     .WithOne(doctor => doctor.Specialization)
            //     .HasForeignKey();
        }
    }
}