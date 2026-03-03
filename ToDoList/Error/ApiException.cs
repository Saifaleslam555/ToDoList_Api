namespace ToDoList.Error
{
    public class ApiException
    {
        public ApiException(int _StatusCode,string _message,string _details)
        {
            StatusCode = _StatusCode;
            Message = _message;
            Details = _details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? Details { get; set; }
    }
}
