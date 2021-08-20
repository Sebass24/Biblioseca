using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioseca.Model;
using Biblioseca.DataAccess.Partners;

namespace Biblioseca.Services
{
    public class PartnerService
    {
        private readonly PartnerDao partnerDao;

        public PartnerService(PartnerDao partnerDao)
        {
            this.partnerDao = partnerDao;
        }
        public IEnumerable<Partner> ListPartners()
        {
            IEnumerable<Partner> partners = partnerDao.GetAll();

            Ensure.NotNull(partners, "No hay socios.");

            return partners;
        }

        public Partner SerchPartnerByUserName(string name)
        {            
            IDictionary<string, object> parameters = new Dictionary<string, object> { { "UserName", name } };
            Partner partner = partnerDao.GetUniqueByHqlQuery("FROM Partner WHERE UserName= :UserName", parameters); //busca en el maping si hay algo q se llama Partner
            return partner;            
        }

        public IEnumerable<Partner> SerchPartnerByFirstName(string firstName)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object> { { "FirstName", firstName } };
            IEnumerable<Partner> partners = partnerDao.GetByHqlQuery("FROM Partner WHERE UserName= :FirstName", parameters); //busca en el maping si hay algo q se llama Partner
            return partners;
        }

        public IEnumerable<Partner> SerchPartnerByLastName(string lastName)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object> { { "LastName", lastName } };
            IEnumerable<Partner> partners = partnerDao.GetByHqlQuery("FROM Partner WHERE UserName= :LastName", parameters); //busca en el maping si hay algo q se llama Partner
            return partners;
        }



    }
}
