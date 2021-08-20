using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using Biblioseca.DataAccess.Categories;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess.Categories
{
    public class CategoryDao : Dao<Category>, ICategoryDao
    {
        public CategoryDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IEnumerable<Category> GetByFilter(CategoryFilter categoryFilter)
        {
            ICriteria criteria = this.Session.CreateCriteria<Category>();

            if (!String.IsNullOrEmpty(categoryFilter.Name))
            {
                criteria.Add(Restrictions.Like("Name", categoryFilter.Name, MatchMode.Anywhere));
            }
            return criteria.List<Category>();
        }
    }
}
