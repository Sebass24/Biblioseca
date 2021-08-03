using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using NHibernate;

namespace Biblioseca.DataAccess
{
    class PunishmentDao : Dao<Punishment>
    {
        public PunishmentDao(ISessionFactory sessionFactory) : base(sessionFactory)
        {

        }

    }
}
