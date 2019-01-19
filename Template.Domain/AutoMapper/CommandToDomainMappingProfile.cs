using AutoMapper;
using Template.Domain.TemplateContext.Commands;
using Template.Domain.TemplateContext.Entities;

namespace Template.Domain.AutoMapper
{
    public class CommandToDomainMappingProfile : Profile
    {
        public CommandToDomainMappingProfile()
        {
            CreateMap<InsertCargoCommand, Cargo>();
        }
    }
}
