using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using NHibernate;

namespace Biblioseca.DataAccess.Books
{
    public class BookDao : Dao<Book>, IBookDao
    {
        public BookDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {

        }

    }


}
