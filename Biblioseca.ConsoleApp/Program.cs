using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;

namespace Biblioseca.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ISessionFactory sessionFactory = new Configuration()
                .Configure()
                .BuildSessionFactory();

            ISession session = sessionFactory.OpenSession();

            Author author = session.Get<Author>(1);
            Category category = session.Get<Category>(1);
            Book book = new Book();
            book.Title = "Fisica 5";
            book.Author = author;
            book.Description = "Magnetismo";
            book.Category = category;
            book.ISBN = "123-443-221";
            book.Price = 100;

            //Book book = session.Get<Book>(1);
            //Author author = new Author();
            //author.FirstName = "Steve";
            //author.LastName = "Rogers";

            session.Save(book);

            Console.WriteLine(book.Id);
            //Console.ReadKey();
        }
    }
}
