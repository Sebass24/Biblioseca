using System.Collections.Generic;
using Biblioseca.DataAccess.Authors;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess.Authors
{
    public class AuthorDao : Dao<Author>, IAuthorDao
    {
        public AuthorDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IEnumerable<Author> GetByFilter(AuthorFilter authorFilter)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Author>();

            if (!string.IsNullOrEmpty(authorFilter.LastName))
            {
                criteria.Add(Restrictions.Like("LastName", authorFilter.LastName, MatchMode.Anywhere));
            }

            if (!string.IsNullOrEmpty(authorFilter.FirtsName))
            {
                criteria.Add(Restrictions.Like("FirstName", authorFilter.FirtsName, MatchMode.Anywhere));
            }
            return criteria.List<Author>();
        }
    }
}