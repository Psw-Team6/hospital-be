using System;
using System.Text.Json.Serialization;
using FluentValidation.AspNetCore;
using HospitalAPI.Exceptions;
using HospitalAPI.Infrastructure;
using HospitalAPI.Mapper;
using HospitalAPI.Validations.Filter;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.Common;
using HospitalLibrary.Doctors.Repository;
using HospitalLibrary.Doctors.Service;
using HospitalLibrary.Feedbacks.Model;
using HospitalLibrary.Feedbacks.Repository;
using HospitalLibrary.Feedbacks.Service;
using HospitalLibrary.Patients.Service;
using HospitalLibrary.Rooms.Repository;
using HospitalLibrary.Rooms.Service;
using HospitalLibrary.Settings;
using HospitalLibrary.sharedModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
            services.AddScoped<RoomService>();
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
            services.AddScoped<GRoomService>();
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            
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
              app.UseReDoc();
              //  app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HospitalAPI v1"));
            }

            app.UseRouting();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
