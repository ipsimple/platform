using IpSimple.Domain;
using Microsoft.AspNetCore.Http;

namespace IpSimple.Extensions;

public static class HttpContextExtensions
{
    /// <summary>
    /// Gets the client IP address from the HttpContext.
    /// If the request is forwarded by Azure Front Door, it will have the below headers
    /// 
    /// "Via": "HTTP/1.1 Azure",
    /// "X-Azure-ClientIP": "203.211.106.230",
    /// "X-Azure-SocketIP": "203.211.106.230",
    /// "X-Azure-Ref": "20240711T031409Z-16f8dbf69ccxc7nz3vdync91ac00000001hg00000000d6vf",
    /// "X-Azure-RequestChain": "",
    /// "X-Forwarded-For": "203.211.106.230,147.243.18.238:45498,147.243.18.238",
    /// "X-Forwarded-Host": "api.ipsimple.org",
    /// "X-Forwarded-Proto": "https",
    /// "X-FD-HealthProbe": ""
    /// 
    /// If X-Azure-ClientIP is present, it will be used as the client IP address. If not then the X-Forwarded-For header will be used.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns>The client IP address if found, otherwise null.</returns>
    public static string? GetClientIpAddress(this HttpContext httpContext)
    {
        // If we found the X-Azure-ClientIP header, we use the client IP address
        var xAzureClientIPHeaderFound = httpContext.Request.Headers.TryGetValue(Constants.HttpHeaders.XAzureClientIPHeader, out var azureClientIp);
        if (xAzureClientIPHeaderFound)
        {
            return azureClientIp.ToString();
        }

        // If we have something else instead of Azure Front Door, we use the X-Forwarded-For header
        var xForwardedForHeaderFound = httpContext.Request.Headers.TryGetValue(Constants.HttpHeaders.XForwardedForHeader, out var forwardedFor);
        if (xForwardedForHeaderFound)
        {
            //The first IP is always the original client IP address
            return forwardedFor.ToString().Split(',')[0];
        }

        //If we don't have any of the above headers, return null and let the caller decide what to do (either the caller can throw the exception or return something back to the client)
        return null;
    }

    /// <summary>
    /// Gets all possible client IP addresses from the X-forwarded-for header.
    /// 
    /// This is useful in some cases where the proxy server might be appending the ip address of the client to the X-forwarded-for header in the wrong order
    /// and it is not the first IP address in the header
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns>All possible client IP addresses if found, otherwise null.</returns>
    public static string? GetAllPossibleClientIpAddresses(this HttpContext httpContext)
    {
        var xForwardedForHeaderFound = httpContext.Request.Headers.TryGetValue(Constants.HttpHeaders.XForwardedForHeader, out var forwardedFor);
        if (xForwardedForHeaderFound)
        {
            return forwardedFor.ToString();
        }

        //If we don't actually have the x-forwarded-for header, return null and let the caller decide what to do (either the caller can throw the exception or return something back to the client)
        return null;
    }
}
