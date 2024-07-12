namespace IpSimple.PublicIp.Api.Services;

public interface IIpAddressService
{
    IResult GetClientIp(HttpContext httpContext, bool getAllXForwardedForIpAddresses = false);
    IResult GetAllClientIps(HttpContext httpContext);
}
