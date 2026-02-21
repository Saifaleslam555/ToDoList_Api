using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.DTO;
using ToDoList.Models;
using ToDoList.Repository.ToDoTasks;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly IToDoTasksRepository toDoTasksRepository;

        public ToDoTaskController(IToDoTasksRepository toDoTasksRepository)
        {
            this.toDoTasksRepository = toDoTasksRepository;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateTask([FromBody] TodoTask Task)
        {
            if (Task == null)
            {
                return BadRequest();
            }

            await toDoTasksRepository.Add(Task);

            await toDoTasksRepository.SaveChanges();

            return Ok(new { message = "done", data = Task });

        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditTask(int id, [FromBody] TaskDTO UpdatedTask)
        {

            var TaskById = await toDoTasksRepository.GetById(id);

            if (TaskById == null)
            {
                return NotFound("not found");
            }

            //  var UpdatedTask = new TaskDTO();

            TaskById.Name = UpdatedTask.Name;
            TaskById.deadline_date = UpdatedTask.deadline_date;
            TaskById.IsCompleted = UpdatedTask.IsCompleted;
            TaskById.categoryID = UpdatedTask.categoryID;

            await toDoTasksRepository.Update(TaskById);
            await toDoTasksRepository.SaveChanges();

            return Ok(TaskById);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var taskbyid = await toDoTasksRepository.GetById(id);

            if (taskbyid == null)
            {
                return NotFound("not found");
            }

            await toDoTasksRepository.Delete(taskbyid);
            await toDoTasksRepository.SaveChanges();

            return Ok("delete completed");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ShowTask(int id) 
        {
            var taskbyid=await toDoTasksRepository.GetById(id);

            if (taskbyid == null)
              return NotFound("not found");
            
            return Ok(taskbyid);
        }

    }
}
