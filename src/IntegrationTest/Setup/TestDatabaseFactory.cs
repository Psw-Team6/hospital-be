using System;
using System.Linq;
using IntegrationAPI;
using IntegrationAPI.Dtos.Request;
using IntegrationLibrary.BloodBank;
using IntegrationLibrary.BloodBank.Model;
using IntegrationLibrary.BloodRequests.Model;
using IntegrationLibrary.ConfigureGenerateAndSend.Model;
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
            return "Host=localhost;Database=IntegrationTestDB;Username=postgres;Password=password;";
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

            context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"BloodBanks\";");

            BloodBank bloodBank = new()
            {
                Id = Guid.NewGuid(),
                Name = "Vampir",
                ServerAddress = "Vampir12345",
                Email = "deki555@hotmail.com",
                Password = "lpe+uKKi6XM=",
                ApiKey = new IntegrationLibrary.BloodBank.Model.ApiKey()
            };

            BloodBank bloodBank1 = new()
            {
                Id = Guid.NewGuid(),
                Name = "Moja Banka Krvi",
                ServerAddress = "Vampir12345",
                Email = "deki555@hotmail.com",
                Password = "lpe+uKKi6XM=",
                ApiKey = new IntegrationLibrary.BloodBank.Model.ApiKey()
            };


            context.BloodBanks.Add(bloodBank);
            context.BloodBanks.Add(bloodBank1);




            context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"NewsFromBloodBank\";");
            NewsFromBloodBank news1 = new()
            {
                Id = Guid.NewGuid(),
                title = "Dobrovoljno davanje krvi",
                content = "Dodjite na dobrovoljno davanje krvi sutra.",
                apiKey = "NkwQR/sa7Rm97+S7/KQxqWl2nZhnWjzLX3dvHOTngEk=",
                newsStatus = IntegrationLibrary.Enums.NewsFromHospitalStatus.ON_HOLD,
                base64image = "",
                bloodBankName = "Vampir"
            };
            NewsFromBloodBank news2 = new()
            {
                Id = Guid.NewGuid(),
                title = "Dobrovoljno davanje krvi i besplatan pregled",
                content = "Dodjite na dobrovoljno davanje krvi sutra, gdje dobijate i besplatan pregled.",
                apiKey = "NkwQR/sa7Rm97+S7/KQxqWl2nZhnWjzLX3dvHOTngEk=",
                newsStatus = IntegrationLibrary.Enums.NewsFromHospitalStatus.ACTIVE,
                base64image = "",
                bloodBankName = "Vampir"
            };

            context.NewsFromBloodBank.Add(news1);
            context.NewsFromBloodBank.Add(news2);
            context.SaveChanges();

            context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"ConfigureGenerateAndSend\";");

            ConfigureGenerateAndSend configuration1 = new()
            {
                Id = Guid.NewGuid(),
                BloodBankName = "Moja Banka Krvi",
                GeneratePeriod = "ONE_MONTH",
                SendPeriod = "EVERY_TWO_MINUT",
                NextDateForSending = DateTime.Now,

            };
             ConfigureGenerateAndSend configuration2 = new()
            {
                Id = Guid.NewGuid(),
                BloodBankName = "Nova banka",
                GeneratePeriod="TWO_MONTH",
                SendPeriod="SIX_MONTH",
                NextDateForSending = DateTime.Now,
             };

            context.ConfigureGenerateAndSend.Add(configuration1);
            context.ConfigureGenerateAndSend.Add(configuration2);
            context.SaveChanges();

        }
        
    }
}