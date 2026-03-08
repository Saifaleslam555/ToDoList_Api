using ToDoList.Data;
using ToDoList.Models;
using ToDoList.Repository.General_Repository;
using ToDoList.Shared.Pagination;

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

        public async Task<PaginatedResult<TodoTask>> GetAllTasks(PagingParam param)
        {
            var quary = context.tasks.AsQueryable();

            return await PaginationHelpers.CreateAsync(quary,param.PageNumber,param.PageSize);
        }

        public string GetStatus()
        {
            throw new NotImplementedException();
        }
    }
}
