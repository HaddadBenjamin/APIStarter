﻿using System;

namespace WriteModel.Domain.Audit.Aggregates
{
    /// <summary>
    /// Permet d'auditer tous les changements réalisés sur votre base de données.
    /// </summary>
    public class AuditDatabaseChange
    {
        public Guid Id { get; set; }
        public string TableName { get; set; }
        public string EntityId { get; set; }
        public string WriteAction { get; set; }
        public string Changes { get; set; }
        public Guid CorrelationId { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public Guid ImpersonatedUserId { get; set; }
    }
}