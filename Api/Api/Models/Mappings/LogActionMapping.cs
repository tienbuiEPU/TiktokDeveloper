using Api.Entities;
using Api.Models.Dto;
using AutoMapper;

namespace Api.Models.Mappings
{
    public class LogActionMapping : Profile
    {
        public LogActionMapping()
        {
            CreateMap<LogActionModel, LogAction>();
        }
    }
}
