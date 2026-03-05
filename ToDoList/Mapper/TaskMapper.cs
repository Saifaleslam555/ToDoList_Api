using AutoMapper;
using ToDoList.DTO;
using ToDoList.Models;

namespace ToDoList.Mapper
{
    public class TaskMapper:Profile
    {
        public TaskMapper()
        {
            CreateMap<TodoTask,TaskDTO>().ReverseMap();
        }
    }
}
