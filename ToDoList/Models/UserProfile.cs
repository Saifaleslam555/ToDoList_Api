using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoList.Data;

namespace ToDoList.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string imgUrl { get; set; }
        public string AccountID { get; set; }

        [ForeignKey("AccountID")]
        public ApplicationUser User { get; set; }


        
    }
}
