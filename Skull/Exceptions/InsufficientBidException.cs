using System.Runtime.Serialization;

namespace Skull.Exceptions
{
    [Serializable]
    internal class InsufficientBidException : Exception
    {
        public InsufficientBidException()
        {
        }

        public InsufficientBidException(string? message) : base(message)
        {
        }

        public InsufficientBidException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected InsufficientBidException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}