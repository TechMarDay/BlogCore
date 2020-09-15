using System;

namespace Common.Exceptions
{
    [Serializable]
    public class NotPermissionException : Exception
    {
        public NotPermissionException() : base("No permission to this action")
        {
        }

        public NotPermissionException(string message) : base(message)
        {
        }

        public NotPermissionException(string message, System.Exception inner) : base(message, inner)
        {
        }

        // A constructor is needed for serialization when an
        // exception propagates from a remoting server to the client.
        protected NotPermissionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}