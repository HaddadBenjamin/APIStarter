using System;

namespace ReadModel.Domain.Index
{
    public class HttpRequest
    {
        public Guid Id { get; set; }
        public string HttpMethod { get; set; }
        public string Uri { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public int HttpStatus { get; set; }
        public string ResponseBody { get; set; }
        public string ClientApplication { get; set; }
        public string Duration { get; set; }
        public Guid CorrelationId { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid ImpersonatedUserId { get; set; }
    }
}
