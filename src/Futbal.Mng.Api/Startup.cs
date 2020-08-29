using System;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using futbal.mng.auth_identity.Extensions;
using Futbal.Mng.Infrastructure.EF;
using Futbal.Mng.Infrastructure.EventBus;
using Futbal.Mng.Infrastructure.Interfaces.EventBus;
using Futbal.Mng.Infrastructure.IoC;
using Futbal.Mng.Webapi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Futbal.Mng.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            services.AddHealthChecks();
            //services.AddRabbit();
            //.RegisterEventBus();
            services.AddCors(options =>
        {
            options.AddPolicy("default",
            cfg =>
            {
                cfg.WithOrigins("http://localhost:4200",
                                    "https://futbalmng.serveo.net");
                cfg.AllowAnyMethod();
                cfg.AllowAnyHeader();
            });
        });
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<FutbalMngContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("FutbalMngDatabase"),
                    m => m.MigrationsAssembly("Futbal.Mng.Infrastructure")));
            services.AddMvc(mvcOptions =>
            {
                mvcOptions.EnableEndpointRouting = false;
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddControllersAsServices();
            //services.AddGrpc();


            var builder = new ContainerBuilder();

            builder.Populate(services);

            builder.Register<IRabbitMqPersistentConnection>(options =>
            {
                var factory = new ConnectionFactory
                {
                    HostName = "localhost",
                    Password = "guest",
                    UserName = "futbal-manager",
                    Port = 5672
                };
                return new DefaultRabbitMqPersistentConnection(factory);
            }).SingleInstance();

            builder.Register<IEventBus>(options =>
            {
                var persistentConnection = options.Resolve<IRabbitMqPersistentConnection>();
                var lifeTimeScope = options.Resolve<ILifetimeScope>();
                Console.WriteLine($"is connected: {persistentConnection.IsConnected}");
                return new EventBusRabbitMq(persistentConnection, lifeTimeScope);
            }).SingleInstance();

            builder.RegisterModule<InfrastructureModule>();
            ApplicationContainer = builder.Build();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            if (env.EnvironmentName == "Development")
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("default");
            app.UseRouting();
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe();
            app.UseMvc();
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGrpcService<GameResponse>();
                endpoints.MapHealthChecks("/health");

            });
        }
    }
}
