using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApiCrud.Entyites;
using WebApiCrud.Models;
using WebApiCrud.Repository.Abstract;
using WebApiCrud.ViewModels;

namespace WebApiCrud.Repository.Implementation
{
    public class CategoryRepo : ICategoryRepo
    {

        private readonly MyDbContext _db;

        public CategoryRepo(MyDbContext db)
        {
            _db=db;
        }



        public List<Category> GetAll()
        {
             var Categories=_db.Categories.Include(p => p.products).ToList();
             return Categories ?? new();
        }

        public Category GetById(int? id)
        {
            var category = _db.Categories.Where(c => c.Id == id).FirstOrDefault();
            return category ?? new();
        }
        //dtoCategory
        public int InsertCategory(Category category)
        {
            //var category = new Category
            //{
            //    categoryName = dtoCategory.categoryName,
            //    //catimageName = dtoCategory.catimageName,
            //    // Handle file mapping if needed
            //};
            
            _db.Categories.Add(category);  
            return _db.SaveChanges();
        }

        public int UpdateCatergory(Category category,int id)
        {
            try
            {
                // Ensure that the entity is being tracked by EF Core
                var existingCategory = _db.Categories.Find(id);
                if (existingCategory == null)
                {
                    // Category with the given Id does not exist in the database
                    return 0;
                }

                // Exclude the Id property from being modified
                _db.Entry(existingCategory).Property(x => x.Id).IsModified = false;

                // Update the properties of the existing category entity
                _db.Entry(existingCategory).CurrentValues.SetValues(category);

                // Save changes to the database
                _db.SaveChanges();

                return 1;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                return 0;
            }
        }


        public int DeleteCategory(int? id)
        {
            var category = _db.Categories.Where(c => c.Id == id).FirstOrDefault();
            if (category != null)
            {
                _db.Categories.Remove(category);
                try
                {
                    _db.SaveChanges();
                    return (1);
                }
                catch {

                    return (-1);

                }
                //return (1);
            }
            else {
                return (-1);
            }

        }
    }
}
