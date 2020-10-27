using System;
using WriteModel.Domain.Audit.Attributes;
using WriteModel.Domain.CQRS.Interfaces;

namespace WriteModel.Domain.Audit.Commands
{
    [ShallNotAuditCommand]
    public class CreateAuditRequest : ICommand
    {
        public string ClientApplication { get; set; }
        public string IPv4 { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public string HttpMethod { get; set; }
        public string Uri { get; set; }
        public object RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public string UserAgent { get; set; }

        public string ResponseBody { get; set; }
        public int HttpStatus { get; set; }
        public TimeSpan Duration { get; set; }
    }
}