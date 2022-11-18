using Api.Entities;
using System.Collections.Generic;

namespace Api.Models.Data
{
    public class TypeAttributeData : TypeAttribute
    {
        public List<TypeAttributeItem> typeAttributeItems { get; set; }
    }
}
