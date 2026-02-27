using AutoMapper;
using ToDoList.DTO.Register;
using ToDoList.Models;

namespace ToDoList.Mapper
{
    public class UserProfileMapper:Profile
    {
        public UserProfileMapper()
        {
            CreateMap<UserProfile, UserProfileDTO>().ReverseMap();
        }
    }
}
