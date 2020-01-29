using System.Collections.Generic;
using Autofac;
using AutoMapper;
using Futbal.Mng.Infrastructure.CommandHandler;
using Futbal.Mng.Infrastructure.EF;
using Futbal.Mng.Infrastructure.Interfaces.CommandHandler;
using Futbal.Mng.Infrastructure.Interfaces.EventHandler;
using Futbal.Mng.Infrastructure.Interfaces.QueryHandler;
using Futbal.Mng.Infrastructure.QueryHandler;

namespace Futbal.Mng.Infrastructure.IoC
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = this.GetType().Assembly;

        builder.RegisterAssemblyTypes(typeof(InfrastructureModule).Assembly).As<Profile>();

        builder.Register(context => new MapperConfiguration(cfg =>
            {
                foreach (var profile in context.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            }))
            .AsSelf()
            .SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();

            builder.RegisterType<UnitOfWork>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();
        
            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IHandleCommand<>))
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IHandleQuery<,>))
                .InstancePerLifetimeScope();

            builder.RegisterType<CommandBus>()
                .As<ICommandBus>()
                .InstancePerLifetimeScope();

            builder.RegisterType<QueryBus>()
                .As<IQueryBus>()
                .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IHandleEvent<>))
                .InstancePerMatchingLifetimeScope();
        }
    }
}