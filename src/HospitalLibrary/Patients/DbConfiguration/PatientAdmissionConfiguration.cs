using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.Patients.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Patients.DbConfiguration
{
    public class PatientAdmissionConfiguration : IEntityTypeConfiguration<PatientAdmission>
    {
        public void Configure(EntityTypeBuilder<PatientAdmission> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.HasOne(patientAdmission => patientAdmission.Patient)
                .WithMany(patient => patient.PatientAdmissions)
                .HasForeignKey(patientAdmission => patientAdmission.PatientId);
            _ = builder.HasOne(patientAdmission => patientAdmission.SelectedBed)
                .WithMany((roomBed => roomBed.Patients))
                .HasForeignKey(patientAdmission => patientAdmission.SelectedBedId);
            _ = builder.HasOne(patienAdmission => patienAdmission.SelectedRoom)
                .WithMany((room => room.Patients))
                .HasForeignKey(patientAdmission => patientAdmission.SelectedRoomId);

        }
    }
}