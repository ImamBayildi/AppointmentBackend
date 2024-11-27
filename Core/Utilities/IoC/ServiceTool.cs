using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services)//.Net'in serviceCollectionlarını al ve build et. Bu kod injection'ları oluşturabilmek için.
                                                                            //İstediğin herhangi bir servisteki karşılığını bu tool ile alabilirim.
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
