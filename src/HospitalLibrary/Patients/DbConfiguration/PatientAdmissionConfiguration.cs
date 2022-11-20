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
           
        }
    }
}