using System;
using Nest;

namespace ReadModel.Domain.Index.HttpRequest
{
    public class HttpRequest
    {
        public Guid Id { get; set; }

        public GeoIp GeoIp { get; set; }
        public HttpRequestRequest Request { get; set; }
        public HttpRequestResponse Response { get; set; }
        public HttpRequestAudit Audit { get; set; }

        public DateTime Date { get; set; }
        public string FormattedDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string FormattedDuration { get; set; }
    }

    public class GeoIp
    {
        public string IPv4 { get; set; }
        public GeoLocation Location { get; set; }
    }

    public class HttpRequestRequest
    {
        public string Method { get; set; }
        public string Uri { get; set; }
        public string Headers { get; set; }
        public string Body { get; set; }
        public string Os { get; set; }
        public string Browser { get; set; }
        public string Device { get; set; }
    }

    public class HttpRequestResponse
    {
        public string Body { get; set; }
        public int HttpStatus { get; set; }
    }

    public class HttpRequestAudit
    {
        public string ClientApplication { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }
        public Guid ImpersonatedUserId { get; set; }
    }
}
