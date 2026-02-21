using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models.DBcontext
{
    public class ToDoDBcontext:IdentityDbContext<ApplicationUser>
    {
        public ToDoDBcontext(DbContextOptions options) : base(options)
        {
        }

        protected ToDoDBcontext()
        {
        }

        public DbSet<TodoTask> tasks { get; set; }
        public DbSet<category>categories { get; set; }

        public DbSet<Photo> photos { get; set; } 
    }
}
