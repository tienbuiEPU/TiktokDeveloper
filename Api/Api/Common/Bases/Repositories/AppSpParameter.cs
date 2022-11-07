using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Common.Bases.Repositories
{
    public class AppSpParameter
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public dynamic Value { get; set; }

        public AppSpParameter(string name, dynamic value)
        {
            Name = name;
            Value = value;
        }

        public AppSpParameter(string name, dynamic value, string typeName)
        {
            Name = name;
            TypeName = typeName;
            Value = value;
        }
    }
}
