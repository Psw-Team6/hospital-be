using HospitalLibrary.BloodConsumptions.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.BloodConsumptions.DbConfiguration
{
    public class BloodConsumptionConfiguration: IEntityTypeConfiguration<BloodConsumption>
    {
        public void Configure(EntityTypeBuilder<BloodConsumption> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.HasOne(bloodConsumption => bloodConsumption.BloodUnit)
                .WithMany(bloodUnit => bloodUnit.Consumptions)
                .HasForeignKey(bloodConsumption => bloodConsumption.BloodUnitId);
        }
    }
}