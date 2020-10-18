using System;

namespace WriteModel.Domain.Exceptions
{
    public class GoneException : Exception
    {
        public GoneException(Guid id) : base($"Aggregate with id {id} has already been deactivated") { }
    }
}