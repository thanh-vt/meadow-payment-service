using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MeadowPaymentService.Constant;
using MeadowPaymentService.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace MeadowPaymentService
{
    public class Startup
    {
        private readonly ILogger _logger;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var loggerFactory = LoggerFactory.Create(builder => { builder.AddConsole(); });

            _logger = loggerFactory.CreateLogger<Startup>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString(AppConstant.ConnectionStringKey.PostgresqlUrl)));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();

            var redisConnStr = Configuration.GetConnectionString(AppConstant.ConnectionStringKey.RedisUrl);
            var multiplexer = ConnectionMultiplexer.Connect(redisConnStr ?? throw new InvalidOperationException("Redis connection string not found"));
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);
            
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKeyResolver = (_, _, _, _) =>
                        {
                            if (Program.ServiceProvider.GetService(typeof(IConnectionMultiplexer)) is not IConnectionMultiplexer connectionMultiplexer)
                            {
                                return new List<SecurityKey>();
                            }

                            var db = connectionMultiplexer.GetDatabase();
                            var jwksStr = db.StringGet(AppConstant.CacheKey.JwksKey);
                            
                            if (!jwksStr.HasValue)
                            {
                                // get JsonWebKeySet from AWS
                                var res = new HttpClient().Send(new HttpRequestMessage(HttpMethod.Get,
                                    Configuration.GetConnectionString(AppConstant.ConnectionStringKey.JwksUrl)));
                                // var json = res.Content.ReadFromJsonAsync<JsonWebKeySet>().Result;
                                jwksStr = res.Content.ReadAsStringAsync().Result;
                                db.StringSet(AppConstant.CacheKey.JwksKey, jwksStr, TimeSpan.FromDays(1));
                            }
                            var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(jwksStr)?.Keys;
                            return keys;
                            // if (json == null) return new List<SecurityKey>();
                            // return json.Keys;
                            // var json = new WebClient().DownloadString("https://lemur-10.cloud-iam.com/auth/realms/buyer/protocol/openid-connect/certs");
                            // serialize the result
                            // var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;
                            // cast the result to be the type expected by IssuerSigningKeyResolver
                            // return keys;
                        },
                        ValidIssuer = Configuration.GetConnectionString(AppConstant.ConnectionStringKey.JwtIssuerUrl),
                        ValidateIssuerSigningKey = false,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidAudience = AppConstant.JwtClaim.Audience,
                        ValidateAudience = true,
                        NameClaimType = AppConstant.JwtClaim.Username
                    };
                });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection();
            app.UseStaticFiles();

            // app.UsePathBase(AppConstant.BasePath);

            app.UseRouting();
            var routes = app.ApplicationServices.GetRequiredService<IActionDescriptorCollectionProvider>()
                .ActionDescriptors
                .Items;
            foreach (var route in routes)
            {
                if (route.AttributeRouteInfo != null)
                {
                    _logger.LogDebug("{Template}", route.AttributeRouteInfo.Template);
                }
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints
                    .MapGroup(AppConstant.BasePath)
                    .MapControllers() // attribute routing
                    // .MapControllerRoute( // conventional routing
                    //     name: "default",
                    //     pattern: "{controller=Home}/{action=Index}/{id?}")
                    ;
            });
        }
    }
    
}