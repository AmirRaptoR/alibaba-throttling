using System;

namespace Alibaba.Heracles.Domain.Exceptions
{
    public class InvalidLimitStringException : Exception
    {
        public InvalidLimitStringException(string message) : base(message)
        {
        }
    }
}