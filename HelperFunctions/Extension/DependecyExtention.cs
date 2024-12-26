using Autofac;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelperFunctions.Extension
{
    public static class DependecyExtention 
    {
        public static (A, B) InitializeDependency<A, B>(this IContainer container) 
        {
           var container1 = container.Resolve<A>();
           var container2 =  container.Resolve<B>();
           return (container1, container2);
        }

        public static (A, B, C ) InitializeDependency<A, B, C> (this IContainer newcontainer)
        {
            (var container1, var container2) = InitializeDependency<A, B>(newcontainer);
            var container3 = newcontainer.Resolve<C>();
            return (container1, container2, container3);
        }
    }
}
