using App.Core.Interfaces;
using App.Core.UseCases;
using App.Entities;
using App.Infrastructure.Data;
using AutoMapper;
using DemoApp.Web.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoApp.Web
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

            services.AddAutoMapper(typeof(MappingProfile));
            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true; /* Registrar un solo email con la cuenta*/
                cfg.Password.RequireDigit = false; /* El password solo requiere digitos*/
                cfg.Password.RequiredLength = 6; /*Longitud del password*/
                cfg.Password.RequiredUniqueChars = 0; /*El password solo requiere caracteres especiales*/
                cfg.Password.RequireLowercase = false; /*El passowrd solo requiere minusculas*/
                cfg.Password.RequireNonAlphanumeric = false; /*El passowrd solo requiere alfanumericos*/
                cfg.Password.RequireUppercase = false; /*El passowrd solo requiere mayusculas*/

            }).AddEntityFrameworkStores<DataContext>();

            services.AddDbContext<DataContext>(cfg =>
            {
                //cfg.UseMySql(this.Configuration.GetConnectionString("MySQLConnection"));
                cfg.UseSqlServer(this.Configuration.GetConnectionString("SQLServerConnection"));
            });

            services.AddScoped<IOperations<Product>, ManageOperations<Product>>();
            services.AddScoped<IRepository<Product>, GenericRepository<Product>>();

            services.AddTransient<Seeder>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddMvc().AddMvcOptions(options => options.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName == "Development")
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
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
