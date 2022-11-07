using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Common.Bases.Exceptions
{
    public class ValidationException : GenericException
    {
        public IDictionary<string, string[]> Errors { get; } = new Dictionary<string, string[]>();
        public string Message { get; set; }

        public ValidationException(string message, System.Exception innerException = null)
             : base(message, innerException)
        { }
        public ValidationException(string key, string message)
            : base()
        {
            var failures = new List<ValidationFailure> { new ValidationFailure(key, message) };

            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
            Message = failures.FirstOrDefault().ErrorMessage;
        }
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : base()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
            Message = failures.FirstOrDefault().ErrorMessage;
        }

    }
}
