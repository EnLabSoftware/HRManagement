using AutoMapper;
using Common.DTOs.Users;
using Business.Users;

namespace Service.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<AddUserRequest, User>();
            CreateMap<User, AddUserResponse>();
            CreateMap<User, UserInfoDTO>();
        }
    }
}
