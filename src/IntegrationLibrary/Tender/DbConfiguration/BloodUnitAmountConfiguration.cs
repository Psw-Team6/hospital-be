using IntegrationLibrary.Tender.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Tender.DbConfiguration
{
    public class BloodUnitAmountConfiguration : IEntityTypeConfiguration<Model.BloodUnitAmount>
    {
        public void Configure(EntityTypeBuilder<BloodUnitAmount> builder)
        {
            _ = builder.HasKey(x => x.Id);
        }
    }
}
