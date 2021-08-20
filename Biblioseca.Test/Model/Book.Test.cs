//using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;

namespace Biblioseca.Test
{
    [TestFixture]
    public class BookTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [SetUp] //before (antes de cada test)
        public void SetUp()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
        }

        [TearDown] //after (despues de cada test)
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();

        }
        [Test]
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
                Description = "Mejor no escribo nada acá",
                ISBN = "123 3214 123",
                Price = 120,
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

        [Test]
        public void UsingCreate()
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

            Book book = Book.Create(
                "De la estratosfera a Japón",
                "blabla",
                "123-321-345",
                123,
                category,
                author,
                2
                );

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

        [Test]
        public void MarkAsDeleted()
        {
            Author author = Author.Create("juan", "carlos");
            Partner partner = Partner.Create("juan", "carlos", "Lkks");
            Category category = Category.Create("Horror");

            Book book = Book.Create(
                "De la estratosfera a Japón",
                "blabla",
                "123-321-345",
                123,
                category,
                author,
                2
                );

            Assert.IsTrue(!book.Deleted);

            book.MarkAsDeleted();

            this.session.Save(category);
            this.session.Save(author);
            this.session.Save(book);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(book.Id > 0);

            Book created = this.session.Get<Book>(book.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(book.Deleted, created.Deleted);
        }

        [Test]
        public void DecreaseStock()
        {
            Author author = Author.Create("juan", "carlos");
            Partner partner = Partner.Create("juan", "carlos", "Lkks");
            Category category = Category.Create("Horror");

            Book book = Book.Create(
                "De la estratosfera a Japón",
                "blabla",
                "123-321-345",
                123,
                category,
                author,
                2
                );

            Assert.IsTrue(book.Stock == 2);
            book.DecreaseStock();
            Assert.IsTrue(book.Stock == 1);

            this.session.Save(category);
            this.session.Save(author);
            this.session.Save(book);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(book.Id > 0);

            Book created = this.session.Get<Book>(book.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(book.Stock, created.Stock);

        }
        [Test]
        public void IncreaseStock()
        {
            Author author = Author.Create("juan", "carlos");
            Partner partner = Partner.Create("juan", "carlos", "Lkks");
            Category category = Category.Create("Horror");

            Book book = Book.Create(
                "De la estratosfera a Japón",
                "blabla",
                "123-321-345",
                123,
                category,
                author,
                2
                );

            Assert.IsTrue(book.Stock == 2);
            book.IncreaseStock();
            Assert.IsTrue(book.Stock == 3);

            this.session.Save(category);
            this.session.Save(author);
            this.session.Save(book);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(book.Id > 0);

            Book created = this.session.Get<Book>(book.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(book.Stock, created.Stock);
        }
    }
}
