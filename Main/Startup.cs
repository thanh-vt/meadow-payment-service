using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using MeadowPaymentService.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace MeadowPaymentService
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKeyResolver = (s, securityToken, identifier, parameters) =>
                    {
                        // get JsonWebKeySet from AWS
                        var res = new HttpClient().Send(new HttpRequestMessage(HttpMethod.Get,
                            new Uri("https://lemur-10.cloud-iam.com/auth/realms/buyer/protocol/openid-connect/certs")));
                        var json = res.Content.ReadFromJsonAsync<JsonWebKeySet>().Result;
                        if (json == null) return new List<SecurityKey>();
                        return json.Keys;
                        // var json = new WebClient().DownloadString("https://lemur-10.cloud-iam.com/auth/realms/buyer/protocol/openid-connect/certs");
                        // serialize the result
                        // var keys = JsonConvert.DeserializeObject<JsonWebKeySet>(json).Keys;
                        // cast the result to be the type expected by IssuerSigningKeyResolver
                        // return keys;
                    },

                    ValidIssuer = "https://lemur-10.cloud-iam.com/auth/realms/buyer",
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidAudience = "account",
                    ValidateAudience = true,
                    NameClaimType = "preferred_username"
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}