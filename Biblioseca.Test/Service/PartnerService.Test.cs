using System;
using System.Collections.Generic;

using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;
using Biblioseca.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace Biblioseca.Test.Service
{
    [TestClass]
    public class PartnerServiceTest
    {

        private PartnerService partnerService;
        private Mock<PartnerDao> partnerDao;

        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.partnerDao = new Mock<PartnerDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void SerchByUserName()
        {
            string userName = "JohnLizard";
            //this.partnerDao.Setup(dao => dao.SerchPartnerByUserName(userName).Return(GetPartner());

            this.partnerService = new PartnerService(this.partnerDao.Object);

            Partner partner = this.partnerService.SerchPartnerByUserName(userName);

            


        }


        private static Partner GetPartner()
        {
            Partner partner = new Partner()
            {
                FirstName = "Lagarto",
                LastName = "Juancho",
                UserName = "JohnLizard"
            };

            return partner;
        }
    }
}