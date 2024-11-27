using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.AutofacAspect.Caching
{
    public class CacheRemoveAspect : MethodInterception//Veriyi manipüle eden tüm metotlarına uygula [CacheRemoveAspect(IAppointmentService.Get)] => IAppointmentService'deki bütün getleri silersin
    {
        private string _pattern;
        private ICacheManager _cacheManager;

        public CacheRemoveAspect(string pattern)
        {
            _pattern = pattern;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        }

        protected override void OnSuccess(IInvocation invocation)//Manipüle edilebilirse çalıştır
        {
            _cacheManager.RemoveByRegexp(_pattern);
        }
    }
}
