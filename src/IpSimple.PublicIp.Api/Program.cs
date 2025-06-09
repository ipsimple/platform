using IpSimple.PublicIp.Api.Services;

namespace IpSimple.PublicIp.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var app = CreateWebApp(args);
        app.Run();
    }

    public static WebApplication CreateWebApp(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSingleton<IIpAddressService, IpAddressService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        var ipAddressService = app.Services.GetRequiredService<IIpAddressService>();

        app.MapGet("/", ipAddressService.GetClientIpv4)
           .WithName("Default")
           .WithOpenApi(operation =>
           {
               operation.Summary = "Public IPv4 address";
               operation.Description = "This endpoint returns the public IPv4 address of the client making the request.";
               return operation;
           });

        app.MapGet("/ipv4", ipAddressService.GetClientIpv4)
           .WithName("GetPublicIPv4")
           .WithOpenApi(operation =>
           {
               operation.Summary = "Public IPv4 address";
               operation.Description = "This endpoint returns the public IPv4 address of the client making the request.";
               return operation;
           });

        app.MapGet("/ipv4/all", ipAddressService.GetAllClientIpv4s)
           .WithName("GetAllPublicIPv4")
           .WithOpenApi(operation =>
           {
               operation.Summary = "All Public IPv4 addresses";
               operation.Description = "This endpoint returns all public IPv4 addresses from the X-Forwarded-For header.";
               return operation;
           });

        app.MapGet("/ipv6", ipAddressService.GetClientIpv6)
           .WithName("GetPublicIPv6")
           .WithOpenApi(operation =>
           {
               operation.Summary = "Public IPv6 address";
               operation.Description = "This endpoint returns the public IPv6 address of the client making the request.";
               return operation;
           });

        app.MapGet("/ipv6/all", ipAddressService.GetAllClientIpv6s)
           .WithName("GetAllPublicIPv6")
           .WithOpenApi(operation =>
           {
               operation.Summary = "All Public IPv6 addresses";
               operation.Description = "This endpoint returns all public IPv6 addresses from the X-Forwarded-For header.";
               return operation;
           });

        return app;
    }
}
