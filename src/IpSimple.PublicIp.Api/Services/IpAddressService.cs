using IpSimple.Domain;
using IpSimple.Domain.Settings;
using IpSimple.Extensions;

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
