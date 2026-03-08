namespace ToDoList.Shared.Pagination
{
    public class PagingParam
    {
        private const int MaxValue = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;

        public int PageSize 
        {
            get => _pageSize;
            set => _pageSize = value > MaxValue ? MaxValue : value;
        }
    }
}
