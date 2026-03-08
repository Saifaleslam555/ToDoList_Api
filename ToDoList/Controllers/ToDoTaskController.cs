using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.DTO;
using ToDoList.Models;
using ToDoList.Repository.ToDoTasks;
using ToDoList.Repository.UnitOfWork;
using ToDoList.Shared.Pagination;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoTaskController : ControllerBase
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper _mapper;

        public ToDoTaskController(IUnitOfWork uow,IMapper _mapper)
        {
            this.uow = uow;
            this._mapper = _mapper;

        }

        [HttpPost("Create")]      
        public async Task<IActionResult> CreateTask([FromBody] TodoTask Task)
        {
            if (Task == null)
            {
                return BadRequest();
            }

            await uow.toDoTasksRepository.Add(Task);

            await uow.Complete();

            return Ok(new { message = "done", data = Task });

        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditTask(int id, [FromBody] TaskDTO UpdatedTask)
        {

            var TaskById = await uow.toDoTasksRepository.GetById(id);

            if (TaskById == null)
            {
                return NotFound("not found");
            }

            _mapper.Map(UpdatedTask,TaskById);

            await uow.toDoTasksRepository.Update(TaskById);
            await uow.Complete();

            return Ok(TaskById);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var taskbyid = await uow.toDoTasksRepository.GetById(id);

            if (taskbyid == null)
            {
                return NotFound("not found");
            }

            await uow.toDoTasksRepository.Delete(taskbyid);
            await uow.Complete();

            return Ok("delete completed");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ShowTask(int id) 
        {
            var taskbyid=await uow.toDoTasksRepository.GetById(id);

            if (taskbyid == null)
              return NotFound("not found");
            
            return Ok(taskbyid);
        }
        [HttpPost("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks([FromQuery]PagingParam param) 
        {

            return Ok(await uow.toDoTasksRepository.GetAllTasks(param));
        }
    }
}
