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
        builder.Services.AddSingleton<IGeolocationService, GeolocationService>();
        builder.Services.AddSingleton<IBulkIpProcessingService, BulkIpProcessingService>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        var ipAddressService = app.Services.GetRequiredService<IIpAddressService>();
        var geolocationService = app.Services.GetRequiredService<IGeolocationService>();
        var bulkService = app.Services.GetRequiredService<IBulkIpProcessingService>();

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

        app.MapGet("/all", ipAddressService.GetClientIps)
           .WithName("GetDualStackIps")
           .WithOpenApi(operation =>
           {
               operation.Summary = "Dual stack IP addresses";
               operation.Description = "Returns both IPv4 and IPv6 addresses if available.";
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

        app.MapGet("/geolocation", geolocationService.GetGeolocationForClient)
           .WithName("GetClientGeolocation")
           .WithOpenApi(operation =>
           {
               operation.Summary = "Geolocation for client IP";
               operation.Description = "Returns geolocation metadata for the calling client.";
               return operation;
           });

        app.MapGet("/geolocation/{ipAddress}", geolocationService.GetGeolocationForIp)
           .WithName("GetIpGeolocation")
           .WithOpenApi(operation =>
           {
               operation.Summary = "Geolocation lookup";
               operation.Description = "Provides geolocation metadata for a specific IP address.";
               return operation;
           });

        app.MapPost("/bulk", bulkService.SubmitBulkJob)
           .WithName("SubmitBulkIpJob")
           .WithOpenApi(operation =>
           {
               operation.Summary = "Submit bulk IP processing";
               operation.Description = "Accepts a list of IP addresses for asynchronous processing.";
               return operation;
           });

        app.MapGet("/bulk/{jobId}", bulkService.GetJobStatus)
           .WithName("GetBulkJobStatus")
           .WithOpenApi(operation =>
           {
               operation.Summary = "Bulk job status";
               operation.Description = "Retrieves processing status for a submitted bulk IP job.";
               return operation;
           });

        app.MapGet("/bulk/{jobId}/results", bulkService.GetJobResults)
           .WithName("GetBulkJobResults")
           .WithOpenApi(operation =>
           {
               operation.Summary = "Bulk job results";
               operation.Description = "Gets processed results for a completed bulk IP job.";
               return operation;
           });

        app.MapGet("/version", () =>
        {
            var version = Environment.GetEnvironmentVariable("APP_VERSION") ?? "unknown";
            return Results.Ok(new { version });
        })
        .WithName("GetVersion")
        .WithOpenApi(operation =>
        {
            operation.Summary = "API Version";
            operation.Description = "Returns the version of the running API (from the APP_VERSION environment variable).";
            return operation;
        });

        return app;
    }
}
