using HospitalLibrary.Rooms.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Rooms.DbConfiguration
{
    public class RoomSplitingConfiguration: IEntityTypeConfiguration<RoomSpliting>
    {
        
        public void Configure(EntityTypeBuilder<RoomSpliting> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Id);

        } 
    }
}