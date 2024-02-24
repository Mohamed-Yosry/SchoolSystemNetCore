using AutoMapper;
using SchoolSystem.Service.Contract.Mappers;

namespace SchoolSystem.API.AutoMapperConfiguration
{
    public static class AutoMapperConfiguration
    {
        public static IMapper AddMappersProfiles()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new CoursesMappingProfile());
                mc.AddProfile(new IdentityUserToRegisterModelMapper());
                mc.AddProfile(new RoleMappingProfile());
                mc.AddProfile(new UsersMappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            return mapper;
        }
    }
}
