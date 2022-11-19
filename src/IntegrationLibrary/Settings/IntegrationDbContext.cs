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

        public IntegrationDbContext(DbContextOptions<IntegrationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
            BloodBank.BloodBank bloodBank = new()
            {
                Id = Guid.NewGuid(),
                Name = "BloodBank",
                ServerAddress = "localhost",
                Email = "aas@gmail.com",
                Password = "123",
                ApiKey = "x"
            };
            modelBuilder.Entity<BloodBank.BloodBank>().HasData(bloodBank);
        }
    }
}
