using HospitalLibrary.Appointments.Model;
using HospitalLibrary.Doctors.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Appointments.DbConfiguration
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            _ = builder.HasKey(x => x.Id);

            // _ = builder.Property(x => x.Username)
            //     .IsRequired();
            // _ = builder.Property(x => x.Email)
            //     .IsRequired();
            _ = builder.HasOne(appointment => appointment.Doctor)
                .WithMany(doctor => doctor.Appointments)
                .HasForeignKey(appointment => appointment.DoctorId);
            _ = builder.HasOne(appointment => appointment.Patient)
                .WithMany(patient => patient.Appointments)
                .HasForeignKey(appointment => appointment.PatientId);
            _ = builder.HasOne(appointment => appointment.Room)
                .WithMany(room => room.Appointments)
                .HasForeignKey(appointment => appointment.RoomId);
            // _ = builder.HasOne(doctor => doctor.Address)
            //     .WithMany(address => address.ApplicationUsers)
            //     .HasForeignKey(doctor => doctor.SpecializationId);
        }
    }
}