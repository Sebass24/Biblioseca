using System.Collections.Generic;
using System.Linq;
using Biblioseca.DataAccess.Loans;
using Biblioseca.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace Biblioseca.Test
{
    [TestClass]
    public class LoanDaoTest
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
            LoanDao loanDao = new LoanDao(this.sessionFactory);

            IEnumerable<Loan> Loans = loanDao.GetAll();

            Assert.IsTrue(Loans.Any());
        }
        
    }
}