using System;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace Futbal.Mng.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                // uncomment to write to Azure diagnostics stream
                //.WriteTo.File(
                //    @"D:\home\LogFiles\Application\identityserver.txt",
                //    fileSizeLimitBytes: 1_000_000,
                //    rollOnFileSizeLimit: true,
                //    shared: true,
                //    flushToDiskInterval: TimeSpan.FromSeconds(1))
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            try
            {
                var host = CreateWebHostBuilder(args).Build();
                Log.Information("Starting host..."); 
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseKestrel(options =>
                {
                    options.Listen(IPAddress.Any, 5001, cfg => {
                        cfg.Protocols = HttpProtocols.Http1AndHttp2;
                        // cfg.UseHttps();
                    }); //HTTP port
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
