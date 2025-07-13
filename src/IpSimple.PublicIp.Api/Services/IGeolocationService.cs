namespace IpSimple.PublicIp.Api.Services;

public interface IGeolocationService
{
    IResult GetGeolocationForClient(HttpContext httpContext);
    IResult GetGeolocationForIp(string ipAddress);
}
