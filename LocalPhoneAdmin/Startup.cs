using LocalPhoneAdmin.Areas;
using LocalPhoneAdmin.Data;
using LocalPhoneApi.Data;
using LocalPhoneApi.Services;
using LocalPhoneDomain.Areas.Identity.Data;
using LocalPhoneDomain.Models;
using LocalPhoneDomain.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace LocalPhoneAdmin
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
            services.AddControllersWithViews();

            services.AddRazorPages();

            //services.AddScoped(typeof(IAdminRepository<>), typeof(AdminRepository<>));

            services.AddDefaultIdentity<UserModel>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApiDbContext>();

            //services.AddDbContext<ApiDbContext>(options =>
            //    options.UseSqlServer(Configuration["ConnectionStrings:Development:LocalPhoneAdminContextConnection"]));
            services.AddDbContext<ApiDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("LocalPhoneAdminContextConnection")));

            
            services.AddDbContext<LocalPhoneAdminContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("LocalPhoneAdminContextConnection")));    

            services.AddScoped(typeof(IApiRepository<>), typeof(ApiRepository<>));
            services.AddScoped<IApiRepository<CustomerModel, string>, CustomerRepository>();
            services.AddScoped(typeof(INonUpdatableItemsRepository<SmsRelatedInformationModel>), typeof(SmsRelatedInformationRepository));

            //Services
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IAvailableNumberService, AvailableNumberService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<INumberService, NumberService>();
            services.AddScoped<IVoxboneService, VoxboneService>();
            services.AddScoped<LocalPhoneAdminContext>();
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
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "City",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "Country",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "Status",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "State",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "Gender",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "Customer",
                   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                   name: "default",
                   pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
