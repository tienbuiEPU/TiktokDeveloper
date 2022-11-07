using Api.Common.Interfaces.Repository;
using AutoMapper;

namespace Api.Models.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap(typeof(IPagedResult<>), typeof(IPagedResult<>)).ReverseMap();
        }
    }
}
