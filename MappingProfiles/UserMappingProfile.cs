using AutoMapper;
using OMS_Backend.DTOs.User;
using OMS_Backend.Models;

namespace OMS_Backend.MappingProfiles
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.Email,
                    opt => opt.MapFrom(src => src.Email.Trim().ToLower()));

            CreateMap<User, UserResponseDto>()
                .ForMember(dest => dest.UserRoleName,
                    opt => opt.MapFrom(src => src.UserRole != null ? src.UserRole.RoleName : null));

            CreateMap<UpdateUserDto, User>()
                .ForAllMembers(opts =>
                    opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}