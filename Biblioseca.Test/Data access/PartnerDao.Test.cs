using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Biblioseca.Test
{
    [TestClass]
    public class PartnerDaoTest
    {
        private ISessionFactory sessionFactory;
        private ISession session;
        private ITransaction transaction;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Configuration().Configure().BuildSessionFactory();
            this.session = this.sessionFactory.OpenSession();
            this.transaction = this.session.BeginTransaction();
            CurrentSessionContext.Bind(this.session); //hack para tener el config de la session
        }

        [TestCleanup]
        public void CleanUp()
        {
            this.transaction.Rollback();
            this.session.Close();
        }

        [TestMethod]
        public void GetAll()
        {
            PartnerDao partnerDao = new PartnerDao(this.sessionFactory);

            IEnumerable<Partner> Partners = partnerDao.GetAll();

            Assert.IsTrue(Partners.Any());
        }
        
    }
}