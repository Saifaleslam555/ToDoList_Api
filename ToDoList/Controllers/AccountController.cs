using Microsoft.AspNetCore.Mvc;
using ToDoList.DTO;
using ToDoList.DTO.Register;
using ToDoList.Server.Interfaces;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService _authService)
        {
            this._authService = _authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO UserFromRequest) 
        {
            if (!ModelState.IsValid) return BadRequest();

            var result= await _authService.RegisterService(UserFromRequest);

            if (result.IsSuccess) return Ok(result);
            
            if (result.Error.Any())  return BadRequest(result);

            return StatusCode(500, result);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> login([FromBody] LoginDTO UserFromRequest)
        {
            if (!ModelState.IsValid) return BadRequest(); 

            var result=await _authService.LoginService(UserFromRequest);

            if (result.IsSuccess) return Ok(result);

            if (result.Error.Any()) return BadRequest(result); 

            return StatusCode(500, result);
        }
    }
}
