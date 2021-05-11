#region Usings

using Gozen.Business.Passenger;
using Gozen.Business.Passenger.Concreates;
using Gozen.Business.Passenger.Strategy;
using Gozen.Data;
using Gozen.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NLog;

#endregion

namespace Gozen.Service.PassengerApi
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
            services.AddDbContext<GozenDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("GozenMsSQL")));
            GlobalDiagnosticsContext.Set("LogConnection", Configuration.GetConnectionString("LogConnection"));

            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddScoped<IPassengerRepository, PassengerRepository>();
            services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
            services.AddScoped<IPassengerOperation, PassengerOperation>();

            services.AddTransient<IPassengerManager, OfflinePassengerManager>();
            services.AddTransient<IPassengerManager, OnlinePassengerManager>();

            services.AddMvc();

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gozen.Service.PassengerApi", Version = "v1" });
            });

            #region Mapster

            /*
            services.AddMapster(options =>
              {
                  options.Default.IgnoreNonMapped(true); // Does not work.
                TypeAdapterConfig.GlobalSettings.Default.IgnoreNonMapped(true); // Does not work.
            }); 
            */

            #endregion

            services.AddMemoryCache();

            services.AddLogging();

            services.AddCors();

            #region OtherConfig

            /*
            var appSettingsSection = Configuration.GetSection("AppSettings");
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.RequireHttpsMetadata = false;
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "test@test.com",
                    ValidAudience = "suha@test.com",
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            */

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, GozenDbContext gozenDbContext)
        {
            // Migrate at Start
            gozenDbContext.Database.Migrate();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gozen.Service.PassengerApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            //app.UseMvc();

            app.UseCors();
        }
    }
}