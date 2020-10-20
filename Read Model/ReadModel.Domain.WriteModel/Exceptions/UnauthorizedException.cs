using System;

namespace ReadModel.Domain.WriteModel.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string resourceName) : base($"You're unauthorized to access to this {resourceName}") { }
    }
}