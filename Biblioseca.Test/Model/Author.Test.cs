using Biblioseca.Model;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;


namespace Biblioseca.Test
{
    [TestFixture]
    public class AuthorTests
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
        public void CreateAuthor()
        {
            Author author = new Author
            {
                FirstName = "Wanda",
                LastName = "Maximoff"
            };

            this.session.Save(author);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(author.Id > 0);

            Author created = this.session.Get<Author>(author.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(author.Id, created.Id);
        }

        [Test]
        public void UsingCreate()
        {
            Author author = Author.Create(
                "Wanda",
                "Maximoff"
                );
            
            this.session.Save(author);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(author.Id > 0);

            Author created = this.session.Get<Author>(author.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(author.Id, created.Id);
        }

        [Test]
        public void MarkAsDeleted()
        {
            Author author = Author.Create("juan", "carlos");

            Assert.IsTrue(!author.Deleted);

            author.MarkAsDeleted();

            this.session.Save(author);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(author.Id > 0);

            Author created = this.session.Get<Author>(author.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(author.Deleted, created.Deleted);
        }
    }
}