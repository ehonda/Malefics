using System;
using System.Runtime.Serialization;

namespace Malefics.Exceptions
{
    public class InvalidTileOperationException : Exception
    {
        /// <inheritdoc />
        public InvalidTileOperationException()
        {
        }

        /// <inheritdoc />
        protected InvalidTileOperationException(
            SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        public InvalidTileOperationException(string? message) : base(message)
        {
        }

        /// <inheritdoc />
        public InvalidTileOperationException(string? message, Exception? innerException) 
            : base(message, innerException)
        {
        }
    }
}
