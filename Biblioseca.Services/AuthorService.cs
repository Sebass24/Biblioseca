using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using Biblioseca.DataAccess.Authors;

namespace Biblioseca.Services
{
    public class AuthorService
    {
        private readonly AuthorDao authorDao;

        public AuthorService(AuthorDao authorDao)
        {
            this.authorDao = authorDao;
        }

        public IEnumerable<Author> ListAuthors()
        {
            IEnumerable<Author> authors = authorDao.GetAll();

            Ensure.NotNull(authors, "No hay autores.");

            return authors;
        }


    }
}