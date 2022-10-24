using System.Reflection;
using HospitalLibrary.Core.Model;
using HospitalLibrary.DoctorRole.Model;
using HospitalLibrary.sharedModel;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
       // public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<Address> Addresses { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // _ = modelBuilder.Entity<Doctor>().HasOne(doctor => doctor.Specialization)
            //     .WithMany(specialization => specialization.Doctors)
            //     .HasForeignKey(doctor => doctor.SpecializationId);
            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Number = "101A", Floor = 1 },
                new Room() { Id = 2, Number = "204", Floor = 2 },
                new Room() { Id = 3, Number = "305B", Floor = 3 }
            );
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
