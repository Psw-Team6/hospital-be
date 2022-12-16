using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.SharedModel.dbConfiguration
{
    public class AllergenConfiguration : IEntityTypeConfiguration<Allergen>
    {
        public void Configure(EntityTypeBuilder<Allergen> builder)
        {
            _ = builder.HasMany(patient => patient.Patients)
                .WithMany(patient => patient.Allergies);
        }
    }
}