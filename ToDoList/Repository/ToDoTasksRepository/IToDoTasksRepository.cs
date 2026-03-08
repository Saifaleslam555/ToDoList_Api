using ToDoList.Models;
using ToDoList.Repository.General_Repository;
using ToDoList.Shared.Pagination;

namespace ToDoList.Repository.ToDoTasks
{
    public interface IToDoTasksRepository:IGenericRepository<TodoTask>
    {
        public string GetStatus();
        public string ChangeStatus();

        Task<PaginatedResult<TodoTask>> GetAllTasks(PagingParam param);
    }
}
