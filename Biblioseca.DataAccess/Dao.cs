using System.Collections.Generic;
using NHibernate;
using NHibernate.Criterion;

namespace Biblioseca.DataAccess
{
    public abstract class Dao<T> : IDao<T>
    {
        private readonly ISessionFactory sessionFactory;

        protected Dao(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }
        public virtual ISession Session
        {
            get { return this.sessionFactory.GetCurrentSession(); }
        }

        public void Save(T entity)
        {
            Session
                .Save(entity);
        }

        public void Delete(T entity)
        {
            this.Session
                .Delete(entity);
        }

        public virtual T Get(int id)
        {
            return this.Session
                .Get<T>(id);
        }

        public IEnumerable<T> GetAll()
        {
            return this.Session
                .Query<T>();
        }

    }
}