namespace ToDoList.Repository.General_Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> Getlist();
        Task<T>GetById(string id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task SaveChanges();
    }
}
