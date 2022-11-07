using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common.Bases.Exceptions
{
    public class CommonException : Exception
    {
        public string LanguageIsoCode { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }

        public CommonException(int errorCode, string errorMessage)
            : base()
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }

        public CommonException(int errorCode, string message = "", string languageIsoCode = "vi")
            : base(message)
        {
            LanguageIsoCode = languageIsoCode;
            ErrorCode = errorCode;
            ErrorMessage = message;
        }

        public CommonException(int errorCode, string message = "", string languageIsoCode = "vi", Exception innerException = null)
            : base(message, innerException)
        {
            LanguageIsoCode = languageIsoCode;
            ErrorCode = errorCode;
            ErrorMessage = message;
        }

        //public CommonException(string name, object key)
        //    : base($"Entity \"{name}\" ({key}) was not found.")
        //{
        //}
    }
}
