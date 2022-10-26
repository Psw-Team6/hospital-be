using HospitalLibrary.Appointments.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Appointments.DbConfiguration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            _ = builder.HasKey(x => x.Id);

            _ = builder.HasOne(appointment => appointment.Doctor)
                .WithMany(doctor => doctor.Appointments)
                .HasForeignKey(appointment => appointment.DoctorId);
            
            _ = builder.HasOne(appointment => appointment.Patient)
                .WithMany(patient => patient.Appointments)
                .HasForeignKey(appointment => appointment.PatientId);
            // _ = builder.Property(appointment => appointment.TimeSlot.Date).HasColumnName("Date");
            // _ = builder.Property(appointment => appointment.TimeSlot.StartTime).HasColumnName("StartTime");
            // _ = builder.Property(appointment => appointment.TimeSlot.Duration).HasColumnName("Duration");
        }
    }
}