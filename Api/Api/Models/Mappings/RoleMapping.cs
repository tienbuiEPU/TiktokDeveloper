using Api.Entities;
using Api.Models.Data;
using AutoMapper;

namespace Api.Models.Mappings
{
    public class RoleMapping : Profile
    {
        public RoleMapping()
        {
            CreateMap<Role, RoleData>();
        }
    }
}
