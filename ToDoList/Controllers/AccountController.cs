using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

//using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Data;
using ToDoList.DTO;
using ToDoList.DTO.Register;
using ToDoList.Models;
using ToDoList.Repository.Photo_Repository;
using ToDoList.Repository.Profile_Repository;
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
            if (!ModelState.IsValid) { return BadRequest(); }

            var result= await _authService.RegisterService(UserFromRequest);

            if (result.IsSuccess) 
            {
                return Ok(result);
            }

            if (result.Error.Any()) 
            {
                return BadRequest(result);
            }

            return StatusCode(500, result);

        }

        #region login_not_done
        //[HttpPost("Login")]
        //public async Task<IActionResult> login([FromBody] LoginDTO UserFromRequest) 
        //{
        //    if (ModelState.IsValid) 
        //    {
        //        ApplicationUser UserFromDB = 
        //            await userManager.FindByEmailAsync(UserFromRequest.Email);
        //        if (UserFromDB != null) 
        //        {
        //            bool found = 
        //                await userManager.CheckPasswordAsync(UserFromDB, UserFromRequest.Password);
        //            if (found==true) 
        //            {
        //                //token claims
        //                List<Claim> UserClaims=new List<Claim>();
        //                UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //                UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, UserFromDB.Id));
        //                UserClaims.Add(new Claim(ClaimTypes.Name, UserFromDB.UserName));
        //                var UserRoles =await userManager.GetRolesAsync(UserFromDB);
        //                foreach (var RoleName in UserRoles) 
        //                {
        //                    UserClaims.Add(new Claim(ClaimTypes.Role,RoleName));
        //                }

        //                //security key
        //                SymmetricSecurityKey signkey = 
        //                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secretkey"]));

        //                //signing credinitals
        //                SigningCredentials credentials = 
        //                    new SigningCredentials(signkey, SecurityAlgorithms.HmacSha256);

        //                //token
        //                JwtSecurityToken mytoken = new JwtSecurityToken(

        //                    issuer: configuration["Jwt:issuerID"],
        //                    audience: configuration["Jwt:audienceID"],
        //                    claims: UserClaims,
        //                    expires: DateTime.Now.AddHours(1),
        //                    signingCredentials: credentials

        //                );

        //                //token response
        //                return Ok(new 
        //                {
        //                    token=new JwtSecurityTokenHandler().WriteToken(mytoken),
        //                    expiration=mytoken.ValidTo
        //                }
        //                );
                         

        //            }
        //        }
        //        ModelState.AddModelError("UserName", "UserName or Password is invalid");
        //    }
        //    return BadRequest(ModelState);
        //}
        #endregion
    }
}
