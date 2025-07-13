using IpSimple.Domain.Settings;
using IpSimple.Extensions;

namespace IpSimple.PublicIp.Api.Services;

public class GeolocationService : IGeolocationService
{
    public IResult GetGeolocationForClient(HttpContext httpContext)
    {
        // TODO: Extract client IP using existing helpers
        // and call external geolocation provider (e.g. MaxMind).
        // Return structured geolocation data similar to issue #14 spec.
        return Results.Json(new { message = "Geolocation lookup not yet implemented" }, JsonSerializerSettings.DefaultJsonSerializer);
    }

    public IResult GetGeolocationForIp(string ipAddress)
    {
        // TODO: Validate the provided IP address and query
        // the geolocation provider for metadata. Cache results
        // and handle provider errors appropriately.
        return Results.Json(new { message = "Geolocation lookup not yet implemented", ipAddress }, JsonSerializerSettings.DefaultJsonSerializer);
    }
}
