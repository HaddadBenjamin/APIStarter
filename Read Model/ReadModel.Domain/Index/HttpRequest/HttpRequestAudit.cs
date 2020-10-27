using System;

namespace ReadModel.Domain.Index.HttpRequest
{
    public class HttpRequestAudit
    {
        public string ClientApplication { get; set; }
        public Guid CorrelationId { get; set; }
        public Guid UserId { get; set; }
        public Guid ImpersonatedUserId { get; set; }
    }
}