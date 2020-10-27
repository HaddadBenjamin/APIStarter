using System;

namespace ReadModel.Domain.WriteModel.Views
{
    public class HttpRequestView
    {
        public Guid Id { get; set; }

        public string ClientApplication { get; set; }
        public string IPv4 { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string HttpMethod { get; set; }
        public string Uri { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public string UserAgent { get; set; }

        public string ResponseBody { get; set; }
        public int HttpStatus { get; set; }
        public TimeSpan Duration { get; set; }

        public Guid CorrelationId { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid ImpersonatedUserId { get; set; }
    }
}
