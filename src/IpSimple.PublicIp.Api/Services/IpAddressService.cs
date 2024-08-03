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
}
