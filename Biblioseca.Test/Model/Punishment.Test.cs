using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using System;


namespace Biblioseca.Test
{
    [TestClass]
    public class PunishmentTests
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
        public void CreatePunishment()
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
                Status = false
            };
            Punishment punishment = new Punishment
            {
                Partner = partner,
                Loan = loan,
            };

            this.session.Save(category);
            this.session.Save(author);
            this.session.Save(book);
            this.session.Save(partner);
            this.session.Save(loan);
            this.session.Save(punishment);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(punishment.Id > 0);

            Punishment created = this.session.Get<Punishment>(punishment.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(punishment.Id, created.Id);



        }
    }
}
