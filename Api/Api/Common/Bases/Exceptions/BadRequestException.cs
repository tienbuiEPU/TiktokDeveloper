using Api.Common.Constants;
using System;

namespace Api.Common.Bases.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Message { get; set; }
        public int ErrorCode { get; set; }

        public BadRequestException()
            : base()
        {
            Message = string.Empty;
            ErrorCode = ApiConstants.StatusCode.Error400;
        }

        public BadRequestException(string message = "")
            : base(message)
        {
            Message = message;
            ErrorCode = ApiConstants.StatusCode.Error400;
        }

        public BadRequestException(string message = "", int errorCode = ApiConstants.StatusCode.Error400)
            : base(message)
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public BadRequestException(string message = "", int errorCode = ApiConstants.StatusCode.Error400, Exception innerException = null)
            : base(message, innerException)
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public BadRequestException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
