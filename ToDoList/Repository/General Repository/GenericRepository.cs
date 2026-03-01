
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoList.Data;

namespace ToDoList.Repository.General_Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ToDoDBcontext context;
        private DbSet<T> _dbset;

        public GenericRepository(ToDoDBcontext context)
        {
            this.context = context;
            this._dbset = context.Set<T>();
        }

        public async Task Add(T entity)
        {
            await  _dbset.AddAsync(entity);
        }

        public Task Delete(T entity)
        {
             _dbset.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<T> GetById(string id)
        {
            return await _dbset.FindAsync(id);
        }

        public async Task<IEnumerable<T>> Getlist()
        {
            return await _dbset.ToListAsync();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            _dbset.Update(entity);
            return Task.CompletedTask;
        }
    }
}
