using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;


namespace Biblioseca.Test
{
    [TestClass]
    public class AuthorTests
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
    }
}