using System.ComponentModel.DataAnnotations;

namespace ToDoList.DTO.Register
{
    public class AccountDTO
    {
        public string Email { set; get; }
        public string username { set; get; }
        public string Password { set; get; }
    }
}
