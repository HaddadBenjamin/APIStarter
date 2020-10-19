using System.Threading.Tasks;

namespace WriteModel.Domain.Audit.Services
{
    public interface IDatabaseChangesAuditService
    {
        Task Audit();
    }
}