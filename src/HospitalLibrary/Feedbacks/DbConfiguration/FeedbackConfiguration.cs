using HospitalLibrary.Feedbacks.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Feedbacks.DbConfiguration
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.HasOne(feedback => feedback.Patient)
                .WithMany(patient => patient.Feedbacks)
                .HasForeignKey(feedback => feedback.PatientId);
            /*_ = builder.HasOne(doctor => doctor.WorkingSchedule)
                .WithMany(schedule =>  schedule.Doctors)
                .HasForeignKey(doctor => doctor.WorkingScheduleId);*/
        }
    }
}