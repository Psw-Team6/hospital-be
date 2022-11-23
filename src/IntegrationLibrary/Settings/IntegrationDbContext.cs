using IntegrationLibrary.BloodRequests.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Settings
{
    public class IntegrationDbContext: DbContext
    {
        public DbSet<BloodBank.BloodBank> BloodBanks { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }

        public DbSet<NewsFromBloodBank.Model.NewsFromBloodBank> NewsFromBloodBank { get; set; }

        public IntegrationDbContext(DbContextOptions<IntegrationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            


            //Start data for Blood requests

            BloodRequest request1 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.ABneg,
                Amount = 10.0,
                Reason = "Operacija",
                Date = new DateTime(2022,12,10),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };

            BloodRequest request2 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.Bneg,
                Amount = 20.0,
                Reason = "Transfuzija",
                Date = new DateTime(2022, 12, 20),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };

            BloodRequest request3 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.Apos,
                Amount = 20.0,
                Reason = "Transfuzija",
                Date = new DateTime(2023, 1, 20),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };


            BloodRequest request4 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.Opos,
                Amount = 5.0,
                Reason = "Zalihe",
                Date = new DateTime(2023, 1, 20),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };

            modelBuilder.Entity<BloodRequest>().HasData(
                request1,
                request2,
                request3,
                request4
            );

            
        }


    }
}
