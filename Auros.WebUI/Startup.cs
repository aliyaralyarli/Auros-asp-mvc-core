using Auros.WebUI.Models.DataContexts;
using Auros.WebUI.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Auros.WebUI
{
    public class Startup
    {
        readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddSession(cfg =>
            {
                cfg.IdleTimeout = TimeSpan.FromMinutes(5);
            });

            services.AddRouting(cfg =>
            {
                cfg.LowercaseUrls = true;
            });

            services.AddDbContext<AurosDbContext>(cfg =>
            {
                cfg.UseSqlServer(configuration.GetConnectionString("cString"));
            });


            //.AddIdentity<AurosUser, AurosRole>()
            //.AddEntityFrameworkStores<AurosDbContext>();
            //services.Configure<IdentityOptions>(cfg =>
            //{
            //    cfg.Password.RequireDigit = false;
            //    cfg.Password.RequireUppercase = false;
            //    cfg.Password.RequireLowercase = false;
            //    cfg.Password.RequireNonAlphanumeric = false;
            //    //cfg.Password.RequiredUniqueChars = 1;
            //    cfg.Password.RequiredLength = 3;

            //    cfg.User.RequireUniqueEmail = true;
            //    //cfg.User.AllowedUserNameCharacters = "abcde...";

            //    cfg.Lockout.MaxFailedAccessAttempts = 3;
            //    cfg.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 1, 0);
            //});

            //services.ConfigureApplicationCookie(cfg =>
            //{
            //    cfg.LoginPath = "/signin.html";
            //    cfg.AccessDeniedPath = "/accesdenied.html";

            //    cfg.ExpireTimeSpan = new TimeSpan(0, 5, 0);
            //    cfg.Cookie.Name = "auros";
            //});
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseRequestLocalization(cfg =>
            {
                cfg.AddSupportedUICultures("az", "en");
                cfg.AddSupportedCultures("az", "en");
                cfg.RequestCultureProviders.Clear();
                cfg.RequestCultureProviders.Add(new CultureProvider());
            });

            app.UseEndpoints(endpoints =>
            {

                //endpoints.MapControllerRoute(
                //    name: "default-signin",
                //    pattern: "signin.html",
                //    defaults: new
                //    {
                //        area = "",
                //        controller="account",
                //        action="sigin"
                //    });

                //endpoints.MapControllerRoute(
                //    name: "default-accesdenied",
                //    pattern: "accesdenied.html",
                //    defaults: new
                //    {
                //        area = "",
                //        controller = "account",
                //        action = "accesdenied"
                //    });
                //endpoints.MapDefaultControllerRoute();

                endpoints.MapControllerRoute(
                    name: "admin-with-lang",
                    pattern: "{lang}/{admin:exists}/{controller=Dashboard}/{action=UserDashboard}/{id?}",
                    constraints: new
                    {
                        lang = "en|az"
                    }
                    );

                endpoints.MapControllerRoute("default-with-lang", "{lang}/{controller}/{action}/{id?}",
                    constraints: new
                    {
                        lang = "en|az"
                    },
                        defaults: new
                        {
                            controller = "home",
                            action = "index"
                        });

                endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}",
                    defaults: new
                    {
                        controller = "home",
                        action = "index"
                    });
            });
        }
    }
}
