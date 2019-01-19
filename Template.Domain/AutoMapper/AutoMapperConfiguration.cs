using AutoMapper;

namespace Template.Domain.AutoMapper
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(ps =>
            {
                ps.AddProfile(new CommandToDomainMappingProfile());
            });
        }
    }
}
