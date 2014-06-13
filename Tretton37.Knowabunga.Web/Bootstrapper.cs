using Autofac;
using Nancy.Bootstrappers.Autofac;
using Nancy.Conventions;
using Nancy.Session;
using Raven.Abstractions.Logging;
using Raven.Client;
using Raven.Client.Embedded;
using Serilog;
using Tretton37.Knowabunga.Core;

namespace Tretton37.Knowabunga.Web
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        protected override ILifetimeScope GetApplicationContainer()
        {
            var builder = new ContainerBuilder();
            //builder.RegisterType<InMemoryHackerService>().As<IHackerService>().SingleInstance();
            //LoggingHackerService

            builder.RegisterInstance(new LoggerConfiguration().WriteTo.Trace().CreateLogger())
                .As<ILogger>()
                .SingleInstance();

            builder.Register(c => new LoggingHackerService(new RavenHackerService(c.Resolve<IDocumentSession>()), c.Resolve<ILogger>()))
                .As<IHackerService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<PairGenerator>().As<IPairGenerator>().InstancePerLifetimeScope();
            builder.RegisterInstance(new EmbeddableDocumentStore().Initialize()).As<IDocumentStore>().SingleInstance();
            builder.Register(ctx => ctx.Resolve<IDocumentStore>().OpenSession()).InstancePerLifetimeScope();


            return builder.Build();
        }

        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("scripts"));
            nancyConventions.StaticContentsConventions.Add(StaticContentConventionBuilder.AddDirectory("fonts"));
            base.ConfigureConventions(nancyConventions);
        }
    }
}