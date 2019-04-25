using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyERP.Web.Host.Middleware
{
    /// <summary>
    /// 请求路由中间件
    /// </summary>
    public class RequestRouteMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// 不需要重定义路径的请求前缀，例如api请求，swagger请求，静态资源请求，这些都不需要重定向，只有前端页面路由需要重定向
        /// </summary>
        private static readonly string[] s_directPathPrefixs = { "/swagger", "/api", "/favicon.ico", "/assets", "/download", "/account", "/signalr" };

        public RequestRouteMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string reqPath = httpContext.Request.Path;

            //判断请求是否需要重定向
            bool isDirectUrl = s_directPathPrefixs.Any(x => reqPath.StartsWith(x, StringComparison.OrdinalIgnoreCase));
            //不是直接访问的地址，需要重定向
            if (!isDirectUrl)
            {
                httpContext.Request.Path = "/Home/Index";
            }
            await _next(httpContext);
        }
    }

    /// <summary>
    /// 请求路由扩展
    /// </summary>
    public static class RequestRouteMiddlewareExtensions
    {
        /// <summary>
        /// 请求路由扩展
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestRoute(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestRouteMiddleware>();
        }
    }
}
