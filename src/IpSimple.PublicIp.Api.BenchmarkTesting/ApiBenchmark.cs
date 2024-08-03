using BenchmarkDotNet.Attributes;

namespace IpSimple.PublicIp.Api.BenchmarkTesting;

public class ApiBenchmark
{
    private static readonly HttpClient Client = new() { BaseAddress = new Uri("http://localhost:5021") };

    [Benchmark]
    public async Task GetIpv4()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/ipv4");
        request.Headers.Add("X-Forwarded-For", "98.207.254.136");
        request.Headers.Add("X-Azure-ClientIP", "203.0.113.195");
        await Client.SendAsync(request);
    }

    [Benchmark]
    public async Task GetIpv4All()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/ipv4/all");
        request.Headers.Add("X-Forwarded-For", "98.207.254.136, 203.0.113.195");
        request.Headers.Add("X-Azure-ClientIP", "203.0.113.195");
        await Client.SendAsync(request);
    }

    [Benchmark]
    public async Task GetIpv6()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/ipv6");
        request.Headers.Add("X-Forwarded-For", "2a00:1450:400f:80d::200e");
        request.Headers.Add("X-Azure-ClientIP", "2a00:1450:400f:80d::200e");
        await Client.SendAsync(request);
    }

    [Benchmark]
    public async Task GetIpv6All()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, "/ipv6/all");
        request.Headers.Add("X-Forwarded-For", "2a00:1450:400f:80d::200e, 2a00:1450:400f:80d::200f");
        request.Headers.Add("X-Azure-ClientIP", "2a00:1450:400f:80d::200e");
        await Client.SendAsync(request);
    }
}
