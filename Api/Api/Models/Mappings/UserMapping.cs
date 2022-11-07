using Api.Entities;
using Api.Models.Data;
using AutoMapper;

namespace Api.Models.Mappings
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserLoginData>();
            CreateMap<User, UserData>();
        }
    }
}
