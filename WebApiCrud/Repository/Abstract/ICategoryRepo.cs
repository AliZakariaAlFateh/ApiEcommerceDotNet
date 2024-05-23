using WebApiCrud.Models;
using WebApiCrud.ViewModels;

namespace WebApiCrud.Repository.Abstract
{
    public  interface ICategoryRepo
    {
        public List<Category> GetAll();

        public Category GetById(int? id);

        public int InsertCategory(Category category);

        public int UpdateCatergory(Category category,int id);

        public int DeleteCategory(int? id);

    }
}