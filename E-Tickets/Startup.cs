using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using E_Tickets.Models.Cart;
using E_Tickets.Models.ModelsDB;
using E_Tickets.Controllers;

namespace E_Tickets
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<eTicketDbContext>(options =>
                                                        options.UseSqlServer(
                                                            Configuration.GetConnectionString("defaultConnection")
                                                                            ));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>{  options.User.RequireUniqueEmail = true;
                                                                             options.Lockout.MaxFailedAccessAttempts = 5;
                                                                         }).AddEntityFrameworkStores<eTicketDbContext>()
                                                                           .AddDefaultUI()
                                                                           .AddDefaultTokenProviders();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton(sc => ShoppingCart.GetShoppingCart(sc));

            services.AddSession();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
