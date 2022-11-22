using System;
using System.Linq;
using IntegrationAPI;
using IntegrationLibrary.BloodBank;
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
                .SingleOrDefault(d => d.ServiceType == typeof(IntegrationDbContext));
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
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE public.\"BloodBanks\";");

            BloodBank bloodBank = new()
            {
                Id = Guid.NewGuid(),
                Name = "Vampir",
                ServerAddress = "Vampir12345",
                Email = "deki555@hotmail.com",
                Password = "lpe+uKKi6XM=",
                ApiKey = "NkwQR/sa7Rm97+S7/KQxqWl2nZhnWjzLX3dvHOTngEk="
            };

            context.BloodBanks.Add(bloodBank);
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
        }
        
    }
}