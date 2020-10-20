using System;

namespace ReadModel.Domain.WriteModel.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
