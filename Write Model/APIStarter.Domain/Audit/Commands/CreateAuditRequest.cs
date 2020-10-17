using System;
using APIStarter.Domain.Audit.Attributes;
using APIStarter.Domain.CQRS.Interfaces;

namespace APIStarter.Domain.Audit.Commands
{
    [ShallNotAuditCommand]
    public class CreateAuditRequest : ICommand
    {
        public string HttpMethod { get; set; }
        public string Uri { get; set; }
        public object RequestHeaders { get; set; }
        public string RequestBody { get; set; }
        public int HttpStatus { get; set; }
        public string ResponseBody { get; set; }
        public TimeSpan Duration { get; set; }
    }
}