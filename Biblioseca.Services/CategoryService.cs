using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using Biblioseca.DataAccess.Categories;

namespace Biblioseca.Services
{
    public class CategoryService
    {
        private readonly CategoryDao categoryDao;

        public CategoryService(CategoryDao categoryDao)
        {
            this.categoryDao = categoryDao;
        }

        public IEnumerable<Category> ListCategories()
        {
            IEnumerable<Category> categories = categoryDao.GetAll();

            Ensure.NotNull(categories, "No hay categorias.");

            return categories;
        }

        public Category Get(int id)
        {
            return this.categoryDao.Get(id);
        }

    }
}