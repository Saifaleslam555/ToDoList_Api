using ToDoList.Models;
using ToDoList.Repository.General_Repository;

namespace ToDoList.Repository.ToDoTasks
{
    public interface IToDoTasksRepository:IGenericRepository<TodoTask>
    {
        public string GetStatus();
        public string ChangeStatus();
    }
}
