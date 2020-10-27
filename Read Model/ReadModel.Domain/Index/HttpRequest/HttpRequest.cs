using System;

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
}
