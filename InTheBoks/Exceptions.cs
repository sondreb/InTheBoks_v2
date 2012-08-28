using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InTheBoks
{
    public class ServiceUnavailableExceptions : Exception
    {
        public ServiceUnavailableExceptions(string message, Exception innerException) : base(message, innerException)
        {

        }

        public ServiceUnavailableExceptions(string message) : base(message)
        {

        }

        
    }

    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException()
        {

        }

        public ItemNotFoundException(string message) : base(message)
        {

        }
    }

}
