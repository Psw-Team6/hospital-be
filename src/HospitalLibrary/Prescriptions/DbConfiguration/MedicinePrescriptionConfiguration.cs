using HospitalLibrary.Prescriptions.Model;
using HospitalLibrary.TreatmentReports.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Prescriptions.DbConfiguration
{
    public class MedicinePrescriptionConfiguration : IEntityTypeConfiguration<MedicinePrescription>
    {
        public void Configure(EntityTypeBuilder<MedicinePrescription> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.HasOne(prescription => prescription.TreatmentReport)
                .WithMany(treatmentReport => treatmentReport.MedicinePrescriptions)
                .HasForeignKey(prescription => prescription.TreatmentReportId);
        }
    }
}