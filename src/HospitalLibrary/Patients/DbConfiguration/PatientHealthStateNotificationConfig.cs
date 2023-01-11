using HospitalLibrary.Patients.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Patients.DbConfiguration
{
    public class PatientHealthStateNotificationConfig:IEntityTypeConfiguration<PatientHealthStateNotification>
    {
        public void Configure(EntityTypeBuilder<PatientHealthStateNotification> builder)
        {
            _ = builder.HasKey(notification => notification.Id);
            _ = builder.HasOne(notification => notification.Patient).WithMany();
            _ = builder.Property(notification => notification.Notifications).HasColumnType("text[]");
        }
    }
}