﻿using System.Collections.Generic;
using System.Linq;

namespace WriteModel.Domain.Helpers
{
    public static class EnumerableHelpers
    {
        public static bool IsNullOrEmpty<T>(IEnumerable<T> sequence) => sequence == null || !sequence.Any();
    }
}
