using System;

namespace ReadModel.ElasticSearch.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string resourceName) : base($"{resourceName} not found") { }
    }
}