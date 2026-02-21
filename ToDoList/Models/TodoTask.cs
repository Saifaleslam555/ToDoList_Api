using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Models
{
    public class TodoTask
    {  
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? categoryID { get; set; }
        public DateTime deadline_date { get; set; }
        public string Priority { get; set; }
        public bool IsCompleted { get; set; }
        [ForeignKey("categoryID")]
        [System.Text.Json.Serialization.JsonIgnore]
        public category? Category { get; set; }

    }
}
