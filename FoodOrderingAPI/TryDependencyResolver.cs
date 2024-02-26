using FoodOrderingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;

namespace FoodOrderingAPI
{
    public class TryDependencyResolver : IDependencyResolver
    {
        protected IUnityContainer container;

        public TryDependencyResolver()
        { 
            container = new UnityContainer();
            container.RegisterType<IFoodRepository, FoodRepository>();
            container.RegisterType<ICartRepository, CartRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
        }
        public IDependencyScope BeginScope()
        {
            return new TryDependencyResolver();
        }

        public void Dispose()
        {
            container.Dispose();
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }
    }
}