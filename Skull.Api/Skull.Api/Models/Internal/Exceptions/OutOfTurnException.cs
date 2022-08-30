using System.Runtime.Serialization;

namespace SkullApi.Models.Internal.Exceptions
{
    [Serializable]
    internal class OutOfTurnException : Exception
    {
        public OutOfTurnException()
        {
        }

        public OutOfTurnException(string? message) : base(message)
        {
        }

        public OutOfTurnException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OutOfTurnException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}