using System;

namespace APIStarter.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceName) : base($"{resourceName} not found") { }
    }
}