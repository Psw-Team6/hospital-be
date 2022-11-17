using HospitalLibrary.Rooms.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Rooms.DbConfiguration
{
    public class GRoomConfiguration: IEntityTypeConfiguration<GRoom>
    {
        
        public void Configure(EntityTypeBuilder<GRoom> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Id)
                .IsRequired();

        }
    }
}