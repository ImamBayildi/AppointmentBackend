using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple = true,Inherited = true)]//Classlarda, Methodlarda birden fazla kere kullanılabilir. Kalıtım alan sınıfın Attribute olmasını sağlar
    public abstract class MethodInterceptionBaseAttribute:Attribute,IInterceptor//CastleDynamicProxy
    {
        public int Priority { get; set; }//Öncelik demek, sıralama yapmak için (önce loglama sonra authorization gibi gibi)

        public virtual void Intercept(IInvocation invocation)
        {

        }
    }
}
