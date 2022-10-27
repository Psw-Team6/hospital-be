using System;
using HospitalLibrary.Doctors.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.DbConfigurations
{
    public class SpecializationConfiguration:IEntityTypeConfiguration<Specialization>
    {
        public void Configure(EntityTypeBuilder<Specialization> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.HasIndex(x => x.Name).IsUnique();
            _ = builder.Property(x => x.Name)
                .IsRequired();
            _ = builder.HasData(
                 new {Id=Guid.NewGuid(), Name = "Surgeon"}
                ,new {Id=Guid.NewGuid(), Name = "Dermatology"}
                ,new {Id=Guid.NewGuid(), Name = "General"}
                );
        }
    }
}