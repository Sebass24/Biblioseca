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

        public void Create(Author author)
        {
            this.authorDao.Save(author);
        }

        public Author Get(int authorId)
        {
            return this.authorDao.Get(authorId);
        }

        public IEnumerable<Author> ListAuthors()
        {
            IEnumerable<Author> authors = authorDao.GetAll();

            Ensure.NotNull(authors, "No hay autores.");

            return authors;
        }

        public IEnumerable<Author> SerchAuthorByFirstName(string firstName)
        {
            AuthorFilter authorFilter = new AuthorFilter()
            {
                FirtsName = firstName
            };

            Ensure.NotNull(authorDao.GetByFilter(authorFilter), "No se encontró el autor");
            
            return authorDao.GetByFilter(authorFilter);
        }
        public IEnumerable<Author> SerchAuthorByLastName(string lastName)
        {
            AuthorFilter authorFilter = new AuthorFilter()
            {
                LastName = lastName
            };

            Ensure.NotNull(authorDao.GetByFilter(authorFilter), "No se encontró el autor");
            
            return authorDao.GetByFilter(authorFilter);
        }



    }
}