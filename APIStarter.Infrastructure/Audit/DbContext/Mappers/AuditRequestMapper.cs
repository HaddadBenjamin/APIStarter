using APIStarter.Domain.Audit.Aggregates;
using APIStarter.Infrastructure.DbContext.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace APIStarter.Infrastructure.Audit.DbContext.Mappers
{
    public class AuditRequestMapper : AggregateMap<AuditRequest>
    {
        protected override void Map(EntityTypeBuilder<AuditRequest> entity)
        {
            entity.HasKey(auditRequest => auditRequest.Id);

            entity.Property(auditRequest => auditRequest.ResponseBody).HasColumnType("text");
            entity.Property(auditRequest => auditRequest.RequestBody).HasColumnType("text");
            entity.Property(auditRequest => auditRequest.Uri).HasMaxLength(500);
            entity.Property(auditRequest => auditRequest.HttpMethod).HasMaxLength(10);
        
            entity.HasIndex(auditRequest => auditRequest.Id);
            entity.HasIndex(auditRequest => auditRequest.HttpMethod);
            entity.HasIndex(auditRequest => auditRequest.Uri);
            entity.HasIndex(auditRequest => auditRequest.RequestHeaders);
            entity.HasIndex(auditRequest => auditRequest.Duration);
            entity.HasIndex(auditEvent => auditEvent.CorrelationId);
            entity.HasIndex(auditCommand => auditCommand.Date);
            entity.HasIndex(auditCommand => auditCommand.UserId);
            entity.HasIndex(auditCommand => auditCommand.ImpersonatedUserId);
        }
    }
}