using System;
using System.Collections.Generic;
using APIStarter.Domain.CQRS.Interfaces;

namespace APIStarter.Domain.Audit.Commands
{
    public class CreateAuditRequest : ICommand
    {
        public string Method { get; set; }
        public string Uri { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public object Body { get; set; }
        public int Status { get; set; }
        public string Message { get; set; }
        public TimeSpan Duration { get; set; }
    }
}