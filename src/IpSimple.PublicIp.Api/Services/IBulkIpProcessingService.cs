namespace IpSimple.PublicIp.Api.Services;

public interface IBulkIpProcessingService
{
    IResult SubmitBulkJob(HttpContext httpContext);
    IResult GetJobStatus(string jobId);
    IResult GetJobResults(string jobId);
}
