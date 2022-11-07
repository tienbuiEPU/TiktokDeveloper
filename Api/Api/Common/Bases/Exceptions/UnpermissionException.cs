using Api.Common.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common.Bases.Exceptions
{
    public class UnpermissionException : Exception
    {
        public string Message { get; set; }
        public int ErrorCode { get; set; }

        public UnpermissionException()
            : base()
        {
            Message = string.Empty;
            ErrorCode = ApiConstants.StatusCode.NoPermision;
        }

        public UnpermissionException(string message = "")
            : base(message)
        {
            Message = message;
            ErrorCode = ApiConstants.StatusCode.NoPermision;
        }

        public UnpermissionException(string message = "", int errorCode = ApiConstants.StatusCode.NoPermision)
            : base(message)
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public UnpermissionException(string message = "", int errorCode = ApiConstants.StatusCode.NoPermision, Exception innerException = null)
            : base(message, innerException)
        {
            Message = message;
            ErrorCode = errorCode;
        }

        public UnpermissionException(string name, object key)
            : base($"Entity \"{name}\" ({key}) was not found.")
        {
        }
    }
}
