using System;
using System.Runtime.Serialization;

namespace CleanCode_ExampleTask21_27
{
    class UnsuitableFormatDataPassportException : Exception
    {
        public UnsuitableFormatDataPassportException()
        {
        }

        public UnsuitableFormatDataPassportException(string message) : base(message)
        {
        }

        public UnsuitableFormatDataPassportException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnsuitableFormatDataPassportException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
