using Api.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common.Bases.Exceptions
{
    public class NotFoundException : Exception
    {
        public string Message { get; set; }
        public int ErrorCode { get; set; }

        public NotFoundException()
            : base()
        {
            Message = string.Empty;
            ErrorCode = ApiConstants.StatusCode.Error400;
        }

        public NotFoundException(string message = "")
            : base(message)
        {
            Message = message;
            ErrorCode = ApiConstants.StatusCode.Error400;
        }

        public NotFoundException(string message = "", int errorCode = ApiConstants.StatusCode.Error400)
            : base(message)
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public NotFoundException(string message = "", int errorCode = ApiConstants.StatusCode.Error400, Exception innerException = null)
            : base(message, innerException)
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public NotFoundException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
