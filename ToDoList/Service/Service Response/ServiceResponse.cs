
namespace ToDoList.Service.Service_Response
{
    public class ServiceResponse 
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Error { get; set; }
    }
}
