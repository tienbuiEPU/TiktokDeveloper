using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common.Bases.Exceptions
{
    public class RequiredTokenExeption : Exception
    {
        public string LanguageIsoCode { get; set; }
        public string ErrorCode { get; set; }

        public RequiredTokenExeption(string errorCode, string languageIsoCode = "vi")
            : base()
        {
            LanguageIsoCode = languageIsoCode;
            ErrorCode = errorCode;
        }

        public RequiredTokenExeption(string errorCode, string languageIsoCode, string message = "")
            : base(message)
        {
            LanguageIsoCode = languageIsoCode;
            ErrorCode = errorCode;
        }

        public RequiredTokenExeption(string errorCode, string languageIsoCode, string message, Exception innerException = null)
            : base(message, innerException)
        {
            LanguageIsoCode = languageIsoCode;
            ErrorCode = errorCode;
        }
    }
}
