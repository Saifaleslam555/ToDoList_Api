using System.ComponentModel.DataAnnotations;

namespace ToDoList.Configurations
{
    public class DefaultImgUrl
    {
        public const string Section = "DefaultImg";
        [Required]
        public required string Url { get; set; }
    }
}
