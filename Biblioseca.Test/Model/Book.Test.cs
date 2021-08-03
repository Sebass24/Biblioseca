using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;

namespace Biblioseca.Test
{
    [TestClass]
    public class BookTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [TestInitialize] //before (antes de cada test)
        public void SetUp()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
        }

        [TestCleanup] //after (despues de cada test)
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();

        }
        [TestMethod]
        public void CreateBook()
        {
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
                Description="Mejor no escribo nada acá",
                ISBN="123 3214 123",
                Price=120,
            };
            this.session.Save(category);
            this.session.Save(author);
            this.session.Save(book);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(book.Id > 0);

            Book created = this.session.Get<Book>(book.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(book.Id, created.Id);

        }
    }
}
