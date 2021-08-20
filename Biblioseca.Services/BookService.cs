using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Loans;
using Biblioseca.Model;


namespace Biblioseca.Service
{
    public class BookService
    {
        private readonly BookDao bookDao;
        private readonly LoanDao loanDao;

        public BookService(BookDao bookDao)
        {
            this.bookDao = bookDao;
            
        }
        public BookService(BookDao bookDao, LoanDao borrowDao)
        {
            this.bookDao = bookDao;
            this.loanDao = borrowDao;
        }

        public IEnumerable<Book> ListBooks()
        {
            IEnumerable<Book> books = bookDao.GetAll();

            Ensure.NotNull(books, "No hay Libros.");

            return books;
        }

        public bool IsAvailable(int bookId)
        {
            Ensure.IsTrue(bookId > 0, "Book.Id debe ser mayor que 0.");

            Book book = this.bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");

            return book.Stock > 0;
        }

        public IEnumerable<Book> ListAvailableBooks()
        {

            IEnumerable<Book> books = bookDao.GetAll();
            List<Book> availableBooks = new List<Book>();

            Ensure.NotNull(books, "No Hay Libros.");

            foreach (Book item in books)
            {
                if (IsAvailable(item.Id))
                {
                    availableBooks.Add(item);
                }
            }           

            return availableBooks;
        }

        public IEnumerable<Book> SerchBookByTitle(string title)
        {
            BookFilterDto bookFilterTitle = new BookFilterDto
            {
                Title = title
            };                

            return bookDao.GetByFilter(bookFilterTitle);
        }

        public IEnumerable<Book> SerchBookByAuthor(string author)
        {
            BookFilterDto bookFilterAuthor = new BookFilterDto
            {
                AuthorFirstName= author
            };

            return bookDao.GetByFilter(bookFilterAuthor);
        }

        public void Create(Book book)
        {
            this.bookDao.Save(book);
        }


    }
}