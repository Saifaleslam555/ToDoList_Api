using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using ToDoList.Configurations;

namespace ToDoList.DTO.Register
{
    public class UserProfileDTO
    {
        public string DisplayName { get; set; }
        public IFormFile Img { get; set; }
   
    }
}
