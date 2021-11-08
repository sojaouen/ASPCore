using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebASPCorePremiereApp.Models;

namespace WebASPCorePremiereApp
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
            services.AddControllersWithViews();
            services.AddDbContext<LivreDbContext>
                (options => options.UseSqlServer(Configuration.GetConnectionString("LivreDbConnection")));
            // Injecter le service d'authentification
            // Ajouter la gestion des pages --> introduction des Pages (cshtml et le cs en mode behind)
            services.AddRazorPages();
            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<LivreDbContext>();

            // 1- Ajouter les packages NuGet
            // 2- Modifier le fichier Startup.cs: injecter le service et ajouter le middleware dans le pipeline
            // 3- Ajouter le middleware d'authentification dans le pipeline
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
            // middleware : ensemble de composants
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            // Ajouter le middleware d'authentification dans le pipeline

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Livres}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
