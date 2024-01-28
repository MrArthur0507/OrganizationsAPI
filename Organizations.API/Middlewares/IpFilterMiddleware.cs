using Organizations.API.Services;
using System.Net;

namespace Organizations.API.Middlewares
{
    public class IpFilterMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IIpFilterService _ipFilterService;

        public IpFilterMiddleware(RequestDelegate next, IIpFilterService ipFilterService)
        {
            _next = next;
            _ipFilterService = ipFilterService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var remoteIpAddress = context.Connection.RemoteIpAddress;
            if (!_ipFilterService.IsIpAddressAllowed(remoteIpAddress.ToString()))
            {
                context.Response.StatusCode = 403;
                return;
            }
            await _next(context);
        }
    }
}
