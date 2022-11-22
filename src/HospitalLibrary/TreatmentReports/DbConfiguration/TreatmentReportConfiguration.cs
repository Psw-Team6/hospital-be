using HospitalLibrary.TreatmentReports.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.TreatmentReports.DbConfiguration
{
    public class TreatmentReportConfiguration : IEntityTypeConfiguration<TreatmentReport>
    {
        public void Configure(EntityTypeBuilder<TreatmentReport> builder)
        {
            _ = builder.HasKey(x => x.Id);
            /*_ = builder.HasOne(report => report.PatientAdmission)
                .WithMany(patient => patient.TreatmentReports)
                .HasForeignKey(report => report.PatientId);*/
        }
    }
}