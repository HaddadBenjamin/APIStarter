using System.Collections.Generic;
using APIStarter.Domain.CQRS.Interfaces;

namespace APIStarter.Domain.Audit.Commands
{
    public class CreateAuditEvents : ICommand
    {
        public IReadOnlyCollection<IEvent> Events { get; set; }
    }
}
