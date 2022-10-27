using HospitalLibrary.Doctors.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Doctors.DbConfiguration
{
    public class HolidayConfiguration  : IEntityTypeConfiguration<Holiday>
    {
        public void Configure(EntityTypeBuilder<Holiday> builder)
        {
            _ = builder.HasKey(x => x.Id);
        }
    }
}