using System;
using Codescovery.JsonServices.Interfaces;
using Codescovery.JsonServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Codescovery.JsonServices.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddJsonServices(this IServiceCollection services,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            return serviceLifetime switch
            {
                ServiceLifetime.Singleton => services.AddSingleton<IJsonService, JsonService>(),
                ServiceLifetime.Scoped => services.AddScoped<IJsonService, JsonService>(),
                ServiceLifetime.Transient => services.AddTransient<IJsonService, JsonService>(),
                _ => throw new ArgumentOutOfRangeException(nameof(serviceLifetime), serviceLifetime, null)
            };
        }
    }
}
