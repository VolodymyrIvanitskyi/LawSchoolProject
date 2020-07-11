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
            //ϳ�������� Config �� appsetting.json
            Configuration.Bind("Project", new Config());

            //ϳ�������� ������ ������
            services.AddTransient<ITextFieldRepository, EFTextFieldRepository>();
            services.AddTransient<IServiceItemRepository, EFServiceItemRepository>();
            services.AddTransient<DataManager>();

            //ϳ�������� �������� ���� �����
            services.AddDbContext<AppDbContext>(x => x.UseSqlServer(Config.ConnectionString)); //B appsettings ������ Connection string

            //����������� identity �������
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            //����������� Authotication cookie
            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "myCompanyAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/account/login";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });
            
            //���������� �������� ����������� � �� Views
            services.AddControllersWithViews()
            // ����������� �������� � ASP Net Core 3.0
            .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //ϳ�������� �������� ��������� �����
            app.UseStaticFiles();

            //ϳ�������� ������� �������������
            app.UseRouting();

            //ϳ�������� �������������� � �����������
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();

            //�������� ������ ��� ��������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=home}/{action=Index}/{id?}");
            });
        }
    }
}
