using System.Collections.Generic;
using Autofac;
using Autofac.Features.Variance;
using AutoMapper;
using MediatR;

namespace Gossip.Web
{
    public static class ContainerBuilderExtensions
    {
        public static ContainerBuilder SetUpMediator(this ContainerBuilder builder)
        {
            builder.RegisterSource(new ContravariantRegistrationSource());

            builder.RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder
                .Register<SingleInstanceFactory>(ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t =>
                    {
                        object o;
                        return c.TryResolve(t, out o) ? o : null;
                    };
                })
                .InstancePerLifetimeScope();

            builder
                .Register<MultiInstanceFactory>(ctx =>
                {
                    var c = ctx.Resolve<IComponentContext>();
                    return t => (IEnumerable<object>) c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
                })
                .InstancePerLifetimeScope();

            return builder;
        }

        public static ContainerBuilder SetUpAutoMapper(this ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(ContainerBuilderExtensions).Assembly)
                .AssignableTo<Profile>()
                .As<Profile>();

            builder.Register(context =>
                {
                    var profiles = context.Resolve<IEnumerable<Profile>>();

                    var config = new MapperConfiguration(x =>
                    {
                        foreach (var profile in profiles)
                        {
                            x.AddProfile(profile);
                        }
                    });

                    return config;
                }).SingleInstance()
                .AutoActivate()
                .AsSelf();

            builder.Register(tempContext =>
            {
                var ctx = tempContext.Resolve<IComponentContext>();
                var config = ctx.Resolve<MapperConfiguration>();

                return config.CreateMapper();
            }).As<IMapper>();

            return builder;
        }
    }
}