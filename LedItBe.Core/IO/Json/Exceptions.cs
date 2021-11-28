using System;

namespace LedItBe.Core.IO.Json
{
    internal class JsonException : Exception
    {
        public JsonException()
        { }

        public JsonException(string message)
            : base(message)
        { }

        public JsonException(string message, params object[] obj)
            : base(string.Format(message, obj))
        { }

        public JsonException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
