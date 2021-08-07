using System;
using System.Collections.Generic;
using Biblioseca.DataAccess.Loans;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Model;
using Biblioseca.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NHibernate;

namespace Biblioseca.Test
{
    [TestClass]
    public class LoanServiceTest
    {
        private LoanService loanService;
        private Mock<LoanDao> loanDao;
        private Mock<BookDao> bookDao;
        private Mock<PartnerDao> partnerDao;
        private Mock<ISessionFactory> sessionFactory;
        private Mock<ISession> session;

        [TestInitialize]
        public void SetUp()
        {
            this.sessionFactory = new Mock<ISessionFactory>();
            this.session = new Mock<ISession>();
            this.loanDao = new Mock<LoanDao>(this.sessionFactory.Object);
            this.bookDao = new Mock<BookDao>(this.sessionFactory.Object);
            this.partnerDao = new Mock<PartnerDao>(this.sessionFactory.Object);
        }

        [TestMethod]
        public void LoanABook()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.loanDao.Setup(dao => dao.GetLoansByBookId(bookId)).Returns(new List<Loan>());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.loanDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Loan loan = this.loanService.LoanABook(bookId, partnerId);

            Assert.IsNotNull(loan);
        }

        [TestMethod]
        public void LoanABookWhenBookDoesNotExist()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));
            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);
            Assert.ThrowsException<BusinessRuleException>(() => this.loanService.LoanABook(bookId, partnerId),
                "Libro no existe. ");
        }


        [TestMethod]
        public void LoanABookWhenPartnerDoesNotExist()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(default(Book));
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(default(Partner));
            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);
            Assert.ThrowsException<BusinessRuleException>(() => this.loanService.LoanABook(bookId, partnerId),
                "Socio no existe. ");
        }

        [TestMethod]
        public void LoanABookWhenBooksWasBorrowed()
        {
            const int bookId = 1;
            const int partnerId = 1;

            this.bookDao.Setup(dao => dao.Get(bookId)).Returns(GetBook());
            this.partnerDao.Setup(dao => dao.Get(partnerId)).Returns(GetPartner());
            this.loanDao.Setup(dao => dao.GetLoansByBookId(bookId)).Returns(GetLoans());
            this.session.Setup(x => x.Save(It.IsAny<object>()));
            this.loanDao.Setup(dao => dao.Session).Returns(this.session.Object);

            this.loanService = new LoanService(this.loanDao.Object, this.bookDao.Object, this.partnerDao.Object);

            Assert.ThrowsException<BusinessRuleException>(() => this.loanService.LoanABook(bookId, partnerId),
                "El libro ya fue prestado.");
        }

        private static IEnumerable<Loan> GetLoans()
        {
            List<Loan> loans = new List<Loan> { new Loan { Id = 1 } };

            return loans;
        }

        private static Partner GetPartner()
        {
            Partner partner = new Partner()
            {
                FirstName = "John",
                LastName = "Smith",
                UserName = "johnsmith"
            };

            return partner;
        }

        private static Book GetBook()
        {
            Book book = new Book
            {
                Title = "A title",
                Description = "A description",
                Price = 1.0,
                Stock= 2
            };

            return book;
        }
    }
}