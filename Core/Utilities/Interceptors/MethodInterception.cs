using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Interceptors
{
    public abstract class MethodInterception : MethodInterceptionBaseAttribute//Sen bir attribute'sun arkadaşım
    {//Örnek: Aspect > Autofac > ValidationAspect içerisinde bu sınıfı kalıtım alan ve bir metodu override eden sınıf var
        protected virtual void OnBefore(IInvocation invocation) { }//CastleDynamicProxy
        protected virtual void OnAfter(IInvocation invocation) { }//Invocation çalıştırmak istediğin method (Add,Delete gibi business method)
        protected virtual void OnException(IInvocation invocation, System.Exception e) { }//Hangi methodu doldurursan (override) o çalışır. Base class. virtual yerine varsayılan davranışı yazabilir miyim?
        protected virtual void OnSuccess(IInvocation invocation) { }
        public override void Intercept(IInvocation invocation)
        {
            var isSuccess = true;
            OnBefore(invocation);//O metodu nerede çalıştırmak itersin
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation, e);//hata aldığında
                throw;
            }
            finally
            {
                if (isSuccess)
                {
                    OnSuccess(invocation);//başarılı olduğunda
                }
            }
            OnAfter(invocation);//methoddan sonra
        }
    }
}
