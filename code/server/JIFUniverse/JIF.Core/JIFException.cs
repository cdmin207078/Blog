using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core
{
    [Serializable]
    public class JIFException : Exception
    {
        public JIFException()
        {
        }

        public JIFException(string message)
            : base(message)
        {
        }

        public JIFException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        protected JIFException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }

        public JIFException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
