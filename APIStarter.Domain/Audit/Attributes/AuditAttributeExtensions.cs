using System;
using System.Linq;
using System.Reflection;

namespace APIStarter.Domain.Audit.Attributes
{
    public static class AuditAttributeExtensions
    {
        public static bool ShouldAuditDatabaseChange(this MemberInfo memberInfo) => !(memberInfo is null || memberInfo.GetCustomAttributes<ShallNotAuditDatabaseChangeAttribute>(true).Any());
        public static bool ShouldAuditDatabaseChange(this Type type) => !(type is null || type.GetCustomAttributes<ShallNotAuditDatabaseChangeAttribute>(true).Any());

        public static bool ShouldAuditCommand(this Type type) => !(type is null || type.GetCustomAttributes<ShallNotAuditCommandAttribute>(true).Any());
        public static bool ShouldAuditEvent(this Type type) => !(type is null || type.GetCustomAttributes<ShallNotAuditEventAttribute>(true).Any());
        public static bool ShouldAuditQuery(this Type type) => !(type is null || type.GetCustomAttributes<ShallNotAuditQueryAttribute>(true).Any());
    }
}