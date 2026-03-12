using Microsoft.Extensions.Options;
using ToDoList.Configurations;

namespace ToDoList.DTO.Register
{
    public class RegisterDTO
    {
       // private readonly IOptions<DefaultImgUrl> _defualtImgUrl;
    

        public AccountDTO accountDTO { get; set; }
        public UserProfileDTO profileDTO { get; set; }

} 
}
