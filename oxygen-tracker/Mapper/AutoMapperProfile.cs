using AutoMapper;
using oxygen_tracker.Models;
using oxygen_tracker.Settings.Models;

namespace oxygen_tracker.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, UserDetail>();
        }
    }
}