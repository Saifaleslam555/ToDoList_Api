using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Data;
using ToDoList.DTO.Register;
using ToDoList.Models;
using ToDoList.Repository.Photo_Repository;
using ToDoList.Repository.Profile_Repository;
using ToDoList.Server.Interfaces;
using ToDoList.Service.Service_Response;

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
                        var ImgFromUser =
                             await _photoRepository.UploadImageAsync(UserFromRequest.profileDTO.Img);

                        var ImgUrlFromUser = ImgFromUser.SecureUrl.AbsoluteUri;

                        var profile = _mapper.Map<UserProfile>(UserFromRequest.profileDTO);
                        profile.AccountID = user.Id;
                        profile.imgUrl = ImgUrlFromUser;

                        await _profileRepository.Add(profile);
                        await _profileRepository.SaveChanges();

                        response.IsSuccess=true;
                response.Message = "the process is done";
                    }
                    catch (Exception ex)
                    {

                        await _userManager.DeleteAsync(user);
                        //return StatusCode(500, ex.Message);
                        response.IsSuccess = false;
                        response.Message= ex.Message;

                    }
            
            return response;    
                }
            
         

       }
      
            //return BadRequest(ModelState);
}

