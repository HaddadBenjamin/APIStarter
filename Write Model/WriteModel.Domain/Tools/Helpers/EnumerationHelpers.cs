using System;

namespace WriteModel.Domain.Tools.Helpers
{
    public static class EnumerationHelpers
    {
        public static EnumerationType ToEnumeration<EnumerationType>(string enumerationString) =>
            (EnumerationType)Enum.Parse(typeof(EnumerationType), enumerationString);
    }
}