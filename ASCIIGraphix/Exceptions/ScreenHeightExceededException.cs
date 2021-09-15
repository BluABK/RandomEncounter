using System;
using System.Runtime.Serialization;

namespace ASCIIGraphix.Exceptions
{
    [Serializable]
    public class ScreenHeightExceededException : Exception
    {
        public ScreenHeightExceededException()
        {
        }

        public ScreenHeightExceededException(string message)
            : base(message)
        {
        }

        public ScreenHeightExceededException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // Ensure Exception is Serializable
        protected ScreenHeightExceededException(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
        }
    }
}
