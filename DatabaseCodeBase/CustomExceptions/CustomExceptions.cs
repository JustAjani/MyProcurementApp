using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseCodeBase.CustomExceptions
{
    internal class InitializationException : Exception
    {
        public InitializationException(string message) : base(message) { }
    }

    internal class ConnectionFailedException : Exception
    {
        public ConnectionFailedException(string message) : base(message) { }
    }
    internal class QuertFailedException : Exception
    {
        public QuertFailedException(string message) : base(message) { }
    }
}
