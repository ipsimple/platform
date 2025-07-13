using IpSimple.Domain;
using IpSimple.Domain.Settings;
using IpSimple.Extensions;
using System.Linq;

namespace IpSimple.PublicIp.Api.Services;

public class IpAddressService : IIpAddressService
{
    public IResult GetClientIp(HttpContext httpContext, bool getAllXForwardedForIpAddresses = false)
    {
        var clientIp = getAllXForwardedForIpAddresses ? httpContext.GetAllPossibleClientIpAddresses() : httpContext.GetClientIpAddress();
        clientIp ??= Constants.ErrorMessages.NoClientIpFound;

        var format = httpContext.Request.Query["format"].ToString();

        if (string.Equals(format, "json", StringComparison.OrdinalIgnoreCase))
        {
            return Results.Json(new IpAddress(clientIp), JsonSerializerSettings.DefaultJsonSerializer, "application/json");
        }

        return Results.Text(clientIp, "text/plain");
    }

    public IResult GetAllClientIps(HttpContext httpContext) => GetClientIp(httpContext, true);

    public IResult GetClientIps(HttpContext httpContext)
    {
        // Combine IPv4 and IPv6 detection using existing helpers
        // so callers can retrieve both addresses in a single call
        var ipv4 = httpContext.GetClientIpv4Address();
        var ipv6 = httpContext.GetClientIpv6Address();

        var format = httpContext.Request.Query["format"].ToString();

        if (string.Equals(format, "json", StringComparison.OrdinalIgnoreCase))
        {
            // TODO: extend model to match issue #5 specification
            return Results.Json(new { ipv4, ipv6 }, JsonSerializerSettings.DefaultJsonSerializer, "application/json");
        }

        var combined = string.Join(", ", new[] { ipv4, ipv6 }.Where(s => !string.IsNullOrWhiteSpace(s)));
        return Results.Text(combined, "text/plain");
    }

    public IResult GetClientIpv4(HttpContext httpContext, bool getAllXForwardedForIpAddresses = false)
    {
        var clientIp = getAllXForwardedForIpAddresses ? httpContext.GetAllPossibleClientIpv4Addresses() : httpContext.GetClientIpv4Address();
        clientIp ??= Constants.ErrorMessages.NoClientIpFound;

        var format = httpContext.Request.Query["format"].ToString();

        if (string.Equals(format, "json", StringComparison.OrdinalIgnoreCase))
        {
            return Results.Json(new IpAddress(clientIp), JsonSerializerSettings.DefaultJsonSerializer, "application/json");
        }

        return Results.Text(clientIp, "text/plain");
    }

    public IResult GetAllClientIpv4s(HttpContext httpContext) => GetClientIpv4(httpContext, true);

    public IResult GetClientIpv6(HttpContext httpContext, bool getAllXForwardedForIpAddresses = false)
    {
        var clientIp = getAllXForwardedForIpAddresses ? httpContext.GetAllPossibleClientIpv6Addresses() : httpContext.GetClientIpv6Address();
        clientIp ??= Constants.ErrorMessages.NoClientIpFound;

        var format = httpContext.Request.Query["format"].ToString();

        if (string.Equals(format, "json", StringComparison.OrdinalIgnoreCase))
        {
            return Results.Json(new IpAddress(clientIp), JsonSerializerSettings.DefaultJsonSerializer, "application/json");
        }

        return Results.Text(clientIp, "text/plain");
    }

    public IResult GetAllClientIpv6s(HttpContext httpContext) => GetClientIpv6(httpContext, true);
}
