using System;
using System.Linq;
using System.Reflection;

namespace APIStarter.Domain.Audit.Attributes
{
    public static class AuditAttributeExtensions
    {
        public static bool ShouldAudit(this MemberInfo memberInfo) => !(memberInfo is null || memberInfo.GetCustomAttributes<ShallNotAuditAttribute>(true).Any());
        public static bool ShouldAudit(this Type type) => !(type is null || type.GetCustomAttributes<ShallNotAuditAttribute>(true).Any());
    }
}