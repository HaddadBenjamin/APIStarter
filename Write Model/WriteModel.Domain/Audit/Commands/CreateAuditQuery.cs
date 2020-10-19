using WriteModel.Domain.CQRS.Interfaces;

namespace WriteModel.Domain.Audit.Commands
{
    public class CreateAuditQuery : ICommand
    {
        public object Query { get; set; }
        public object QueryResult { get; set; }
    }
}