using IpSimple.PublicIp.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace IpSimple.PublicIp.Api.Tests;

public class IpAddressServiceTests
{
    private readonly IIpAddressService ipAddressService;

    public IpAddressServiceTests() => ipAddressService = new IpAddressService();

    [Fact]
    public void GetClientIp_WithoutFormat_ReturnsPlainText()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers["X-Forwarded-For"] = "98.207.254.136";

        // Act
        var result = ipAddressService.GetClientIp(context);

        // Assert
        Assert.NotNull(result);
        var textResult = Assert.IsType<ContentHttpResult>(result);
        Assert.Equal("text/plain", textResult.ContentType);
        Assert.Equal("98.207.254.136", textResult.ResponseContent);
    }

    [Fact]
    public void GetClientIp_WithFormatJson_ReturnsJson()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers["X-Forwarded-For"] = "98.207.254.136";
        context.Request.QueryString = new QueryString("?format=json");

        // Act
        var result = ipAddressService.GetClientIp(context);

        // Assert
        Assert.NotNull(result);
        var jsonResult = Assert.IsType<JsonHttpResult<IpAddress>>(result);
        var ip = jsonResult.Value.Ip;
        Assert.Equal("98.207.254.136", ip);
    }

    [Fact]
    public void GetAllClientIps_WithoutFormat_ReturnsPlainText()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers["X-Forwarded-For"] = "98.207.254.136, 203.0.113.195";

        // Act
        var result = ipAddressService.GetAllClientIps(context);

        // Assert
        Assert.NotNull(result);
        var textResult = Assert.IsType<ContentHttpResult>(result);
        Assert.Equal("text/plain", textResult.ContentType);
        Assert.Equal("98.207.254.136, 203.0.113.195", textResult.ResponseContent);
    }

    [Fact]
    public void GetAllClientIps_WithFormatJson_ReturnsJson()
    {
        // Arrange
        var context = new DefaultHttpContext();
        context.Request.Headers["X-Forwarded-For"] = "98.207.254.136, 203.0.113.195";
        context.Request.QueryString = new QueryString("?format=json");

        // Act
        var result = ipAddressService.GetAllClientIps(context);

        // Assert
        Assert.NotNull(result);
        var jsonResult = Assert.IsType<JsonHttpResult<IpAddress>>(result);
        var ip = jsonResult.Value.Ip;
        Assert.Equal("98.207.254.136, 203.0.113.195", ip);
    }
}
