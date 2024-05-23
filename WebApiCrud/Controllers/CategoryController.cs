using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiCrud.Entyites;
using WebApiCrud.Models;
using WebApiCrud.Repository.Abstract;
using WebApiCrud.ViewModels;

namespace WebApiCrud.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo _Catrepo;

        private readonly MyDbContext _db;
        private readonly IFileServices _FileRepo;
        public CategoryController(ICategoryRepo Catrepo , MyDbContext db, IFileServices fileRepo)
        {
            _Catrepo = Catrepo;
            _db = db;
            _FileRepo = fileRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Categories = _Catrepo.GetAll();
            return Ok(Categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int? id)
        {
            return Ok(_Catrepo.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> InsertCategory([FromForm] Category Category)
        {
            string? ImageName = null;
            if (Category.catimagefile != null)
            {
                ImageName = _FileRepo.UploadImageAPI(Category.catimagefile);
                Category.catimageName= ImageName;
            }
            if (ModelState.IsValid) {

                _Catrepo.InsertCategory(Category);
                return Ok(Category);
            }
            return Ok(ModelState);

        }


        //[HttpPost]
        //public async Task<IActionResult> InsertCategory(Category Category)
        //{
        //    _db.Categories.Add(Category);
        //    _db.SaveChanges();
        //    return Ok(Category);
        //}




        //[HttpPut]
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory([FromForm] Category Category,int id)
        {
            _Catrepo.UpdateCatergory(Category,id);
            return Ok(Category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            var result = _Catrepo.DeleteCategory(id);
            if (result == 1)
            {
                return Ok(new { message = "Successfully Deleted" });
            }
            else
            {
                return BadRequest(new { message = "Failed to delete category" });
            }
        }
    }
}
