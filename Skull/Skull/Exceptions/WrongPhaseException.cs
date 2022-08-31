using System.Runtime.Serialization;

namespace Skull.Exceptions
{
    [Serializable]
    internal class WrongPhaseException : Exception
    {
        public WrongPhaseException()
        {
        }

        public WrongPhaseException(string? message) : base(message)
        {
        }

        public WrongPhaseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected WrongPhaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}