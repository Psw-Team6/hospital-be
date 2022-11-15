using HospitalLibrary.BloodConsumptions.Model;
using HospitalLibrary.BloodUnits.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.BloodUnits.DbConfiguration
{
    public class BloodUnitConfiguration: IEntityTypeConfiguration<BloodConsumption>
    {
        public void Configure(EntityTypeBuilder<BloodConsumption> builder)
        {
            _ = builder.HasKey(x => x.Id);
        }
    }
}