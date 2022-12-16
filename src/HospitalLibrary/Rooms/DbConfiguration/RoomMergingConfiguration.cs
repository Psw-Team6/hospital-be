using HospitalLibrary.Rooms.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Rooms.DbConfiguration
{
    public class RoomMergingConfiguration: IEntityTypeConfiguration<RoomMerging>
    {
        
        public void Configure(EntityTypeBuilder<RoomMerging> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Id);

        } 
    }
}