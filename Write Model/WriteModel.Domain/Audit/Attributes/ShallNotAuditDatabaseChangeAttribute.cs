using System;

namespace WriteModel.Domain.Audit.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class ShallNotAuditDatabaseChangeAttribute : Attribute { }
}