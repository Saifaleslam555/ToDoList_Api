using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ToDoList.Shared.Pagination
{
    public class PaginatedResult<T>
    {
        public PaginationMetaData metaData { get; set; } = default!;
        public List<T> items { get; set; } = [];
        
    }

    public class PaginationMetaData 
    {
        public int CurnnetPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }

    public class PaginationHelpers 
    {
        public static async Task<PaginatedResult<T>> CreateAsync<T>(IQueryable<T> query,
            int pageNumber, int pageSize) 
        {
            var count= await query.CountAsync();
            var items=await query.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();

            return new PaginatedResult<T>
            {
                metaData = new PaginationMetaData
                {
                    CurnnetPage = pageNumber,
                    TotalPage = (int)Math.Ceiling(count / (double)pageSize),
                    PageSize = pageSize,
                    TotalCount = count

                },
                items = items

            };
        } 
    }
}
