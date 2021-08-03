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

            Author author = new Author
            {
                FirstName = "Carlitos",
                LastName = "Saul"
            };

            Category category = new Category
            {
                Name = "Horror"
            };
            Book book = new Book
            {
                Title = "De la estratosfera a Japón",
                Author = author,
                Category = category,
                Description = "Mejor no escribo nada acá",
                ISBN = "123 3214 123",
                Price = 120,
            };
            Partner partner = new Partner
            {
                FirstName = "Julio",
                LastName = "Pascual",
                UserName = "Nobita",
            };

            Loan loan = new Loan
            {
                Book = book,
                Partner = partner,
                Start = DateTime.Parse("5/1/2008 8:30:52 AM"),
                Finish = DateTime.Parse("12/1/2008 8:30:52 AM"),
                Status = false
            };

            session.Save(category);
            session.Save(author);
            session.Save(book);
            session.Save(partner);
            session.Save(loan);

            
            session.Close();

            Console.WriteLine(loan.Id);
            //Console.ReadKey();
        }
    }
}
