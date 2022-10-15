using Backendapi.Models.Data.Service;
using Backendapi.Models.Data;
using Backendapi.Models.Data.IOTSMSDBContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Backendapi
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
            services.AddControllers();
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<IOTSMSDBContext>()
                    .AddDefaultTokenProviders();

            services.AddDbContext<IOTSMSDBContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("Conn"));
            });


            services.AddScoped<AccountService>();
            services.AddScoped<GenderService>();
            services.AddScoped<RegionService>();
            services.AddScoped<SoilCategoryService>();
            services.AddScoped<ArduinoService>();
            services.AddScoped<FarmerService>();
            services.AddScoped<SoilDataService>();
            services.AddScoped<FarmService>();
            services.AddScoped<SettingsService>();
            services.AddScoped<SMSService>();
            services.AddScoped<MapLocationService>();
            


            //For swagger
            services.AddSwaggerGen(options => {

                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "IOT Soil Monitoring System",
                        Description = "Developing apis for IOT Soil Monitoring System",
                        Version = "v1"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

           

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IOT Soil Monitoring System");

            });
        }
    }
}
