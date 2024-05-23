namespace WebApiCrud.Repository.Abstract
{
    public interface IFileServices
    {
        public string UploadImageAPI(IFormFile image,string existingImageName = null);
    }
}
