using System;
using System.IO;
using System.Net;

namespace LedItBe.Core.Api.Http
{
    public abstract class HttpApiResponseBase
    {
        public HttpStatusCode Code { get; protected set; }
        public bool IsSuccess { get; protected set; }
        public string ErrorMessage { get; protected set; }
        public Exception Exception { get; protected set; }
        public Stream Content { get; protected set; }
    }

    public class HttpApiResponse : HttpApiResponseBase
    {
        private HttpApiResponse()
        { }

        public static HttpApiResponse Create(HttpStatusCode code, Stream content) => new HttpApiResponse
        {
            Code = code,
            IsSuccess = true,
            Content = content
        };

        public static HttpApiResponse CreateError(HttpStatusCode code, string message, Exception exception = null) => new HttpApiResponse
        {
            Code = code,
            IsSuccess = false,
            ErrorMessage = message,
            Exception = exception
        };

        public static HttpApiResponse CreateError(HttpStatusCode code, Exception exception = null) => new HttpApiResponse
        {
            Code = code,
            IsSuccess = false,
            Exception = exception,
            ErrorMessage = exception?.Message ?? $"Server returned {code} response"
        };
    }

    public class HttpApiResponse<T> : HttpApiResponseBase
    {
        public T Result { get; private set; }

        public static HttpApiResponse<T> Create(HttpStatusCode code, T result) => new HttpApiResponse<T>
        {
            Code = code,
            IsSuccess = true,
            Result = result
        };

        public static HttpApiResponse<T> CreateError(HttpStatusCode code, string message, Exception exception = null) => new HttpApiResponse<T>
        {
            Code = code,
            IsSuccess = false,
            ErrorMessage = message,
            Exception = exception
        };

        public static HttpApiResponse<T> CreateError(HttpStatusCode code, Exception exception = null) => new HttpApiResponse<T>
        {
            Code = code,
            IsSuccess = false,
            Exception = exception,
            ErrorMessage = exception?.Message ?? $"Server returned {code} response"
        };
    }
}
