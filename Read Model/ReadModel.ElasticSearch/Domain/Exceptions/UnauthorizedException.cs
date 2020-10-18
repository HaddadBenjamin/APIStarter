using System;

namespace ReadModel.ElasticSearch.Domain.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string resourceName) : base($"You're unauthorized to access to this {resourceName}") { }
    }
}