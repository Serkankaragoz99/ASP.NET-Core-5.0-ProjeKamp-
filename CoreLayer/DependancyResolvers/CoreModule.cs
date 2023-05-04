using CoreLayer.CrossCuttingConcerns.Caching.Microsoft;
using CoreLayer.CrossCuttingConcerns.Caching;
using CoreLayer.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer.Utilities.MailUtilities;
using CoreLayer.Utilities.CaptchaUtilities;

namespace CoreLayer.DependancyResolvers
{
    public class CoreModule: ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();

            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();

            serviceCollection.AddSingleton<IMailService, OutlookMailManager>();

            serviceCollection.AddSingleton<ICaptchaService, RecaptchaManager>();

            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
