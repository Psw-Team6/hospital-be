using System;
using HospitalLibrary.Rooms.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Rooms.DbConfiguration
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
            
            _ = builder.HasOne((room) => room.Floor)
                .WithMany(floor => floor.Rooms)
                .HasForeignKey(room => room.FloorId);
            
        }
    }
}