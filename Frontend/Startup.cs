using Frontend.Models.Data;
using Frontend.Models.Data.IOTSMSDBContext;
using Frontend.Models.Data.Service;
using Frontend.Models.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Frontend
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
           

            //configure identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
                   .AddEntityFrameworkStores<IOTSMSDBContext>()
                   .AddDefaultTokenProviders();
           
            //configure database connection
            services.AddDbContext<IOTSMSDBContext>(o =>
            {
                o.UseSqlServer(Configuration.GetConnectionString("Conn"));
            });

            //baseurl from appsettings
            var baseurl = Configuration.GetSection("MySettings").GetSection("ApiBaseURL").Value;

            //Get baseurl configured in tye yaml file
            services.AddHttpClient("backendapi", httpClient =>
            {
                httpClient.BaseAddress = new Uri(baseurl);
            });

            //configure password settings
            services.Configure<IdentityOptions>(options =>
            {
                // Default Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
           // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);

                options.LoginPath = "/Accounts/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            //for sessions
            services.AddSession(o=>{
                o.IdleTimeout = TimeSpan.FromMinutes(60);
                o.Cookie.HttpOnly = true;
                o.Cookie.IsEssential = true;
             
            });

            services.AddScoped<UserAccountService>();
            services.AddScoped<SoilCategoryService>();
            services.AddScoped<FarmService>();
            services.AddScoped<RegionService>();
            services.AddScoped<ArduinoService>();
            services.AddScoped<GenderService>();
            services.AddScoped<FarmerService>();
            services.AddScoped<SoilDataService>();
            services.AddScoped<SettingsService>();
            services.AddScoped<FarmMapLocationService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
           
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
