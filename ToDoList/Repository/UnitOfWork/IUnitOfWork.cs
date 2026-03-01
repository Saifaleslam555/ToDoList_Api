using ToDoList.Repository.Photo_Repository;
using ToDoList.Repository.Profile_Repository;
using ToDoList.Repository.ToDoTasks;

namespace ToDoList.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        //public IPhotoRepository photoRepository { get;}
        public IProfileRepository profileRepository { get;}
        public IToDoTasksRepository toDoTasksRepository { get;}
        public Task<bool> Complete();
        public bool HasChanges();
    }
}
