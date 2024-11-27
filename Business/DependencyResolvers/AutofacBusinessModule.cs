using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Business.CrossCuttingConcerns;
using Castle.DynamicProxy;
using Core.Aspects.AutofacAspect.ExceptionLog;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers
{
    public class AutofacBusinessModule : Module// : importing on Autofac
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (false)//Neyi seçtiysem ona göre yapılandırma yazabilirim. Farklı veritabanları kullanılıyor olabilir.
            {

            }
            //Çeşitli build configleri yapabilirim.Development,stageing,production da farklı instance'lar verebilirim
            ///////Auth
            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();

            ///////Auth

            builder.RegisterType<AppointmentService>().As<IAppointmentService>().SingleInstance();
            builder.RegisterType<EfAppointmentDal>().As<IAppointmentDal>().SingleInstance();

            builder.RegisterType<ErrorDatabaseLogger>().As<IErrorDatabaseLogger>().SingleInstance();

            //base.Load(builder);
            ////////Interceptorların devreye gireceğini söyle: Business > DependencyResolvers > Autofac > AutofacBusinessModule 
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();//Çalışan uygulama içerisinde

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()//İmplemente edilmiş interface'leri bul
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()//Onlar için AspectInterceptorSelector'ı çağır. Bizim bütün sınıflarımız için önce bunu çalıştır. Çalıştırırken git bak bu adamın aspect'i varmı?
                }).SingleInstance();
            /////////////
        }
    }
}
//startup.cs'de varsayılan IoC yerine bunu kullanması gerektiğini söylemeyi unutma
//Ders 12 sonu Interceptors ve Validation Aspects, 17 Şubat Ders Sonu commiti
