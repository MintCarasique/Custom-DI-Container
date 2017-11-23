using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DIContainer.Exceptions
{
    public class ElementAlreadyExistsException : ApplicationException
    {
        public ElementAlreadyExistsException()
        {
        }

        public ElementAlreadyExistsException(string message) : base(message)
        {
        }

        public ElementAlreadyExistsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ElementAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
