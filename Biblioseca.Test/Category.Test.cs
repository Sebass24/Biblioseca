using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;


namespace Biblioseca.Test
{
    [TestClass]
    public class CategoryTests
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
        public void CreateCategory()
        {
            Category category = new Category();
            category.Name = "Horror";

            this.session.Save(category);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(category.Id > 0);

            Category created = this.session.Get<Category>(category.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(category.Id, created.Id);
        }
    }
}