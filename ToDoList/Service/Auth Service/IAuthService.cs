using ToDoList.DTO;
using ToDoList.DTO.Register;
using ToDoList.Service.Service_Response;

namespace ToDoList.Server.Interfaces
{
    public interface IAuthService
    {
        Task<ServiceResponse> RegisterService(RegisterDTO _registerDTO);
        Task<ServiceResponse> LoginService(LoginDTO _loginDTO);
    }
}
