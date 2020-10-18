using System;

namespace APIStarter.Domain.Audit.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ShallNotAuditQueryAttribute : Attribute { }
}