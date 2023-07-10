using AutoMapper;
using MakeYourHomework.AuthService.Dtos;
using MakeYourHomework.AuthService.Models;

namespace MakeYourHomework.AuthService.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Nickname, opt => opt.MapFrom(src => src.Nickname))
            .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.UserType));
    }
}
