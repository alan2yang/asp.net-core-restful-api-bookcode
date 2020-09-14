using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Library.API.Extensions.Middlewares
{
    public class RequestRateLimitingMiddleware
    {
        private const int Limit = 10;
        private readonly RequestDelegate next;
        private readonly IMemoryCache requestStore;

        public RequestRateLimitingMiddleware(RequestDelegate next, IMemoryCache requestStore)
        {
            this.next = next;
            this.requestStore = requestStore;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestKey = $"{context.Request.Method}-{context.Request.Path}";
            int hitCount = 0;

            var cacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1)
            };

            if (requestStore.TryGetValue(requestKey, out hitCount))
            {
                if (hitCount < Limit)
                {
                    await ProcessRequest(context, requestKey, hitCount, cacheOptions);
                }
                else
                {
                    context.Response.Headers["X-RateLimit-RetryAfter"] = cacheOptions.AbsoluteExpiration?.ToString();
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                }
            }
            else
            {
                await ProcessRequest(context, requestKey, hitCount, cacheOptions);
            }
        }

        private async Task ProcessRequest(HttpContext context, string requestKey, int hitCount, MemoryCacheEntryOptions cacheOptions)
        {
            hitCount++;
            requestStore.Set(requestKey, hitCount, cacheOptions);

            context.Response.Headers["X-RateLimit-Limit"] = Limit.ToString();
            context.Response.Headers["X-RateLimit-Remaining"] = (Limit - hitCount).ToString();
            await next(context);
        }
    }
}