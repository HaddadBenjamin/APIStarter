using System.Threading.Tasks;

namespace APIStarter.Domain.Audit.Services
{
    public interface IDatabaseChangesAuditService
    {
        Task Audit();
    }
}