using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyCompany.Service;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.EntityFramework;
using MyCompany.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace MyCompany
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {
            //Підключаємо Config із appsetting.json
            Configuration.Bind("Project", new Config());

            //Підключаємо потрібні сервіси
            services.AddTransient<ITextFieldRepository, EFTextFieldRepository>();
            services.AddTransient<IServiceItemRepository, EFServiceItemRepository>();
            services.AddTransient<DataManager>();

            //Підключаємо контекст Бази даних
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString)); //B appsettings змінити Connection string

            //Налаштовуємо identity систему
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //Налаштовуємо Authotication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });
            
            //Добавляємо підтримку контроллерів і їх Views
            services.AddControllersWithViews()
            // Виставляємо підтримку з ASP Net Core 3.0
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //Підключаємо підтримку статичних файлів
            app.UseStaticFiles();

            //Підключаємо систему маршрутизації
            app.UseRouting();

            //Підключаємо аутентифікацію і авторизацію
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //Реєструємо потрібні нам маршрути
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=home}/{action=Index}/{id?}");
            });
        }
    }
}
