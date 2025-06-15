public class AuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private const string ValidToken = "12345";

    public AuthenticationMiddleware(RequestDelegate next) => _next = next;

    public async Task Invoke(HttpContext context)
    {
        if (!context.Request.Headers.TryGetValue("Authorization", out var token) || token != $"Bearer {ValidToken}")
        {
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Unauthorized");
            return;
        }

        await _next(context);
    }
}
