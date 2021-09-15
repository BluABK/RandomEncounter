using System;
using System.Runtime.Serialization;

namespace ASCIIGraphix.Exceptions
{
    [Serializable]
    public class ScreenWidthExceededException : Exception
    {
        public ScreenWidthExceededException()
        {
        }

        public ScreenWidthExceededException(string message)
            : base(message)
        {
        }

        public ScreenWidthExceededException(string message, Exception inner)
            : base(message, inner)
        {
        }

        // Ensure Exception is Serializable
        protected ScreenWidthExceededException(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
        }
    }
}
