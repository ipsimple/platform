using IpSimple.Domain;
using Microsoft.AspNetCore.Http;

namespace IpSimple.Extensions.Tests;

public class HttpContextExtensionsTests
{
    [Fact]
    public void GetClientIpAddress_XAzureClientIPHeaderPresent_ReturnsAzureClientIP()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers[Constants.HttpHeaders.XAzureClientIPHeader] = "203.211.106.230";

        // Act
        var result = HttpContextExtensions.GetClientIpAddress(context);

        // Assert
        Assert.Equal("203.211.106.230", result);
    }

    [Fact]
    public void GetClientIpAddress_XForwardedForHeaderPresent_ReturnsFirstForwardedForIP()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers[Constants.HttpHeaders.XForwardedForHeader] = "203.211.106.230,147.243.18.238:45498,147.243.18.238";

        // Act
        var result = HttpContextExtensions.GetClientIpAddress(context);

        // Assert
        Assert.Equal("203.211.106.230", result);
    }

    [Fact]
    public void GetClientIpAddress_NoRelevantHeadersPresent_ReturnsNull()
    {
        // Arrange
        var context = new DefaultHttpContext();

        // Act
        var result = HttpContextExtensions.GetClientIpAddress(context);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetAllPossibleClientIpAddresses_XForwardedForHeaderPresent_ReturnsAllForwardedForIPs()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers[Constants.HttpHeaders.XForwardedForHeader] = "203.211.106.230,147.243.18.238:45498,147.243.18.238";

        // Act
        var result = HttpContextExtensions.GetAllPossibleClientIpAddresses(context);

        // Assert
        Assert.Equal("203.211.106.230,147.243.18.238:45498,147.243.18.238", result);
    }

    [Fact]
    public void GetAllPossibleClientIpAddresses_NoRelevantHeadersPresent_ReturnsNull()
    {
        // Arrange
        var context = new DefaultHttpContext();

        // Act
        var result = HttpContextExtensions.GetAllPossibleClientIpAddresses(context);

        // Assert
        Assert.Null(result);
    }
}
