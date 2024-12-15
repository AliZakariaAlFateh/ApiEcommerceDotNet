using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using NuGet.Protocol.Core.Types;
using System.IO;
using System.Net.Http.Headers;
using WebApiCrud.Entyites;
using WebApiCrud.Models;
using WebApiCrud.Repository.Abstract;
using WebApiCrud.ViewModels;

namespace WebApiCrud.Controllers
{   //[action]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        MyDbContext db;
        private object product;
        private readonly IFileServices FileRepo;

        public ProductController(MyDbContext _db, IFileServices _FileRepo)
        {
            db = _db;
            FileRepo= _FileRepo;
        }
        [HttpGet]
        //[Authorize]
        public IActionResult GetAll()
        {

            //List<Product> products = db.Products.ToList();
            //if (products.Count > 0)
            //{

            //    return Ok(products);
            //}
            //return NotFound("No Data Found");

            try
            {

                List<Product> products = db.Products.ToList();
                if (products.Count > 0)
                {
                    return Ok(products);
                }
                return NotFound("No Data Found");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            //List <Product> products = new List<Product>();

            //return Content("ALi ZalGuhny");
        }

        //public ActionResult<List <Product>> GetData() {

        //    //List <Product> products = new List<Product>();
        //    List<Product> products = db.Products.ToList();
        //    return products;
        //    //return Content("ALi ZalGuhny");
        //}
        //[HttpGet("{id}")] if i wnat two method with the same name ....
        [HttpGet("{id:int}",Name="GetById")]
        public IActionResult Details(int id)
        {

            try
            {
                var model = db.Products.Find(id);
                if (model == null)
                {
                    return NotFound($"This Product Not Found with id : {id}");
                }
                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }




        //[HttpGet]
        //public JsonResult GetAllForShow()
        //{
        //    var member = repostory.GetAll();
        //    return Json(new { member });
        //}


        [HttpGet("{name:alpha}")]
        public IActionResult Details(string name)
        {

            try
            {
                var model = db.Products.FirstOrDefault(d=>d.ProductName==name);
                if (model == null)
                {
                    return NotFound($"This Product Not Found with name : {name}");
                }
                return Ok(model);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        private readonly string _uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        [AllowAnonymous]
        [HttpPost]
        public string UploadImage(IFormFile image) //method to upload image in my site
        {
            string uploadsFolder = Path.Combine(_uploadsFolder);
            string ImageName = Guid.NewGuid().ToString() + "_" + image.FileName;
            string filePath = Path.Combine(uploadsFolder, ImageName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            //return ImageName;
            return "Success";
        }



        //[AllowAnonymous]
        [HttpPost]
        public IActionResult UploadImage2(IFormFile file) //method to upload image in my site
        {
            //var file = Request.Form.Files[0]; if i use a function without parameter...
            if (file.Length > 0)
            {

                var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                string ImageName = Guid.NewGuid().ToString() + "_" + filename;
                var fullpath = Path.Combine(_uploadsFolder, ImageName);
                var dbPath = Path.Combine(_uploadsFolder, ImageName);
                using (var stream = new FileStream(fullpath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok(new { dbPath });

            }
            else {
            
            return BadRequest();
                    }

        }


        //public string UploadImageService(IFormFile file) //method to upload image in my site
        //{
        //    //var file = Request.Form.Files[0]; if i use a function without parameter...
        //    if (file.Length > 0)
        //    {

        //        var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //        string ImageName = Guid.NewGuid().ToString() + "_" + filename;
        //        var fullpath = Path.Combine(_uploadsFolder, ImageName);
        //        var dbPath = Path.Combine(_uploadsFolder, ImageName);
        //        using (var stream = new FileStream(fullpath, FileMode.Create))
        //        {
        //            file.CopyTo(stream);
        //        }
        //        return ImageName;

        //    }
        //    else
        //    {

        //        return "not";
        //    }

        //}



        //[AllowAnonymous]
        [HttpPost]
        public ActionResult Post([FromForm] DtoProduct model)
        {
            
            string? ImageName = null;
            //if (model.imagefile != null)
            ImageName = FileRepo.UploadImageAPI(model.imagefile,null);
            // Generate a unique file name for the uploaded image
            //string imageName = $"{Guid.NewGuid()}_{model.imagefile.FileName}";//////

            //// Combine the uploads folder with the image file name
            //string imagePath = Path.Combine(_uploadsFolder, imageName);/////

            //// Copy the uploaded image to the specified path
            //using (var stream = new FileStream(imagePath, FileMode.Create))/////
            //{
            //    //await model.ImageFile.CopyToAsync(stream);
            //    model.imagefile.CopyTo(stream);////////////
            //}
            var newProduct=new Product();
            if (model.Id == 0 && ModelState.IsValid)
            {
                

                try
                {
                    if (model.categoryid == 0)
                    {
                        return BadRequest("This category not found ...");
                    }
                    else
                    {
                        //model.imageName = ImageName;
                        newProduct.imageName = ImageName;
                        newProduct.categoryid = model.categoryid;
                        newProduct.ProductName = model.ProductName;
                        newProduct.Price = model.Price;
                        newProduct.Qty = model.Qty;
                        newProduct.ActualCount = model.Qty;
                        //db.Add(model);
                        db.Add(newProduct);
                        db.SaveChanges();
                        //return Ok("Product is Created successfully ..");
                        //return Created("https://localhost:7269/api/Product/Details/" + model.Id, model);
                        string url = Url.Link("GetById", new { id = model.Id });
                        return Created(url, new { product = model, message = "product is created successfully.", url = url });
                        //return Ok();

                    }


                }
                catch (Exception ex)
                {

                    return BadRequest(ex.Message);
                }


            }


            //i changed the returned value
            return BadRequest(ModelState);



        }

        //[HttpPut]
        //public ActionResult Put(Product model)
        //{


        //    if (model == null || model.Id == 0)
        //    {
        //        if (model == null)
        //        {
        //            return BadRequest("invalid model");

        //        }
        //        else if (model.Id == 0)
        //        {
        //            return BadRequest("this model not found");
        //        }
        //    }
        //    try
        //    {
        //        var prod = db.Products.Find(model.Id);
        //        if (prod == null)
        //        {

        //            return NotFound("Not Found");
        //        }
        //        prod.ProductName = model.ProductName;
        //        prod.Price = model.Price;
        //        prod.Qty = model.Qty;
        //        db.SaveChanges();
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {

        //        return BadRequest(ex.Message);
        //    }
        //}















        [HttpPut("{id}")]
        //with i use constraint at Data annotation we use Route Data like Query String
        //url/id=1
        //with out i use constraint at Data annotation we use query string 
        //url/?id=1
        public ActionResult Put([FromForm] DtoProduct model, int id)
        {

            if (model == null || id == 0)
            { //model.Id == 0 

                if (model == null)
                {
                    return BadRequest("This is Invalid Product .");

                }
                else if (id == 0)
                {//model.Id

                    return BadRequest($" this Product with {model.Id} is invalid .");
                    //return BadRequest($" this Product with {id} is invalid .");
                    //return NotFound();

                }
            }



            #region New code for Update
            if (ModelState.IsValid)
            {
                try
                {
                    //var product = db.Products.AsNoTracking().FirstOrDefault(p => p.Id == model.Id);
                    var product = db.Products.Find(id);
                    if (product == null)
                    {
                        //return NotFound($"The Product with Id {model.Id} was not found.");
                        //return NotFound($"The Product with Id {id} was not found.");
                        return NotFound();

                    }

                    string? ImageName = null;
                    //if (model.imagefile != null)
                    if (model.imagefile != null)
                    {
                        ImageName = FileRepo.UploadImageAPI(model.imagefile, ImageName);
                    }
                    else
                    {
                        ImageName=product.imageName ;
                    }

                    product.ProductName = model.ProductName;
                    product.Price = model.Price;
                    product.Qty = model.Qty;
                    product.categoryid = model.categoryid;
                    product.imageName = ImageName;
                    product.ActualCount = model.Qty;
                    //db.Attach(model); // Attach the entity to the context
                    //the neaxt comment code is true ...
                    //db.Entry(product).State = EntityState.Modified; // Mark the entity as modified
                    //this code is true also ...
                    db.Update(product);
                    db.SaveChanges();
                    //return Ok("Product updated successfully.");
                    //Don't use 204 beacuse dont return the object with id and error message
                    //model.Id
                    return StatusCode(200, new { message = "Product updated successfully", id = id });
                    //return Ok();



                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {

                //return StatusCode(400, new { message = "Product you want to updated it invalid", id = id });
                return BadRequest(ModelState);

            }


            #endregion

        }

        //public ActionResult Put(Product model, int id)
        //{
        //    if (model == null || id == 0)
        //    {
        //        if (model == null)
        //        {
        //            return BadRequest("Invalid Product.");
        //        }
        //        else if (id == 0)
        //        {
        //            return BadRequest($"Invalid Product with id {model.Id}.");
        //        }
        //    }

        //    #region New code for Update

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var product = db.Products.Find(id);
        //            if (product == null)
        //            {
        //                return NotFound();
        //            }

        //            product.ProductName = model.ProductName;
        //            product.Price = model.Price;
        //            product.Qty = model.Qty;
        //            int categoryidd = model.categoryid;
        //            product.categoryid = categoryidd;
        //            // Convert categoryid to integer if provided as string
        //            if (model.categoryid != null && int.TryParse(model.categoryid, out int categoryId))
        //            {
        //                product.categoryid = categoryId;
        //            }
        //            else
        //            {
        //                return BadRequest("Invalid categoryid.");
        //            }

        //            db.Entry(product).State = EntityState.Modified;
        //            db.SaveChanges();

        //            return StatusCode(200, new { message = "Product updated successfully", id = id });
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        return StatusCode(400, new { message = "Invalid Product you want to update", id = id });
        //    }

        //    #endregion
        //}

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            try
            {
                var product = db.Products.Find(id);
                if (product == null)
                {

                    return NotFound($"this Product with id {id} Not found ..");
                    //return NotFound();
                }
                db.Products.Remove(product);
                db.SaveChanges();
                //
                return StatusCode(200 , new { message=$"Product with id {product.ProductName} is Deleted Successfully ...", prod = product });
                //return Ok(new { message=$"Product with id {product.ProductName} is Deleted Successfully ...", prod = product });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpGet]
        public IActionResult GetAllCategories()
        {

            try
            {

                var categories = db.Categories.Include(p=>p.products).ToList();
                if (categories.Count > 0)
                {
                    return Ok(categories);
                }
                return NotFound("No Data Found");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
            //List <Product> products = new List<Product>();

            //return Content("ALi ZalGuhny");
        }


        #region Old code for update
        //try
        //{
        //    var product = db.Products.Find(model.Id);
        //    if (product == null)
        //    {
        //        return NotFound($"This  Product with {model.Id} Not Found .");

        //    }
        //    product.ProductName = model.ProductName;
        //    product.Price = model.Price;
        //    product.Qty = model.Qty;

        //    //db.Update(model); we dont use update here because tracking for the two object
        //    db.SaveChanges();
        //    return Ok("Product Updated successfully ..");
        //}
        //catch (Exception ex)
        //{

        //    return BadRequest(ex.Message);
        //} 
        #endregion



        //[HttpGet("api/products")]
        [HttpGet]
        //[Authorize]
        //[Authorize(Roles = "Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ProductWithCategoryNameVM>>> GetProductsWithCategory()
        {
            try
            {
                 var products = await db.Products
                .Include(p => p.category)
                .Select(pc => new ProductWithCategoryNameVM
                {
                    Id = pc.Id,
                    productName = pc.ProductName,
                    price = pc.Price,
                    Qty = pc.Qty,
                    categoryid = pc.categoryid,
                    categoryname = pc.category.categoryName, // assuming Category is not null
                    imageName=pc.imageName
                })
                .ToListAsync();
                if (products.Count() > 0) {

                    return products;
                }

                return NotFound("No Data Found");
            }
            catch (Exception ex)
            {

                   return BadRequest(ex.Message);
            }
        }
















    }






}
