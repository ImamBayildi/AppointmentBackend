using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.AutofacAspect.Caching
{
    public class CacheAspect : MethodInterception//OnBefore yerine Intercept ile örnek
    {
        private int _duration;
        private ICacheManager _cacheManager;

        public CacheAspect(int duration = 60)//default 60 minute
        {
            _duration = duration;
            _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();//ServiceTool ile kullandığım cache manager'ı al :Microsoft.Extensions.DependencyInjection;
        }

        public override void Intercept(IInvocation invocation)
        {
            //                              ReflectedTyp.FullNamee=namespace+ServiceName(ClassName)     . MethodAdı     çıktı => Appointment.Business.IAppointmentService.Add  gibi
            var methodName = string.Format($"{invocation.Method.ReflectedType.FullName}.{invocation.Method.Name}");//sınıfın ve metodun adını bul
            var arguments = invocation.Arguments.ToList();//Metodun parametrelerini listeye çevir(varsa)
            //                             argument.Select:parametreleri listeye çevir  ?varsa bunu ?? yoksa bunu ekle
            var key = $"{methodName}({string.Join(",", arguments.Select(x => x?.ToString() ?? "<Null>"))})";//Metodun parametreleri varsa onu key(.Add(p)) içine ekle (parametreler için araya , koyarak ekle)
            if (_cacheManager.IsAdd(key))//Bellekte var mı? BreakPoint noktası
            {
                invocation.ReturnValue = _cacheManager.Get(key);//Metodda direk return et (çalıştırmadan)
                return;
            }
            invocation.Proceed();//Yoksa metodu çalıştır
            _cacheManager.Add(key, invocation.ReturnValue, _duration); //Metod veriyi getirdikten sonra cache'e ekle
        }
    }
}
//Soru : GetAll dedikten sonra belleğe eklendi. Sonrasında GetById(1) yöntemiyle veri getirilmek istenirse, GettAll metodu ile keylenmiş bellekteki verinin 1. parametresini alabilir miyiz?