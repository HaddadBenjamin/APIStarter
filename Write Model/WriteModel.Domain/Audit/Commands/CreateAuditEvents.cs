using System.Collections.Generic;
using WriteModel.Domain.CQRS.Interfaces;

namespace WriteModel.Domain.Audit.Commands
{
    public class CreateAuditEvents : ICommand
    {
        public IReadOnlyCollection<IEvent> Events { get; set; }
    }
}
