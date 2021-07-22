using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;


namespace Biblioseca.Test
{
    [TestClass]
    public class PartnerTests
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
    }
}
