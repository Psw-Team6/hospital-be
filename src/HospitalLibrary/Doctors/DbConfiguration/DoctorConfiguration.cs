using HospitalLibrary.Doctors.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Doctors.DbConfiguration
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            // _ = builder.HasKey(x => x.Id);
            //
            // _ = builder.Property(x => x.Username)
            //     .IsRequired();
            // _ = builder.Property(x => x.Email)
            //     .IsRequired();// _ = builder.HasKey(x => x.Id);
            //
            // _ = builder.Property(x => x.Username)
            //     .IsRequired();
            // _ = builder.Property(x => x.Email)
            //     .IsRequired();
            _ = builder.HasOne(doctor => doctor.Specialization)
                .WithMany(specialization => specialization.Doctors)
                .HasForeignKey(doctor => doctor.SpecializationId);
            _ = builder.HasMany(doctor => doctor.Patients)
                .WithOne(patient => patient.Doctor)
                .HasForeignKey(patient => patient.DoctorId);
        }
    }
}