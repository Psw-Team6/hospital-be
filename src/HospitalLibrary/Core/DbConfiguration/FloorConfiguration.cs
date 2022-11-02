using HospitalLibrary.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Core.DbConfiguration
{
    public class FloorConfiguration: IEntityTypeConfiguration<Floor>
    {
        
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Id)
                .IsRequired();
            
            _ = builder.HasKey(x => x.BuildingId);
            _ = builder.Property(x => x.BuildingId)
                .IsRequired();
            
            _ = builder.HasKey(x => x.FloorPlanViewId);
            _ = builder.Property(x => x.FloorPlanViewId)
                .IsRequired();
        } 
    }
}