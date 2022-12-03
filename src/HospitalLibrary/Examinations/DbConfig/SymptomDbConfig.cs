using System;
using HospitalLibrary.Examinations.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Examinations.DbConfig
{
    public class SymptomDbConfig:IEntityTypeConfiguration<Symptom>
    {
        public void Configure(EntityTypeBuilder<Symptom> builder)
        {
            _ = builder.HasKey(x => x.Id);
            _ = builder.HasData(
                new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Eye irritation"
                },
                new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Runny nose"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Stuffy nose"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Puffy, watery eyes"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Sneezing"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "High temperature"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Difficulty breathing"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Cold"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Flu"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Fever"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Headache"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Eye irritation"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "More intense pain and fatigue"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Dry cough"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Sore throat"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Abdominal pain"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Diarrhea"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Mononucleosis"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Stomach Aches"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Nausea"
                }, new Symptom
                {
                    Id = Guid.NewGuid(),
                    Description = "Vomiting"
                }
            );
        }
    }
}