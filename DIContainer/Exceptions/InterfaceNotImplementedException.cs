using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer.Exceptions
{
    public class InterfaceNotImplementedException : ApplicationException
    {
        public InterfaceNotImplementedException()
        {
        }

        public InterfaceNotImplementedException(string message) : base(message)
        {
        }

        public InterfaceNotImplementedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InterfaceNotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
