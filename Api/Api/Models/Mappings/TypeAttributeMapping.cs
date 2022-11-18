using Api.Entities;
using Api.Models.Data;
using AutoMapper;

namespace Api.Models.Mappings
{
    public class TypeAttributeMapping : Profile
    {
        public TypeAttributeMapping()
        {
            CreateMap<TypeAttribute, TypeAttributeData>();
        }
    }
}
