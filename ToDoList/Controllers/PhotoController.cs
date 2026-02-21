using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoList.Models;
using ToDoList.Repository.Photo_Repository;

namespace ToDoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoRepository photoRepository;

        public PhotoController(IPhotoRepository photoRepository)
        {
            this.photoRepository = photoRepository;
        }

        [HttpPost("Addphoto")]
        public async Task<IActionResult> AddPhoto(IFormFile file) 
        {
            var result=await photoRepository.UploadImageAsync(file);

            if(result.Error !=null) return BadRequest(result.Error);

            var photo = new Photo();

            photo.url = result.SecureUrl.AbsoluteUri;
            photo.publicID = result.PublicId;

            return Ok(photo);
              
            
        }
    }
}
