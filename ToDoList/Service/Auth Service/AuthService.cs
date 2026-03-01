using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.DTO;
using ToDoList.DTO.Register;
using ToDoList.Models;
using ToDoList.Repository.Photo_Repository;
using ToDoList.Repository.Profile_Repository;
using ToDoList.Server.Interfaces;
using ToDoList.Service.Service_Response;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;


namespace ToDoList.Service.Auth_Service
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IProfileRepository _profileRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthService(UserManager<ApplicationUser> _userManager, IProfileRepository _profileRepository,
            IPhotoRepository _photoRepository, IConfiguration _configuration, IMapper _mapper)
        {
            this._userManager = _userManager;
            this._profileRepository = _profileRepository;
            this._photoRepository = _photoRepository;
            this._configuration = _configuration;
            this._mapper = _mapper;
        }

        public async Task<ServiceResponse> RegisterService(RegisterDTO UserFromRequest)
        {
            var response = new ServiceResponse(); 

            ApplicationUser user = new ApplicationUser();

            var ImgFromUser =
                    await _photoRepository.UploadImageAsync(UserFromRequest.profileDTO.Img);
            //var ImgUrlFromUser = ImgFromUser.SecureUrl.AbsoluteUri;

            user.Email = UserFromRequest.accountDTO.Email;
            user.UserName = UserFromRequest.accountDTO.username;

            IdentityResult identityResult =
                  await _userManager.CreateAsync(user, UserFromRequest.accountDTO.Password);

            if (!identityResult.Succeeded)
            {
                response.IsSuccess = false;
                response.Error = identityResult.Errors.Select(e => e.Description).ToList();
                return response;
            }

            try
            {
                var profile = _mapper.Map<UserProfile>(UserFromRequest.profileDTO);
                profile.AccountID = user.Id;
                profile.imgUrl = ImgFromUser.PublicId;

                await _profileRepository.Add(profile);
                await _profileRepository.SaveChanges();

                response.IsSuccess = true;
                response.Message = "the process is done";
            }
            catch (Exception ex)
            {
                await _photoRepository.DeleteImageAsync(ImgFromUser.PublicId);

                await _userManager.DeleteAsync(user);
                 
                //return StatusCode(500, ex.Message);
                response.IsSuccess = false;
                response.Message = ex.Message;

            }
            return response;    
        }

        public async Task<ServiceResponse> LoginService(LoginDTO UserFromRequest) 
        {
            var response = new ServiceResponse();

            ApplicationUser UserFromDB =
                await _userManager.FindByEmailAsync(UserFromRequest.Email);

            if (UserFromDB != null)
            {
                bool found =
                    await _userManager.CheckPasswordAsync(UserFromDB, UserFromRequest.Password);

                if (found == true)
                {
                    //token claims
                    List<Claim> UserClaims = new List<Claim>();
                    UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                    UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, UserFromDB.Id));
                    UserClaims.Add(new Claim(ClaimTypes.Name, UserFromDB.UserName));
                    var UserRoles = await _userManager.GetRolesAsync(UserFromDB);
                    foreach (var RoleName in UserRoles)
                    {
                        UserClaims.Add(new Claim(ClaimTypes.Role, RoleName));
                    }

                    //security key
                    SymmetricSecurityKey signkey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secretkey"]));

                    //signing credinitals
                    SigningCredentials credentials =
                        new SigningCredentials(signkey, SecurityAlgorithms.HmacSha256);

                    //token
                    JwtSecurityToken mytoken = new JwtSecurityToken(

                        issuer: _configuration["Jwt:issuerID"],
                        audience: _configuration["Jwt:audienceID"],
                        claims: UserClaims,
                        expires: DateTime.Now.AddHours(1),
                        signingCredentials: credentials

                    );

                    response.IsSuccess = true;
                    response.Message = "the login is done";

                    response.Data = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(mytoken),
                        expiration = mytoken.ValidTo
                    };

                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "your username or passwoed is wrong";
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "your is data is not compelete";
            }

            return response;
        }
                            
    }


}
      


