using Biblioseca.Model;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;

namespace Biblioseca.DataAccess.Partners
{
    public class PartnerDao : Dao<Partner>, IPartnerDao
    {
        public PartnerDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {
        }

        public IEnumerable<Partner> GetByFilter(PartnerFilter partnerFilter)
        {
            ICriteria criteria = this.Session.CreateCriteria<Partner>();
            if (!string.IsNullOrEmpty(partnerFilter.LastName))
            {
                criteria.Add(Restrictions.Like("LastName", partnerFilter.LastName, MatchMode.Anywhere));
            }
            if (!string.IsNullOrEmpty(partnerFilter.UserName))
            {
                criteria.Add(Restrictions.Like("UserName", partnerFilter.UserName, MatchMode.Anywhere));
            }
            if (!string.IsNullOrEmpty(partnerFilter.FirsName))
            {
                criteria.Add(Restrictions.Like("FirstName", partnerFilter.FirsName, MatchMode.Anywhere));
            }


            return criteria.List<Partner>();
        }
    }
}