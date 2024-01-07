using SchoolSystem.Service.Contract.Mappers;

namespace SchoolSystem.API.MapperConfiguration
{
    public static class MapperConfiguration
    {
        public static IServiceCollection AddMappersProfiles(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddAutoMapper(typeof(IdentityUserToRegisterModelMapper));
            return serviceDescriptors;
        }
    }
}
