using System.Text;
using FluentValidation.AspNetCore;
using HospitalAPI.Exceptions;
using HospitalAPI.Extensions;
using HospitalAPI.Infrastructure;
using HospitalAPI.Mapper;
using HospitalAPI.Validations.Filter;
using HospitalLibrary.Appointments.Service;
using HospitalLibrary.EquipmentMovement;
using HospitalLibrary.EquipmentMovement.Service;
using HospitalLibrary.Settings;
using HospitalLibrary.sharedModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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
            options.UseNpgsql(Configuration.GetConnectionString("HospitalDB")!));
            services.Configure<EmailOptions>(Configuration.GetSection(EmailOptions.SendGridEmail));
            services.AddAutoMapper(typeof(MappingProfile));
            //services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
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
            
            services.AddScoped<IEquipmentMovementAppointmentService, EquipmentMovementAppointmentService>();
            services.AddSingleton<IHostedService, CheckIfAppointmentIsDone>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.Configure<EmailOptions>(options => Configuration.GetSection("EmailOptions").Bind(options));
            services.AddMyDependencyGroup();
            services.AddHttpClient();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("veryverysecret.....")),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                }
            );
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
