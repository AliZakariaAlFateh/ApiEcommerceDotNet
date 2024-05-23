using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WebApiCrud.Repository.Abstract;

namespace WebApiCrud.Repository.Implementation
{
    public class FileServices:IFileServices
    {
        private readonly string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

        public string UploadImageAPI(IFormFile file,string existingImageName = null) //method to upload image in my site
        {
            string ImageName = null;
            string filePath = null;

            //var file = Request.Form.Files[0]; if i use a function without parameter...
            //file.Length > 0
            if (file != null)
            {

                var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                if (existingImageName != null)
                {

                    ImageName = file.FileName;
                    filePath = Path.Combine(_uploadsFolder, existingImageName);
                    // If the file already exists, delete it before replacing
                    //if (File.Exists(filePath))
                    //{
                    //    File.Delete(filePath);
                    //}


                }
                else
                {
                    ImageName = file.FileName;
                }

                //i am cancled the Guid to prevent the repeated images....
                ImageName = Guid.NewGuid().ToString() + "_" + filename;
                filePath = Path.Combine(_uploadsFolder, ImageName);
                var dbPath = Path.Combine(_uploadsFolder, ImageName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
               

            }
            return ImageName;


        }
    }
}
