﻿namespace WriteModel.Domain.Audit.Configuration
{
    public class AuditConfiguration
    {
        public bool AuditCommands { get; set; }
        public bool AuditQueries { get; set; }
        public bool AuditEvents { get; set; }
        public bool AuditDatabaseChanges { get; set; }
        public bool AuditRequests { get; set; }
    }
}
