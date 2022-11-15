using HospitalLibrary.Rooms.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Rooms.DbConfiguration
{
    public class FloorConfiguration: IEntityTypeConfiguration<Floor>
    {
        
        public void Configure(EntityTypeBuilder<Floor> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Id)
                .IsRequired();
            
        } 
    }
}