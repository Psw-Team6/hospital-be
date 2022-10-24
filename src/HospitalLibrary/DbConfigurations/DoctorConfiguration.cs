using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HospitalLibrary.Doctors.Model;


namespace HospitalLibrary.DbConfigurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            _ = builder.HasKey(x => x.Id);

            _ = builder.Property(x => x.Username)
                .IsRequired();
            _ = builder.Property(x => x.Email)
                .IsRequired();
            _ = builder.HasOne(doctor => doctor.Specialization)
                .WithMany(specialization => specialization.Doctors)
                .HasForeignKey(doctor => doctor.SpecializationId);
            // _ = builder.HasOne(doctor => doctor.Address)
            //     .WithMany(address => address.ApplicationUsers)
            //     .HasForeignKey(doctor => doctor.SpecializationId);
        }
    }
}