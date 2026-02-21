namespace ToDoList.Models
{
    public class category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int userID { get; set; }
        public List<TodoTask> tasks { get; set; }
    }
}
