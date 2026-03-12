using AutoMapper;
using OMS_Backend.Models;

namespace OMS_Backend.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile() {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash,
                opt => opt.MapFrom(src => src.Password));
        }
    }
}
