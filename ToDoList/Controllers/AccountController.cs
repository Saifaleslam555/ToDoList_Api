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
using ToDoList.DTO;
using ToDoList.Models.DBcontext;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<ApplicationUser> userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO UserFromRequest) 
        {
            if (ModelState.IsValid)
            {

                ApplicationUser user = new ApplicationUser();
                
                //UserFromRequest.Email = user.Email;
                //UserFromRequest.username = user.UserName;

                user.Email=UserFromRequest.Email;
                user.UserName = UserFromRequest.username;

                //test
                user.address = "default";
               
                IdentityResult identityResult=
                    await userManager.CreateAsync(user, UserFromRequest.Password);
                
                if (identityResult.Succeeded) { 
                
                    return Ok(IdentityResult.Success);
                }
                
                foreach (var item in identityResult.Errors) {

                    ModelState.AddModelError("Password", item.Description);
                }

            }
            return BadRequest(ModelState);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> login([FromBody] LoginDTO UserFromRequest) 
        {
            if (ModelState.IsValid) 
            {
                ApplicationUser UserFromDB = 
                    await userManager.FindByEmailAsync(UserFromRequest.Email);
                if (UserFromDB != null) 
                {
                    bool found = 
                        await userManager.CheckPasswordAsync(UserFromDB, UserFromRequest.Password);
                    if (found==true) 
                    {
                        //token claims
                        List<Claim> UserClaims=new List<Claim>();
                        UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, UserFromDB.Id));
                        UserClaims.Add(new Claim(ClaimTypes.Name, UserFromDB.UserName));
                        var UserRoles =await userManager.GetRolesAsync(UserFromDB);
                        foreach (var RoleName in UserRoles) 
                        {
                            UserClaims.Add(new Claim(ClaimTypes.Role,RoleName));
                        }

                        //security key
                        SymmetricSecurityKey signkey = 
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Secretkey"]));

                        //signing credinitals
                        SigningCredentials credentials = 
                            new SigningCredentials(signkey, SecurityAlgorithms.HmacSha256);

                        //token
                        JwtSecurityToken mytoken = new JwtSecurityToken(

                            issuer: configuration["Jwt:issuerID"],
                            audience: configuration["Jwt:audienceID"],
                            claims: UserClaims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: credentials

                        );

                        //token response
                        return Ok(new 
                        {
                            token=new JwtSecurityTokenHandler().WriteToken(mytoken),
                            expiration=mytoken.ValidTo
                        }
                        );
                         

                    }
                }
                ModelState.AddModelError("UserName", "UserName or Password is invalid");
            }
            return BadRequest(ModelState);
        }

    }
}
