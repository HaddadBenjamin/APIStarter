using WriteModel.Domain.CQRS.Interfaces;

namespace WriteModel.Domain.Audit.Commands
{
    public class CreateAuditCommand : ICommand
    {
        public ICommand Command { get; set; }
    }
}