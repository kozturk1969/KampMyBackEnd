using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            serviceCollection.AddMemoryCache(); //MomoryCacheManager da IMemoryCache _memoryCache; satırının çalışabilmesi  için yazıldı.
            serviceCollection.AddSingleton<ICacheManager, MomoryCacheManager>(); //bu da bizim cachemanagerimiz.
                                                                                 //ileride Redid ile çalışmak istenirse RedisCacheManager modülü yaılmalı MomoryCacheManager ile değiştirilmeşli
                                                                                 //Redis olursa serviceCollection.AddMemoryCache() satırına ihtiyaç kalmıyor
        }
    }
}
