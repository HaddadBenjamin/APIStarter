﻿using System;

namespace APIStarter.Domain.Exceptions
{
    public class GoneException : Exception
    {
        public GoneException(Guid id) : base($"Aggregate with id {id} has already been deactivated") { }
    }
}