using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Futbal.Mng.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 5000); //HTTP port
                                                         //options.Listen(IPAddress.Loopback, 6010, cfg => cfg.UseHttps()); //HTTPS port
                })
                .ConfigureAppConfiguration((WebHostBuilderContext ctx, IConfigurationBuilder builder) =>
                {
                    builder.SetBasePath(ctx.HostingEnvironment.ContentRootPath)
                    .AddJsonFile("./appsettings.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"./appsettings.{ctx.HostingEnvironment.EnvironmentName}.json", optional: false)
                    .AddEnvironmentVariables();
                })
                .UseStartup<Startup>();
    }
}
