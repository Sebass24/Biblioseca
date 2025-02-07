﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.DataAccess.Loans;
using Biblioseca.DataAccess.Books;
using Biblioseca.DataAccess.Partners;
using Biblioseca.DataAccess;
using Biblioseca.Model;

namespace Biblioseca.Services
{
    public class LoanService
    {
        private readonly LoanDao loanDao;
        private readonly BookDao bookDao;
        private readonly PartnerDao partnerDao;

        public LoanService(LoanDao loanDao, BookDao bookDao, PartnerDao partnerDao)
        {
            this.loanDao = loanDao;
            this.bookDao = bookDao;
            this.partnerDao = partnerDao;
        }

        public LoanService(LoanDao loanDao) 
        {
            this.loanDao = loanDao;
        }


        public Loan LoanABook(int bookId, int partnerId)
        {
            Book book = bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");
            Ensure.IsTrue(book.Stock > 0, "Este libro no está disponibles. ");

            Partner partner = partnerDao.Get(partnerId);
            Ensure.NotNull(partner, "Socio no existe. ");
            
            IEnumerable<Loan> loans = loanDao.GetLoans(partnerId);
            Ensure.IsTrue(loans.Count() < 2, "El socio no puede pedir más libros");

            Loan loan = new Loan
            {
                Book = book,
                Partner = partner,
                Start = DateTime.Now
            };

            book.DecreaseStock();

            loanDao.Save(loan);

            return loan;
        }

        public void Returns(int bookId, int partnerId)
        {
            Book book = bookDao.Get(bookId);
            Ensure.NotNull(book, "El libro no existe. ");

            Partner partner = partnerDao.Get(partnerId);
            Ensure.NotNull(partner, "El socio no existe. ");

            Loan loan = loanDao.GetLoan(bookId, partnerId);
            Ensure.NotNull(loan, "No existe el prestamo");

            book.IncreaseStock();
            loan.Finish = DateTime.Now;

            loanDao.Save(loan);
        }

        public IEnumerable<Loan> ListLoans()
        {
            IEnumerable<Loan> loans = loanDao.GetAll();

            Ensure.NotNull(loans, "No hay prestamos.");

            return loans;
        }



    }
}
