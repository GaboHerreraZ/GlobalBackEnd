using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace App.Service.Rest.App_Start
{
    public class UnityResolver : IDependencyResolver
    {
        protected IUnityContainer _container;

        public UnityResolver(IUnityContainer container)
        {
            if(container == null)
            {
                throw new ArgumentNullException("Contenedor vacio");
            }

            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            var container = _container.CreateChildContainer();
            return new UnityResolver(container);
        }

        public void Dispose()
        {
            _container.Dispose();
        }


        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch(ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }
    }
}