using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Api.Common.Bases.Exceptions
{
    public class DbUpdateConcurrencyException : Exception
    {
        public Dictionary<string, PropertyInfo> DbValues { get; set; }

        public DbUpdateConcurrencyException(Dictionary<string, PropertyInfo> dbValues)
        { }

        public DbUpdateConcurrencyException(Dictionary<string, PropertyInfo> dbValues, string message)
            : base(message)
        { }

        public DbUpdateConcurrencyException(Dictionary<string, PropertyInfo> dbValues, string message, Exception innnerException)
            : base(message, innnerException)
        { }
    }
}
