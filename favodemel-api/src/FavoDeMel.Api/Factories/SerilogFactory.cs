using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace FavoDeMel.Api.Factories
{
    public static class SerilogFactory
    {
        public static ILogger GetLogger()
        {
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Default", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.AspNetCore.Session.CookieProtection", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.AspNetCore.DataProtection.KeyManagement.KeyRingBasedDataProtector", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Error)
                .MinimumLevel.Override("IdentityServer4.Hosting.IdentityServerMiddleware", LogEventLevel.Error)
                .MinimumLevel.Override("IdentityServer4.Endpoints.AuthorizeEndpoint", LogEventLevel.Error)
                .MinimumLevel.Override("Jaeger.Reporters", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
            .CreateLogger();
        }
    }
}
