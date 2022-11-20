using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using HospitalAPI.Exceptions;
using HospitalAPI.Infrastructure;
using HospitalAPI.Mapper;
using HospitalAPI.Validations.Filter;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Doctors.Service;
using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.Feedbacks.Repository;
using HospitalLibrary.Feedbacks.Service;
using HospitalLibrary.Patients.Service;
using HospitalLibrary.Settings;
using HospitalLibrary.sharedModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using NSwag.AspNetCore;

namespace HospitalAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<HospitalDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("HospitalDB")));
            services.Configure<EmailOptions>(Configuration.GetSection(EmailOptions.SendGridEmail));
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddMvc(options =>
                {
                    options.EnableEndpointRouting = false;
                    options.Filters.Add<ValidationFilter>();
                })
#pragma warning disable CS0618
                .AddFluentValidation(mvc => mvc.RegisterValidatorsFromAssemblyContaining<Startup>())
#pragma warning restore CS0618
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
          
            services.AddOpenApiDocument(options =>
            {
                options.SchemaNameGenerator = new CustomSwaggerSchemaNameGenerator();
            });
            services.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
            });
            services.AddTransient<ExceptionMiddleware>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<SpecializationsService>();
            services.AddScoped<ISpecializationsRepository, SpecializationsRepository>();
            services.AddScoped<IRoomService, RoomService>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<FeedbackService>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<DoctorService>();

            services.AddScoped<IWorkingSchueduleRepository, WorkingScheduleRepository>();
            services.AddScoped<WorkingScheduleService>();

            services.AddScoped<PatientService>();
            services.AddScoped<AppointmentService>();
            services.AddScoped<ScheduleService>();
            services.AddScoped<IEmailService, EmailService>();
            services.Configure<EmailOptions>(options => Configuration.GetSection("EmailOptions").Bind(options));
            services.AddScoped<BuildingService>();
            services.AddScoped<FloorService>();
            services.AddScoped<FloorPlanViewService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<HospitalDbContext>();
                context?.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
              //  app.UseSwagger();
              app.UseOpenApi();
              app.UseSwaggerUi3();
              //  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HospitalAPI v1"));
            }

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
