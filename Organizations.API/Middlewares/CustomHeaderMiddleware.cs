namespace Organizations.API.Middlewares
{
    public class CustomHeaderMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("X-Api-Name", "OrganizationsAPI");
            await _next(context);
        }
    }
}
