using HospitalLibrary.Holidays.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Holidays.DbConfiguration
{
    public class HolidayConfiguration  : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.OwnsOne(holiday => holiday.DateRange);
            _ = builder.HasOne(holiday => holiday.Doctor)
                .WithMany(doctor => doctor.Holidays)
                .HasForeignKey(holiday => holiday.DoctorId);
        }
    }
}