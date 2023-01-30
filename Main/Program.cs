using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MeadowPaymentService
{
    public class Program
    {
        
        public static IServiceProvider ServiceProvider { get; private set; }
        public static void Main(string[] args)
        {
            IHost build = CreateHostBuilder(args).Build();
            ServiceProvider = build.Services;
            build.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
