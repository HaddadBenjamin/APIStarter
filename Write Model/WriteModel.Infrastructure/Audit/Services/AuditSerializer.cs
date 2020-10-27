using Newtonsoft.Json;
using WriteModel.Domain.Audit.Services;

namespace WriteModel.Infrastructure.Audit.Services
{
    public class AuditSerializer : IAuditSerializer
    {
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public string Serialize(object @object) => JsonConvert.SerializeObject(@object, Formatting.Indented, SerializerSettings);
    }
}