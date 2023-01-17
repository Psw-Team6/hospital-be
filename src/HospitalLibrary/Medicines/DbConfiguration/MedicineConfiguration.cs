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
                Name = "Xanax",
                Amount = 1000
            };
            var med2 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Aderol",
                Amount = 1000
            };
            var med3 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Defrinol",
                Amount = 1000
            };
            var med4 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Fervex",
                Amount = 1000
            };
            var med5 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Paracetanol",
                Amount = 1000
            };
            var med6 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Cefalexin",
                Amount = 1000
            };
            
            _ = builder.HasData(med1,med2,med3,med4,med5,med6);
            
            var med7 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Kafetin",
                Amount = 1000
            };
            var med8 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Hemomicin",
                Amount = 1000
            };
            var med9 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Strpsils",
                Amount = 1000
            };
            var med10 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Dexomin",
                Amount = 1000
            };
            var med11 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Lexilium",
                Amount = 1000
            };
            var med12 = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = "Aerius",
                Amount = 1000
            };
            
            _ = builder.HasData(med7,med8,med9,med10,med11,med12);
        }
    }
}