using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Owin;
using ResourceReservation.Abstractions;
using ResourceReservation.Logging;
using ResourceReservationApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace ResourceReservationApi
{
    public partial class Startup
    {
        public void Configuration (IAppBuilder appBuilder)
        {

            // In OWIN you create your own HttpConfiguration rather than re-using the GlobalConfiguration.
            var config = new HttpConfiguration();

            // Enable CORS
            // Look up "Enable CORS on Owin Web API" to see what NuGet to install
            //config.EnableCors(new EnableCorsAttribute("*","*","*"))

            // ROUTING
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", "{controller}/{id}",
                new { controller = "Value", id = RouteParameter.Optional }
                );

            // HOOK UP SWASHBUCKLE SWAGGER HERE (alternative is NSwag)
            // Read documenation specific to Owin i.e. Only Swashbuckle.Core package is needed

            // AUTOFAC
            var containerBuilder = new ContainerBuilder();

            // Register Http Request message - what is this?
            containerBuilder.RegisterHttpRequestMessage(config);

            // Register controllers all at once using assembly scanning...
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            // Register Assembly modules - what is this?
            containerBuilder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());

            // OPTIONAL - Register the filter provider if you have custom filters that need DI.
            // Also hook the filters up to controllers.
            //containerBuilder.RegisterWebApiFilterProvider(config);
            //containerBuilder.RegisterType<CustomActionFilter>()
            //    .AsWebApiActionFilterFor<TestController>()
            //    .InstancePerRequest();

            // Register my version of ILogger that wraps Serilog, its ILogger and sinks
            containerBuilder.Register(c => Logger.Instance).As<ILogger>().SingleInstance();

            // Entity Framework
            // how to register this? Use Repository pattern and/or Unit of Work pattern?
            // As in Serilog case, should EF be wrapped within Repository?

            // Autofac will add middleware to IAppBuilder in the order registered.
            // The middleware will execute in the order added to IAppBuilder.
            //containerBuilder.RegisterType<FirstMiddleware>().InstancePerRequest();
            //containerBuilder.RegisterType<SecondMiddleware>().InstancePerRequest();


            // Create and assign a dependency resolver for Web API to use.
            var container = containerBuilder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // The Autofac middleware should be the first middleware added to the IAppBuilder.
            // If you "UseAutofacMiddleware" then all of the middleware in the container
            // will be injected into the pipeline right after the Autofac lifetime scope
            // is created/injected.
            //
            // Alternatively, you can control when container-based
            // middleware is used by using "UseAutofacLifetimeScopeInjector" along with
            // "UseMiddlewareFromContainer". As long as the lifetime scope injector
            // comes first, everything is good.
            appBuilder.UseAutofacMiddleware(container);
            // Again, the alternative to "UseAutofacMiddleware" is something like this:
            // app.UseAutofacLifetimeScopeInjector(container);
            // app.UseMiddlewareFromContainer<FirstMiddleware>();
            // app.UseMiddlewareFromContainer<SecondMiddleware>();

            // Make sure the Autofac lifetime scope is passed to Web API.
            // Sam: is this line necessary?
            appBuilder.UseAutofacWebApi(config);


            appBuilder.UseWebApi(config);
        }
    }
}