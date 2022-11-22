using HospitalLibrary.EquipmentMovement.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.EquipmentMovement.DbConfiguration
{
    public class EquipmentMovementAppointmentConfiguration: IEntityTypeConfiguration<EquipmentMovementAppointment>
    {
        
        public void Configure(EntityTypeBuilder<EquipmentMovementAppointment> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Id)
                .IsRequired();
            
        } 
    }
}