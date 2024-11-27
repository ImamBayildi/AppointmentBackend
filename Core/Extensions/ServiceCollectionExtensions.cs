using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ServiceCollectionExtensions//Eklenti yazabilmek için statik olmalı
    {
        public static IServiceCollection AddDependencyResolvers(this IServiceCollection serviceCollection, ICoreModule[] modules)//this: neyi genişletmek istiyorum? bu bir parametre değil
        {
            foreach (var module in modules)
            {
                module.Load(serviceCollection);
            }

            return ServiceTool.Create(serviceCollection);
        }//Ekleyeceğimiz bütün injection'ları bir arada toplayabileceğimiz bir yapı. İstediğim kadar modül ekleyebilirim
    }
}
