using System;

namespace Common.Exceptions
{
    [Serializable]
    public class UserHttpResponseException : Exception
    {
        public UserHttpResponseException() : base("There's a exception when request http")
        {
        }

        public UserHttpResponseException(string message) : base(message)
        {
        }

        public UserHttpResponseException(string message, System.Exception inner) : base(message, inner)
        {
        }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected UserHttpResponseException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}