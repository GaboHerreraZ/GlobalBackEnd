using App.Service.Rest.App_Start;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(InjectorInitializer), "Initialize")]


namespace App.Service.Rest.App_Start
{
    using App.Business;
    using System.Web.Http;
    using Unity;
    using App.Interface.Business;
    using Unity.Lifetime;
    public class InjectorInitializer
    {
        public static void Initialize()
        {
            var container = new UnityContainer();

            //Initialize container, register types of interfaces with services
            InitializeContainer(container);

            //Configure Dependency Injection Resolver
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);

        }

        private static void InitializeContainer(UnityContainer container)
        {
            container.RegisterType<ITransaction, TransactionBusiness>(new HierarchicalLifetimeManager());
            container.RegisterType<IRate, RateBusiness>(new HierarchicalLifetimeManager());

        }
    }
}