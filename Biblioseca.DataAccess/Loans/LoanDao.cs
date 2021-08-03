using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess.Loans
{
    public class LoanDao : Dao<Loan>, ILoanDao
    {
        public LoanDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {

        }

        public virtual IEnumerable<Loan> GetLoansByBookId(int bookId)
        {
            ICriteria criteria = this.Session
                .CreateCriteria<Loan>();

            criteria.CreateCriteria("Book")
                .Add(Restrictions.Eq("Id", bookId));

            return criteria.List<Loan>();
        }
    }

}
