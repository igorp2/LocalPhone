using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Logging;
using System.Text;
using Microsoft.EntityFrameworkCore;
using LocalPhoneApi.Data;
using LocalPhoneDomain.Services;
using LocalPhoneApi.Services;
using System.Linq;
using LocalPhoneDomain.Models;

namespace LocalPhoneApi
{
    public class Startup
    {
        readonly string DefaulPolicyName = "Policy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true;

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://localhost:44334/",
                    ValidAudience = "https://localhost:44334/",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("af0e52040f694981a3e212b5b795d52d2d86fd10399742cbbb7882600abf974082600abf9740")),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.DEFAULT_ADMIN_POLICY_NAME, Policies.AdminPolicy.GetAuthorizationPolicy());
                config.AddPolicy(Policies.DEFAULT_SYSTEM_POLICY_NAME, Policies.SystemPolicy.GetAuthorizationPolicy());
                config.AddPolicy(Policies.DEFAULT_USER_POLICY_NAME, Policies.UserPolicy.GetAuthorizationPolicy());
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            }).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => new BadRequestObjectResult(new
                {
                    errors = actionContext.ModelState.Values
                        .SelectMany(value => value.Errors)
                        .Select(error => error.ErrorMessage)
                });
            });

            services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Development:LocalPhoneAdminContextConnection"]));
            //services.AddDbContext<ApiDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("LocalPhoneAdminContextConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LocalPhoneApi", Version = "v1" });
            });

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
        }

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
            app.UseMiddleware(typeof(CorsMiddleware));
            app.UseCors(DefaulPolicyName);
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllers().RequireCors(DefaulPolicyName);
                endpoints.MapDefaultControllerRoute().RequireAuthorization(DefaulPolicyName);
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LocalPhoneApi v1");
            });
        }
    }

    public class CorsMiddleware
    {
        private readonly RequestDelegate _next;

        public CorsMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                SetDefaultHttpResponseHeadersForAGivenContext(context);
                HandleHttpRequest(context);
                await _next(context);
            }
            catch
            {
            }
        }

        private static void SetDefaultHttpResponseHeadersForAGivenContext(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            context.Response.Headers.Add("Access-Control-Allow-Headers", "Authorization, Origin, X-Requested-With, Content-Type, Accept");
            context.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,DELETE,OPTIONS");
            context.Response.Headers.Add("Content-Type", "application/json");
            context.Response.Headers.Add("Access-Control-Max-Age", "3600");
        }

        private static async void HandleHttpRequest(HttpContext context)
        {
            if (context.Request.Method == "OPTIONS")
            {
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                await context.Response.WriteAsync(string.Empty);
            }
        }
    }

    public class Policy
    {
        protected readonly string policyName;

        public Policy(string policyName)
        {
            this.policyName = policyName;
        }

        public string GetPolicyName()
        {
            return policyName;
        }

        public virtual AuthorizationPolicy GetAuthorizationPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser().RequireRole(policyName).Build();
        }
    }

    public class Policies
    {
        public const string DEFAULT_ADMIN_POLICY_NAME = "admin";
        public const string DEFAULT_USER_POLICY_NAME = "usuario";
        public const string DEFAULT_SYSTEM_POLICY_NAME = "sistema";

        public static readonly Policy AdminPolicy = new(DEFAULT_ADMIN_POLICY_NAME);
        public static readonly Policy UserPolicy = new(DEFAULT_USER_POLICY_NAME);
        public static readonly Policy SystemPolicy = new(DEFAULT_SYSTEM_POLICY_NAME);
    }

}
