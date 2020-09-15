using System;

namespace Common.Exceptions
{
    [Serializable]
    public class ArgumentWrongException : Exception
    {
        public ArgumentWrongException() : base()
        {
        }

        public ArgumentWrongException(string message) : base(message)
        {
        }

        public ArgumentWrongException(string message, System.Exception inner) : base(message, inner)
        {
        }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected ArgumentWrongException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}