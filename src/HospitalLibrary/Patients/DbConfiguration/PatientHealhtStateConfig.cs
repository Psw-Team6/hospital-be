using HospitalLibrary.Patients.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Patients.DbConfiguration
{
    public class PatientHealthStateDbConfig:IEntityTypeConfiguration<PatientHealthState>
    {
        public void Configure(EntityTypeBuilder<PatientHealthState> builder)
        {
            _ = builder.OwnsOne(p => p.BloodPressure);
            _ = builder.OwnsOne(p => p.BloodSugarLevel);
            _ = builder.OwnsOne(p => p.BodyFatPercent);
            _ = builder.OwnsOne(p => p.MenstrualCycle);
            _ = builder.HasOne(p => p.Root).WithMany();
            _ = builder.HasKey(x => x.Id);
            // _ = builder.Property(p => p.MenstrualCycle).IsRequired(false);
        }
    }
}