using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kritner.TodoBackend.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // takes injected env variable "PORT" from heroku and maps it to the app
                    webBuilder.UseUrls($"http://0.0.0.0:{Environment.GetEnvironmentVariable("PORT")}");
                    webBuilder.UseStartup<Startup>();
                });
    }
}