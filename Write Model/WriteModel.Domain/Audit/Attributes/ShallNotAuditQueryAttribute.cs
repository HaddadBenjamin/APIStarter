using System;

namespace WriteModel.Domain.Audit.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ShallNotAuditQueryAttribute : Attribute { }
}