using System;
using APIStarter.Domain.CQRS.Interfaces;

namespace APIStarter.Domain.Audit.Commands
{
    public class CreateAuditRequest : ICommand
    {
        public string Method { get; set; }
        public string Uri { get; set; }
        public object Headers { get; set; }
        public string Body { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public TimeSpan Duration { get; set; }
    }
}