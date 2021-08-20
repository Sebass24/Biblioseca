using Biblioseca.Model;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NUnit.Framework;


namespace Biblioseca.Test
{
    [TestFixture]
    public class PartnerTests
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
        public void CreatePartner()
        {
            Partner partner = new Partner
            {
                FirstName = "Julio",
                LastName = "Pascual",
                UserName = "Nobita",
            };

            this.session.Save(partner);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(partner.Id > 0);

            Partner create = this.session.Get<Partner>(partner.Id);

            Assert.IsNotNull(create);
            Assert.AreEqual(partner.Id, create.Id);


        }

        [Test]
        public void UsingCreate()
        {
            Partner partner = Partner.Create(
                "Julio",
                "Pascual",
                "Novita24");
            

            this.session.Save(partner);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(partner.Id > 0);

            Partner create = this.session.Get<Partner>(partner.Id);

            Assert.IsNotNull(create);
            Assert.AreEqual(partner.Id, create.Id);


        }

        [Test]

        public void MarkAsDeleted()
        {
            Partner partner = Partner.Create("juan", "carlos","Lkks");

            Assert.IsTrue(!partner.Deleted);

            partner.MarkAsDeleted();

            this.session.Save(partner);
            this.session.Flush();
            this.session.Clear();

            Assert.IsTrue(partner.Id > 0);

            Partner created = this.session.Get<Partner>(partner.Id);

            Assert.IsNotNull(created);

            Assert.AreEqual(partner.Deleted, created.Deleted);
        }
    }
}
