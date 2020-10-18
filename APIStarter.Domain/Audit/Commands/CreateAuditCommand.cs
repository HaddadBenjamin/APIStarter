using APIStarter.Domain.CQRS.Interfaces;

namespace APIStarter.Domain.Audit.Commands
{
    public class CreateAuditCommand : ICommand
    {
        public ICommand Command { get; set; }
    }
}