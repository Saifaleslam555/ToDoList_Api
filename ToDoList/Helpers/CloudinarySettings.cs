using System.ComponentModel.DataAnnotations;

namespace ToDoList.Helpers
{
    public class CloudinarySettings
    {
        [Required]
        public string Cloudname { get; set; }
        [Required]
        public string ApiKey { get; set; }
        [Required]
        public string ApiSecret { get; set; }
    }
}
