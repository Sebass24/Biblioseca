using Biblioseca.Model;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;


namespace Biblioseca.Test
{
    [TestFixture]
    public class CategoryTests
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

        [Test]
        public void UsingCreate()
        {
            Category category = Category.Create(
                "Horror"
               );

            this.session.Save(category);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(category.Id > 0);

            Category created = this.session.Get<Category>(category.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(category.Id, created.Id);
        }

        [Test]
        public void MarkAsDeleted()
        {
            Category category = Category.Create("Horror");

            Assert.IsTrue(!category.Deleted);

            category.MarkAsDeleted();

            this.session.Save(category);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(category.Id > 0);

            Category created = this.session.Get<Category>(category.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(category.Deleted, created.Deleted);
        }
    }
}