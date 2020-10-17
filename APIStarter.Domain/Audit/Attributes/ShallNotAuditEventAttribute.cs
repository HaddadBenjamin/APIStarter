using System;

namespace APIStarter.Domain.Audit.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Class)]
    public class ShallNotAuditEventAttribute : Attribute { }
}