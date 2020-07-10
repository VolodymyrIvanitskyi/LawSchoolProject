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

            app.UseRouting();

            //ϳ�������� �������� ��������� �����
            app.UseStaticFiles();

            //�������� ������ ��� ��������
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=home}/{action=Index}/{id?}");
            });
        }
    }
}
