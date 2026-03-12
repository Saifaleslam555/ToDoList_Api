using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using ToDoList.Configurations;
using ToDoList.Helpers;

namespace ToDoList.Repository.Photo_Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly Cloudinary _cloudinary;
        private readonly IOptions<DefaultImgUrl> defualtImgUrl;

        public PhotoRepository(IOptions<CloudinarySettings> config,IOptions<DefaultImgUrl> defualtImgUrl)
        {
            var account = new Account(
                
                config.Value.Cloudname,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary( account );
            this.defualtImgUrl = defualtImgUrl;
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicID)
        {
            var deleteprem = new DeletionParams(publicID);

            return await _cloudinary.DestroyAsync(deleteprem);
        }

        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            var UploadResult = new ImageUploadResult();

            if (file.Length > 0) 
            {
                var stream = file.OpenReadStream();
                var UploadPrem = new ImageUploadParams
                {
                    File = new FileDescription(file.Name, stream),
                    Folder="ToDolist_folder"
                };
                UploadResult=await _cloudinary.UploadAsync(UploadPrem);
            }

            return UploadResult;
        }

        public string GetImgUrl(ImageUploadResult img) 
        {
            return img.SecureUrl.AbsoluteUri;
        }

        public Task<string> DefualtImg()
        {
            var imgPulbicID = defualtImgUrl.Value.Url;

            var img = _cloudinary.Api.UrlImgUp.BuildUrl(imgPulbicID);

            return Task.FromResult(img);

        }
    }
}
