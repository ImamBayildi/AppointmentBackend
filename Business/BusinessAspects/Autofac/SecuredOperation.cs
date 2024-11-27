using Business.Constants;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;

using Core.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Business.BusinessAspects.Autofac
{
    //JWT Method yetklendirme Aspect'i
    public class SecuredOperationAspect : MethodInterception
    {
        private string[] _roles;
        private IHttpContextAccessor _httpContextAccessor;//Microsoft.AsptNetCore.Http.Abstractions

        public SecuredOperationAspect(string roles)
        {
            _roles = roles.Split(',');// Attribute'te tek string geçebiliyoruz. Virgül ile ayırarak array haline getir
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();// : Microsoft.Extensions.DependencyInjection
                                                                                                  //ServiceTool, dependency'leri yakalayabilmek için yazılmış. Aspect zincir içerisinde değil.
                                                                                                  //Autofac'te yaptığımız injection değerlerini alır

        }

        protected override void OnBefore(IInvocation invocation)
        {
            var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();//:Core.Extensions
            //Kullanıcının claimRollerini bul
            foreach (var role in _roles)//onları gez
            {
                if (roleClaims.Contains(role))//ilgili rol varsa return et (bunu durdur ve invocation'ı çalıştırmayadevam et)
                {
                    return;
                }
            }
            throw new Exception(Messages.AuthorizationDenied);//Yoksa hata ver
        }
    }
}

//EXCEPTİON LOG'DA DA BU SERVİCEtOOL KULLANILACAK