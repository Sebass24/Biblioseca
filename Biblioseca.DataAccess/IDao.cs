using System.Collections.Generic;

namespace Biblioseca.DataAccess
{
    public interface IDao<T> //poner entre <> significa que admite tipos de datos generoicos
    {
        void Save(T entity);
        void Delete(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}