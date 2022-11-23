using System;
using System.Linq;
using IntegrationAPI;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodRequests.Model;
using IntegrationLibrary.NewsFromBloodBank.Model;
using IntegrationLibrary.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTest.Setup
{
    public class TestDatabaseFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(collection =>
            {
                using var scope = BuildServiceProvider(collection).CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<IntegrationDbContext>();
                InitializeDataBase(db);
            });
        }
        private static ServiceProvider BuildServiceProvider(IServiceCollection serviceCollection)
        {
            var desc = serviceCollection
                .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<IntegrationDbContext>));
            serviceCollection.Remove(desc);
            serviceCollection.AddDbContext<IntegrationDbContext>(opt =>
            {
                opt.UseNpgsql(CreateConnectionStringForTest());
            });
            return serviceCollection.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Host=localhost;Database=IntegrationTestDB;Username=postgres;Password=tamara;";
        }
        private static void InitializeDataBase(IntegrationDbContext context)
        {

            context.Database.EnsureCreated();
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"BloodRequests\";");

            BloodRequest request1 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.ABneg,
                Amount = 10.0,
                Reason = "SADDDDDDSAD",
                Date = new DateTime(),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };

            BloodRequest request2 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.Bneg,
                Amount = 20.0,
                Reason = "Operacija",
                Date = new DateTime(),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };

            BloodRequest request3 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.ABneg,
                Amount = 10.0,
                Reason = "SADDDDDDSAD",
                Date = new DateTime(),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };

            BloodRequest request4 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.Bneg,
                Amount = 20.0,
                Reason = "Operacija",
                Date = new DateTime(),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };

            BloodRequest request5 = new()
            {
                Id = Guid.NewGuid(),
                Type = BloodType.ABneg,
                Amount = 10.0,
                Reason = "SADDDDDDSAD",
                Date = new DateTime(),
                DoctorUsername = "Ilija",
                Status = Status.PENDING,
                Comment = ""
            };


            context.BloodRequests.Add(request1);
            context.BloodRequests.Add(request2);
            context.BloodRequests.Add(request3);
            context.BloodRequests.Add(request4);


            context.SaveChanges();
        }
        
    }
}