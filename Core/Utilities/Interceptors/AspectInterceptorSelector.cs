using Castle.DynamicProxy;
using Core.Aspects.AutofacAspect.ExceptionLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public class AspectInterceptorSelector : IInterceptorSelector//Interface Castle.Dynamic.Proxy altında
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)//method info:reflection'dan gelir
        {
            var classAttributes = type.GetCustomAttributes<MethodInterceptionBaseAttribute>//Git class'ın attributelarını oku
                (true).ToList();
            var methodAttributes = type.GetMethod(method.Name)// Git methodun attribute'larını oku
                .GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttributes.AddRange(methodAttributes);//Onları listeye koy
            //classAttributes.Add(new ExceptionLogAspect(typeof(ErrorDatabaseLogger)));//!!! Otomatik olarak sistemdeki bütün metotları loga dahil et. Bağımlılık yaratıyor???

            return classAttributes.OrderBy(x => x.Priority).ToArray();//Öncelik değerine göre sırala(priority) ve dön
        }
    }
}
