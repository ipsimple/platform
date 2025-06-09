using IpSimple.Domain;
using Microsoft.AspNetCore.Http;
using System.Net;

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
    }    /// <summary>
    /// Gets the client IPv4 address from the HttpContext.
    /// Filters out IPv6 addresses and returns only IPv4.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns>The client IPv4 address if found, otherwise null.</returns>
    public static string? GetClientIpv4Address(this HttpContext httpContext)
    {
        var clientIp = GetClientIpAddress(httpContext);
        if (string.IsNullOrEmpty(clientIp))
        {
            return null;
        }

        // Parse all IPs from the string (could be comma-separated)
        var ips = clientIp.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var ip in ips)
        {
            var trimmedIp = ip.Trim().Split(':')[0]; // Remove port if present
            if (IPAddress.TryParse(trimmedIp, out var ipAddress) && ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return trimmedIp;
            }
        }

        return null;
    }    /// <summary>
    /// Gets the client IPv6 address from the HttpContext.
    /// Filters out IPv4 addresses and returns only IPv6.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns>The client IPv6 address if found, otherwise null.</returns>
    public static string? GetClientIpv6Address(this HttpContext httpContext)
    {
        var clientIp = GetClientIpAddress(httpContext);
        if (string.IsNullOrEmpty(clientIp))
        {
            return null;
        }

        // Parse all IPs from the string (could be comma-separated)
        var ips = clientIp.Split(',', StringSplitOptions.RemoveEmptyEntries);

        foreach (var ip in ips)
        {
            var trimmedIp = ip.Trim().Split(' ')[0]; // Remove any extra info
            if (IPAddress.TryParse(trimmedIp, out var ipAddress) && ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                return trimmedIp;
            }
        }

        return null;
    }    /// <summary>
    /// Gets all possible client IPv4 addresses from the X-forwarded-for header.
    /// Filters and returns only IPv4 addresses.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns>All IPv4 addresses found, otherwise null.</returns>
    public static string? GetAllPossibleClientIpv4Addresses(this HttpContext httpContext)
    {
        var allIps = GetAllPossibleClientIpAddresses(httpContext);
        if (string.IsNullOrEmpty(allIps))
        {
            return null;
        }

        var ips = allIps.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var ipv4Addresses = new List<string>();

        foreach (var ip in ips)
        {
            var trimmedIp = ip.Trim().Split(':')[0]; // Remove port if present
            if (IPAddress.TryParse(trimmedIp, out var ipAddress) && ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                ipv4Addresses.Add(trimmedIp);
            }
        }

        return ipv4Addresses.Count > 0 ? string.Join(", ", ipv4Addresses) : null;
    }    /// <summary>
    /// Gets all possible client IPv6 addresses from the X-forwarded-for header.
    /// Filters and returns only IPv6 addresses.
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns>All IPv6 addresses found, otherwise null.</returns>
    public static string? GetAllPossibleClientIpv6Addresses(this HttpContext httpContext)
    {
        var allIps = GetAllPossibleClientIpAddresses(httpContext);
        if (string.IsNullOrEmpty(allIps))
        {
            return null;
        }

        var ips = allIps.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var ipv6Addresses = new List<string>();

        foreach (var ip in ips)
        {
            var trimmedIp = ip.Trim().Split(' ')[0]; // Remove any extra info
            if (IPAddress.TryParse(trimmedIp, out var ipAddress) && ipAddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                ipv6Addresses.Add(trimmedIp);
            }
        }

        return ipv6Addresses.Count > 0 ? string.Join(", ", ipv6Addresses) : null;
    }
}
