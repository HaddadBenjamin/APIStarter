﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WriteModel.Domain.Audit.Aggregates;
using WriteModel.Infrastructure.Tools.DbContext.Mappers;

namespace WriteModel.Infrastructure.Audit.DbContext.Mappers
{
    public class AuditEventMapper : AggregateMap<AuditEvent>
    {
        protected override void Map(EntityTypeBuilder<AuditEvent> entity)
        {
            entity.HasKey(auditEvent => auditEvent.Id);

            entity.Property(auditEvent => auditEvent.Event).HasColumnType("text");
            entity.Property(auditEvent => auditEvent.EventName).HasMaxLength(50);

            entity.HasIndex(auditEvent => auditEvent.Id);
            entity.HasIndex(auditEvent => auditEvent.EventName);
            entity.HasIndex(auditEvent => auditEvent.CorrelationId);
            entity.HasIndex(auditCommand => auditCommand.Date);
            entity.HasIndex(auditCommand => auditCommand.UserId);
            entity.HasIndex(auditCommand => auditCommand.ImpersonatedUserId);
            entity.HasIndex(auditCommand => auditCommand.AggregateRootVersion);
            entity.HasIndex(auditCommand => auditCommand.EventId);
        }
    }
}