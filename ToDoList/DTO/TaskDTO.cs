namespace ToDoList.DTO
{
    public class TaskDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int categoryID { get; set; }
        public DateTime deadline_date { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
    }
}
