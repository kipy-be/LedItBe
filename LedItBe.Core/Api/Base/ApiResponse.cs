using System;
using System.IO;
using System.Net;

namespace LedItBe.Core.Api.Base
{
    public abstract class ApiResponseBase
    {
        public HttpStatusCode Code { get; protected set; }
        public bool IsSuccess { get; protected set; }
        public string ErrorMessage { get; protected set; }
        public Exception Exception { get; protected set; }
        public Stream Content { get; protected set; }
    }

    public class ApiResponse : ApiResponseBase
    {
        private ApiResponse()
        { }

        public static ApiResponse Create(HttpStatusCode code, Stream content) => new ApiResponse
        {
            Code = code,
            IsSuccess = true,
            Content = content
        };

        public static ApiResponse CreateError(HttpStatusCode code, string message, Exception exception = null) => new ApiResponse
        {
            Code = code,
            IsSuccess = false,
            ErrorMessage = message,
            Exception = exception
        };

        public static ApiResponse CreateError(HttpStatusCode code, Exception exception = null) => new ApiResponse
        {
            Code = code,
            IsSuccess = false,
            Exception = exception,
            ErrorMessage = exception?.Message ?? $"Server returned {code} response"
        };
    }

    public class ApiResponse<T> : ApiResponseBase
    {
        public T Result { get; private set; }

        public static ApiResponse<T> Create(HttpStatusCode code, T result) => new ApiResponse<T>
        {
            Code = code,
            IsSuccess = true,
            Result = result
        };

        public static ApiResponse<T> CreateError(HttpStatusCode code, string message, Exception exception = null) => new ApiResponse<T>
        {
            Code = code,
            IsSuccess = false,
            ErrorMessage = message,
            Exception = exception
        };

        public static ApiResponse<T> CreateError(HttpStatusCode code, Exception exception = null) => new ApiResponse<T>
        {
            Code = code,
            IsSuccess = false,
            Exception = exception,
            ErrorMessage = exception?.Message ?? $"Server returned {code} response"
        };
    }
}
