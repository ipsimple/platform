namespace IpSimple.Domain;

public static class Constants
{
    public static class HttpHeaders
    {
        public const string XAzureClientIPHeader = "X-Azure-ClientIP";
        public const string XForwardedForHeader = "X-Forwarded-For";
        public const string ViaHeader = "Via";
        public const string XAzureSocketIPHeader = "X-Azure-SocketIP";
        public const string XAzureRefHeader = "X-Azure-Ref";
        public const string XAzureRequestChainHeader = "X-Azure-RequestChain";
        public const string XForwardedHostHeader = "X-Forwarded-Host";
        public const string XForwardedProtoHeader = "X-Forwarded-Proto";
        public const string XFDHealthProbeHeader = "X-FD-HealthProbe";
    }

    public static class ErrorMessages
    {
        public const string NoClientIpFound = "No client IP address found in the request (X-Forwarded-For header is missing).";
    }
}
