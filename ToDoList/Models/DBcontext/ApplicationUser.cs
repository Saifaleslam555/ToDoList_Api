using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ToDoList.Models.DBcontext
{
    public class ApplicationUser:IdentityUser
    {
        public string address { get; set; }
    }
}
