using HospitalLibrary.Doctors.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Doctors.DbConfiguration
{
    public class WorkingConfiguration:IEntityTypeConfiguration<WorkingSchedule>
    {
        public void Configure(EntityTypeBuilder<WorkingSchedule> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.OwnsOne(app => app.ExpirationDate).
                Property(x => x.From)
                .HasColumnName("ScheduleFrom");
            _ = builder.OwnsOne(app => app.ExpirationDate).
                Property(x => x.To)
                .HasColumnName("ScheduleTo");
            _ = builder.OwnsOne(app => app.DayOfWork).
                Property(x => x.From)
                .HasColumnName("DayOfWorkFrom");
            _ = builder.OwnsOne(app => app.DayOfWork).
                Property(x => x.To)
                .HasColumnName("DayOfWorkTo");
        }
    }
}