namespace WriteModel.Domain.Audit.Services
{
    public interface IAuditSerializer
    {
        string Serialize(object @object);
    }
}