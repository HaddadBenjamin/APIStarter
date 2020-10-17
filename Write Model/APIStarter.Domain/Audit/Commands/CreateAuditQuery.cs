using APIStarter.Domain.CQRS.Interfaces;

namespace APIStarter.Domain.Audit.Commands
{
    public class CreateAuditQuery : ICommand
    {
        public object Query { get; set; }
        public object QueryResult { get; set; }
    }
}