using System;
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

        public Loan LoanABook(int bookId, int partnerId)
        {
            Book book = bookDao.Get(bookId);
            Ensure.NotNull(book, "Libro no existe. ");

            Partner partner = partnerDao.Get(partnerId);
            Ensure.NotNull(partner, "Socio no existe. ");
            Ensure.IsTrue(partner.loans.Count < 2, "No puede pedir prestado más libros. ");

            IEnumerable<Loan> loans = loanDao.GetLoansByBookId(bookId);
            Ensure.IsTrue(!loans.Any(), "El libro ya fue prestado. ");

            Loan loan = new Loan
            {
                Book = book,
                Partner = partner,
                Start = DateTime.Now
            };

            loanDao.Save(loan);

            return loan;
        }
    }
}
