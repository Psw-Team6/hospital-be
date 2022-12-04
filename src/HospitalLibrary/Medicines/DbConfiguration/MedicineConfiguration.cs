using System;
using System.Collections.Generic;
using HospitalLibrary.Medicines.Model;
using HospitalLibrary.SharedModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Medicines.DbConfiguration
{
    public class MedicineConfiguration : IEntityTypeConfiguration<Medicine>
    {
        public void Configure(EntityTypeBuilder<Medicine> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.Property(x => x.Amount)
                .IsRequired();
            _ = builder.Property(x => x.Name)
                .IsRequired();
            _ = builder.HasMany(medicine => medicine.Ingredients)
                .WithMany(ingredient => ingredient.Medicines);
            var ing1 = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Paracetamol"
            };
            var ing2 = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Acetaminophen"
            };
            var ing3 = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Mucinex"
            };
            var ing4 = new Ingredient
            {
                Id = Guid.NewGuid(),
                Name = "Tylenol"
            };
            var ingredients1 = new List<Ingredient> {ing1,ing2}; 
            var ingredients2 = new List<Ingredient> {ing1,ing3}; 
            var ingredients3 = new List<Ingredient> {ing1,ing4}; 
            var ingredients5 = new List<Ingredient> {ing1,ing2};
            var med1 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Medicine1",
                Amount = 1000
            };
            var med2 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Medicine2",
                Amount = 1000
            };
            var med3 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Medicine3",
                Amount = 1000
            };
            var med4 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Medicine4",
                Amount = 1000
            };
            var med5 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Medicine5",
                Amount = 1000
            };
            var med6 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Medicine6",
                Amount = 1000
            };
            _ = builder.HasData(med1,med2,med3,med4,med5,med6);
        }
    }
}