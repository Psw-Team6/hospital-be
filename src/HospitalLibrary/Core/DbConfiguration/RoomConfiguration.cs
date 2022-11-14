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
            _ = builder.HasMany((room) => room.Beds)
                .WithOne(bed => bed.Room)
                .HasForeignKey(bed => bed.RoomId);
        }
    }
}