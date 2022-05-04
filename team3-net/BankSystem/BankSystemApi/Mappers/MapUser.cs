using AutoMapper;
using BankSystemApi.DTO;
using BankSystemApi.Models;

namespace BankSystemApi.Mappers
{
    public static class MapUser
    {
        public static UserAuthenticate ToUserAuthenticate(this User user)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserAuthenticate>()
                    .ForMember("Login", opt => opt.MapFrom(c => c.Login))
                    .ForMember("Password", opt => opt.MapFrom(src => src.Password))
                    .ForMember("Role", opt => opt.MapFrom(src => new Role 
                    {
                        Id=src.RoleId,
                        RoleName=src.Role.RoleName
                    })));
            var mapper = new Mapper(config);
            return mapper.Map<User, UserAuthenticate>(user);
        }

        public static User ToUser(this UserDTO user)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>()
                    .ForMember("Login", opt => opt.MapFrom(c => c.Login))
                    .ForMember("Password", opt => opt.MapFrom(src => src.Password)));
            var mapper = new Mapper(config);
            return mapper.Map<UserDTO, User>(user);
        }
    }
}
