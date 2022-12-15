using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender.DbConfiguration
{
    public class TenderConfiguration : IEntityTypeConfiguration<Model.Tender>
    {

        public void Configure(EntityTypeBuilder<Model.Tender> builder)
        {
            _ = builder.HasMany(tender => tender.BloodUnitAmount)
        .WithOne(BloodUnitAmount => BloodUnitAmount.Tender)
        .HasForeignKey(BloodUnitAmount => BloodUnitAmount.TenderId);
            
        }
    }
}
