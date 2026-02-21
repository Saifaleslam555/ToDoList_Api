using CloudinaryDotNet.Actions;

namespace ToDoList.Repository.Photo_Repository
{
    public interface IPhotoRepository
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);

        Task<DeletionResult> DeleteImageAsync(string publicID);
       
    }
}
