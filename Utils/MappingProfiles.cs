using AutoMapper;
using Hackathon.Models;
using Hackathon.Dto;

namespace Hackathon.Utils
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserGetDto>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
