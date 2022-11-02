using HospitalLibrary.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Core.DbConfiguration
{
    public class RoomConfiguration : IEntityTypeConfiguration<Room>
    {
        
        public void Configure(EntityTypeBuilder<Room> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Id)
                .IsRequired();
            
            _ = builder.HasKey(x => x.BuildingId);
            _ = builder.Property(x => x.BuildingId)
                .IsRequired();
            
            _ = builder.HasKey(x => x.FloorId);
            _ = builder.Property(x => x.FloorId)
                .IsRequired();
            
            _ = builder.HasKey(x => x.FloorPlanViewId);
            _ = builder.Property(x => x.FloorPlanViewId)
                .IsRequired();
        }
    }
}