using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ToDoList.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string? address { get; set; }
    }
}
