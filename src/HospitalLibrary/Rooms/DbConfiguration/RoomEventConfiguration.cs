using HospitalLibrary.Rooms.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Rooms.DbConfiguration
{
    public class RoomEventConfiguration: IEntityTypeConfiguration<RoomEvent>
    {
        public void Configure(EntityTypeBuilder<RoomEvent> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Id)
                .IsRequired();
        }
    }
}