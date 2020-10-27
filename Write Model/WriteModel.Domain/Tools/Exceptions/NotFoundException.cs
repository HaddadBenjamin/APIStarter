using System;

namespace WriteModel.Domain.Tools.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceName) : base($"{resourceName} not found") { }
    }
}