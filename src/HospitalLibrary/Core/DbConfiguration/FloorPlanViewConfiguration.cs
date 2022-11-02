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
            
            _ = builder.HasKey(x => x.PosX);
            _ = builder.Property(x => x.PosX)
                .IsRequired();
            _ = builder.HasKey(x => x.PosY);
            _ = builder.Property(x => x.PosY)
                .IsRequired();
            
            _ = builder.HasKey(x => x.Lenght);
            _ = builder.Property(x => x.Lenght)
                .IsRequired();
            
            _ = builder.HasKey(x => x.Width);
            _ = builder.Property(x => x.Width)
                .IsRequired();
        } 
    }
}