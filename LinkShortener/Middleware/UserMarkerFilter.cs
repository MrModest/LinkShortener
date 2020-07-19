using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LinkShortener.Middleware
{
    public class UserMarkerMiddleware
    {
        private readonly RequestDelegate _next;
        
        public UserMarkerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Session.GetString("UserId") == null)
            {
                context.Session.SetString("UserId", Guid.NewGuid().ToString());
            }
            
            await _next(context);
        }
    }
}