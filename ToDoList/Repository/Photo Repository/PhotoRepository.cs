using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;
using ToDoList.Helpers;

namespace ToDoList.Repository.Photo_Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly Cloudinary _cloudinary;
        public PhotoRepository(IOptions<CloudinarySettings> config)
        {
            var account = new Account(
                
                config.Value.Cloudname,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );
            _cloudinary = new Cloudinary( account );
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
    }
}
