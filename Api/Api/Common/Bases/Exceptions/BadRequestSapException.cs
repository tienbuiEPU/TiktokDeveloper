using Api.Common.Constants;
using System;

namespace Api.Common.Bases.Exceptions
{
    public class BadRequestSapException : Exception
    {
        public string Message { get; set; }
        public int ErrorCode { get; set; }

        public BadRequestSapException()
            : base()
        {
            Message = string.Empty;
            ErrorCode = ApiConstants.StatusCode.Error400;
        }

        public BadRequestSapException(string message = "", int errorCode = ApiConstants.StatusCode.Error400)
            : base(message)
        {
            Message = message;
            ErrorCode = errorCode;
        }
    }
}
