using Microsoft.EntityFrameworkCore;
using ToDoList.Data;
using ToDoList.Helpers;
using ToDoList.Repository.Photo_Repository;
using ToDoList.Repository.Profile_Repository;
using ToDoList.Repository.ToDoTasks;

namespace ToDoList.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ToDoDBcontext context;
        private readonly CloudinarySettings cloudinarySettings;
        private IPhotoRepository _photoRepository;
        private IProfileRepository _profileRepository;
        private IToDoTasksRepository _ToDoTasksRepository;

        public UnitOfWork(ToDoDBcontext context)
        {
            this.context = context;
        } 

        public IProfileRepository profileRepository => _profileRepository 
            ??= new ProfileRepository(context);

        public IToDoTasksRepository toDoTasksRepository => _ToDoTasksRepository 
            ??= new ToDoTasksRepository(context);

        public async Task<bool> Complete()
        {
            try
            {
                return await context.SaveChangesAsync() > 0;
            }
            catch (DbUpdateException ex) 
            {
                throw new Exception("error while saving changes", ex);
            }
        }

        public bool HasChanges()
        {
            return context.ChangeTracker.HasChanges();
        }
    }
}
