using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Repository.General_Repository;

namespace ToDoList.Repository.Profile_Repository
{
    public class ProfileRepository:GenericRepository<UserProfile>, IProfileRepository
    {
        private readonly ToDoDBcontext context;

        public ProfileRepository(ToDoDBcontext context):base(context)
        {
            this.context = context;
        }
    }
}
