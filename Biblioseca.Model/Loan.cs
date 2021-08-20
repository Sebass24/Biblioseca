using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    public class Loan : Entity
    {
        public virtual Book Book { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual DateTime? Start { get; set; }
        public virtual DateTime? Finish { get; set; } // ? sirve para 
              

        public static Loan Create(Book book, Partner partner)
        {
            Ensure.NotNull(book, "Loan.Book no puede ser nulo. ");
            Ensure.NotNull(partner, "Loan.Partner no puede ser nulo. ");

            Loan loan = new Loan
            {
                Book = book,
                Partner = partner,
                Start = DateTime.Now
            };

            return loan;
        }

        public virtual void Returned()
        {
            Finish = DateTime.Now;
        }

    }
}
