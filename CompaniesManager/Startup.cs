using CompaniesManager.Models;
using CompaniesManager.Services.Sorters;
using CompaniesManager.Services.Delimiters;
using CompaniesManager.Services.FileExtractors;
using CompaniesManager.Services.Interfaces;
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

            services.AddSingleton<IDelimiter, CommaDelimiter>(delimiter => new CommaDelimiter(5));
            services.AddSingleton<IDelimiter, HashDelimiter>(delimiter => new HashDelimiter(4));
            services.AddSingleton<IDelimiter, HyphenDelimiter>(delimiter => new HyphenDelimiter(6));

            services.AddSingleton<ICompaniesExtractor, TextFileExtractor>();
            services.AddSingleton<ICompaniesExtractor, ExcelFileExtractor>();

            services.AddSingleton<IComparer<Company>, CompanyNameSorter>();
            services.AddSingleton<IComparer<Company>, ContactNameSorter>();
            services.AddSingleton<IComparer<Company>, YearsAndNameSorter>();

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
                app.UseExceptionHandler("/Error");
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
