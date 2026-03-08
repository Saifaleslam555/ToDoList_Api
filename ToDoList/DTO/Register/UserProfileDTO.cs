using System.Text.Json.Serialization;

namespace ToDoList.DTO.Register
{
    public class UserProfileDTO
    {
        public string DisplayName { get; set; }
        public IFormFile? Img { get; set; }
        //[System.Text.Json.Serialization.JsonIgnore]
        //public string? imgUrl { get; set; }
    }
}
