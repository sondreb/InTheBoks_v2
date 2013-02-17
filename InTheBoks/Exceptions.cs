using System;

namespace InTheBoks
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException()
        {
        }

        public ItemNotFoundException(string message)
            : base(message)
        {
        }
    }

    public class TokenExpiredException : Exception
    {
        public TokenExpiredException()
        {

        }

        public TokenExpiredException(string message)
            : base(message)
        {

        }
    }

    public class ServiceUnavailableExceptions : Exception
    {
        public ServiceUnavailableExceptions(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ServiceUnavailableExceptions(string message)
            : base(message)
        {
        }
    }
}