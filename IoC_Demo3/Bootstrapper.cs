using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Web.Http;
using Unity.WebApi;

namespace IoC_Demo3
{
    public static class Bootstrapper
    {
        public static void Initialize()
        {
            IUnityContainer container = new UnityContainer();


            //loading configuration
            container.LoadConfiguration();

            //set service locator
            UnityServiceLocator locator = new UnityServiceLocator(container);
            ServiceLocator.SetLocatorProvider(()=>locator);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}