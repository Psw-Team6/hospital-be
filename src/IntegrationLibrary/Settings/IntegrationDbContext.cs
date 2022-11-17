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

        public IntegrationDbContext(DbContextOptions<IntegrationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
