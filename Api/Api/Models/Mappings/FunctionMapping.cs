using Api.Entities;
using Api.Models.Data;
using AutoMapper;

namespace Api.Models.Mappings
{
    public class FunctionMapping : Profile
    {
        public FunctionMapping()
        {
            CreateMap<Function, FunctionData>();
        }
    }
}
