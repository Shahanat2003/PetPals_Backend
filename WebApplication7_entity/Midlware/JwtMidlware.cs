namespace WebApplication7_petPals.Midlware
{
    public class JwtMidlware
    {
        private readonly RequestDelegate _next;
        public JwtMidlware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Cookies.TryGetValue("AuthorizationToken", out var cookie)) {
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Request.Headers.Append("Authorization", $"Bearer {cookie}");
                }
                await _next(context);
            }

        }
    }
}
