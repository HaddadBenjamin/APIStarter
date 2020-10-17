using System;
using System.Linq;
using System.Reflection;

namespace APIStarter.Domain.Audit.Attributes
{
    public static class AuditAttributeExtensions
    {
        public static bool ShouldAuditDatabaseChange(this MemberInfo memberInfo) => !memberInfo.GetCustomAttributes<ShallNotAuditDatabaseChangeAttribute>(true).Any();
        public static bool ShouldAuditDatabaseChange(this Type type) => !type.GetCustomAttributes<ShallNotAuditDatabaseChangeAttribute>(true).Any();

        public static bool ShouldAuditCommand(this Type type) => !type.GetCustomAttributes<ShallNotAuditCommandAttribute>(true).Any();
        public static bool ShouldAuditEvent(this Type type) => !type.GetCustomAttributes<ShallNotAuditEventAttribute>(true).Any();
        public static bool ShouldAuditEvent(this MemberInfo memberInfo) => !memberInfo.GetCustomAttributes<ShallNotAuditEventAttribute>(true).Any();
        public static bool ShouldAuditQuery(this Type type) => !type.GetCustomAttributes<ShallNotAuditQueryAttribute>(true).Any();
    }
}