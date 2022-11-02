using HospitalLibrary.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Core.DbConfiguration
{
    public class FloorPlanViewConfiguration: IEntityTypeConfiguration<FloorPlanView>
    {
        
        public void Configure(EntityTypeBuilder<FloorPlanView> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Id)
                .IsRequired();
        } 
    }
}