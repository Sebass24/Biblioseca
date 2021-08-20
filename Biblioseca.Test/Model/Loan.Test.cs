using Biblioseca.Model;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using System;
using NUnit.Framework;


namespace Biblioseca.Test
{
    [TestFixture]
    public class LoanTests
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
        public void CreateLoan()
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
                
            };

            this.session.Save(category);
            this.session.Save(author);
            this.session.Save(book);
            this.session.Save(partner);
            this.session.Save(loan);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(loan.Id > 0);

            Loan created = this.session.Get<Loan>(loan.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(loan.Id, created.Id);



        }

        [Test]
        public void UsingCreate()
        {
            Author author = Author.Create("Carlitos", "Saul");

            Category category = Category.Create("Horror");

            Book book = Book.Create(
                "sarasa",
                "habla de la sarasa",
                "123-321-123",
                123,
                category,
                author,
                2);

            Partner partner = Partner.Create(
                "julio",
                "Pascual",
                "novita");

            Loan loan = Loan.Create(book, partner);

            this.session.Save(category);
            this.session.Save(author);
            this.session.Save(book);
            this.session.Save(partner);
            this.session.Save(loan);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(loan.Id > 0);

            Loan created = this.session.Get<Loan>(loan.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(loan.Id, created.Id);

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

            Loan loan = Loan.Create(book, partner);

            Assert.IsTrue(!loan.Deleted);

            loan.MarkAsDeleted();

            this.session.Save(partner);
            this.session.Save(category);
            this.session.Save(author);
            this.session.Save(book);
            this.session.Save(loan);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(loan.Id > 0);

            Loan created = this.session.Get<Loan>(loan.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(loan.Deleted, created.Deleted);
        }

        [Test]
        public void Return()
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

            Loan loan = Loan.Create(book, partner);

            
            Assert.IsNull(loan.Finish);
            loan.Returned();
            Assert.IsNotNull(loan.Finish);

            this.session.Save(partner);
            this.session.Save(category);
            this.session.Save(author);
            this.session.Save(book);
            this.session.Save(loan);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(loan.Id > 0);

            Loan created = this.session.Get<Loan>(loan.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(loan.Finish, created.Finish);
        }

    }
}
