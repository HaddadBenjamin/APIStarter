using System.Collections.Generic;
using System.Linq;

namespace APIStarter.Domain.Helpers
{
    public static class EnumerableHelpers
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> sequence) => sequence == null || !sequence.Any();
    }
}
