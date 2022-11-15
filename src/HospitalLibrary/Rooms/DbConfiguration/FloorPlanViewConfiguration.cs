using HospitalLibrary.Rooms.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Rooms.DbConfiguration
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