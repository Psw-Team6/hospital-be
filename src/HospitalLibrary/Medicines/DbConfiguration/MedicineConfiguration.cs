using HospitalLibrary.Medicines.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Medicines.DbConfiguration
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Amount)
                .IsRequired();
            _ = builder.Property(x => x.Name)
                .IsRequired();
            _ = builder.HasMany(medicine => medicine.Ingredients)
                .WithMany(ingredient => ingredient.Medicines);
        }
    }
}