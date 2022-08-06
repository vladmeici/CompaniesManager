using CompaniesManager.Models;
using CompaniesManager.Services.Comparers;
using CompaniesManager.Services.Delimiters;
using CompaniesManager.Services.FileExtractors;
using CompaniesManager.Services.Interfaces;
using CompaniesManager.Services.Sorters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace CompaniesManager
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["MilestoneConnectionString"]));

            services.AddSingleton<IDelimiter, CommaDelimiter>();
            services.AddSingleton<IDelimiter, HashDelimiter>();
            services.AddSingleton<IDelimiter, HyphenDelimiter>();

            services.AddSingleton<ICompaniesExtractor, TextFileExtractor>();
            services.AddSingleton<ICompaniesExtractor, ExcelFileExtractor>();

            services.AddSingleton<IComparer<Company>, CompanyNameComparer>();
            services.AddSingleton<IComparer<Company>, ContactNameComparer>();
            services.AddSingleton<IComparer<Company>, YearsAndNameComparer>();

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Companies}/{action=Index}/{id?}");
            });
        }
    }
}
