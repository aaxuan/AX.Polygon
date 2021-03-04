using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AX.Polygon.Util
{
    public static class IOCManager
    {
        private static IServiceCollection Services { get; set; }
        private static IServiceProvider ServiceProvider { get; set; }
        public static IHttpContextAccessor HttpContextAccessor { get { return ServiceProvider.GetRequiredService<IHttpContextAccessor>(); } }

        public static void BindServiceCollection(this IServiceCollection services)
        {
            Services = services;
        }

        public static void BindServiceProvider(this IApplicationBuilder app)
        {
            ServiceProvider = app.ApplicationServices;
            //HttpContextAccessor = ServiceProvider.GetRequiredService<IHttpContextAccessor>();
        }

        public static T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        public static T GetScopeService<T>()
        {
            using (var serviceScope = ServiceProvider.CreateScope())
            {
                return serviceScope.ServiceProvider.GetService<T>();
            }
        }

        /// <summary>
        /// 框架内部注入
        /// </summary>
        public static void InitAXPolygon(this IServiceCollection services)
        {
            Services.AddSingleton<IDGenerator, GuidGenerator>();
        }
    }
}