namespace IpSimple.PublicIp.Api.Services;

public interface IIpAddressService
{
    IResult GetClientIp(HttpContext httpContext, bool getAllXForwardedForIpAddresses = false);
    IResult GetAllClientIps(HttpContext httpContext);
    IResult GetClientIps(HttpContext httpContext);
    IResult GetClientIpv4(HttpContext httpContext, bool getAllXForwardedForIpAddresses = false);
    IResult GetAllClientIpv4s(HttpContext httpContext);
    IResult GetClientIpv6(HttpContext httpContext, bool getAllXForwardedForIpAddresses = false);
    IResult GetAllClientIpv6s(HttpContext httpContext);
}
