using ToDoList.Models;
using ToDoList.Models.DBcontext;
using ToDoList.Repository.General_Repository;

namespace ToDoList.Repository.ToDoTasks
{
    public class ToDoTasksRepository : GenericRepository<TodoTask>, IToDoTasksRepository
    {
        private readonly ToDoDBcontext context;

        public ToDoTasksRepository(ToDoDBcontext context):base(context)
        {
            this.context = context;
        }

        public string ChangeStatus()
        {
            throw new NotImplementedException();
        }

        public string GetStatus()
        {
            throw new NotImplementedException();
        }
    }
}
