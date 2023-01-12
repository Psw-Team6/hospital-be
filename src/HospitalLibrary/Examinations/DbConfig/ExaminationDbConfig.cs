using HospitalLibrary.Examinations.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Examinations.DbConfig
{
    public class ExaminationDbConfig:IEntityTypeConfiguration<Examination>
    {
        public void Configure(EntityTypeBuilder<Examination> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder
                .HasMany(x => x.Symptoms)
                .WithMany(x => x.Examinations);
            _ = builder.Property(x => x.Anamnesis);
            _ = builder
                .HasMany(x => x.Prescriptions)
                .WithOne()
                .HasForeignKey(x => x.Id);
                
            _ = builder.Ignore(x => x.IdApp);
            _ = builder.Ignore(x => x.Changes);
        }
    }
}