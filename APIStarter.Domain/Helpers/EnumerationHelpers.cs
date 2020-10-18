using System;

namespace APIStarter.Domain.Helpers
{
    public static class EnumerationHelpers
    {
        public static EnumerationType ToEnumeration<EnumerationType>(string enumerationString) =>
            (EnumerationType)Enum.Parse(typeof(EnumerationType), enumerationString);
    }
}