using HospitalLibrary.Examinations.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Examinations.DbConfig
{
    public class PrescriptionDbConfig:IEntityTypeConfiguration<ExaminationPrescription>
    {
        public void Configure(EntityTypeBuilder<ExaminationPrescription> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder
                .HasMany(x => x.Medicines)
                .WithMany(x => x.ExaminationPrescriptions);
        }
    }
}