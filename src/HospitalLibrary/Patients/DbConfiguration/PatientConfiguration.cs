using HospitalLibrary.Patients.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Patients.DbConfiguration
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            _ = builder.HasKey(x => x.Id);
            
            _ = builder.Property(x => x.Username)
                .IsRequired();
            
            _ = builder.Property(x => x.Email)
                .IsRequired();
            
            _ = builder.HasMany(patient => patient.Allergies)
                .WithMany(allergies => allergies.Patients);
            
            _ = builder.HasMany(patient => patient.PatientAdmissions)
                .WithOne(patientAdmission => patientAdmission.Patient)
                .HasForeignKey(patientAdmission => patientAdmission.PatientId);

        }
    }
}