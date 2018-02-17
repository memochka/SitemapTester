[assembly: WebActivator.PostApplicationStartMethod(typeof(SitemapTester.WebUI.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace SitemapTester.WebUI.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;

    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    using SitemapTester.Domain.Abstract;
    using SitemapTester.Domain.Concrete;
    using SitemapTester.WebUI.Abstract;
    using SitemapTester.WebUI.Infrastructure;

    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IUrlsParse, UrlsParse>(Lifestyle.Transient);
            container.Register<IMeasureDomain, MeasureDomain>(Lifestyle.Transient);
            container.Register<ISaveMeasurementsToDb, SaveMeasurementsToDb>(Lifestyle.Scoped);
            container.Register<IMeasurementRepository, MeasurementRepository>(Lifestyle.Transient);
        }
    }
}