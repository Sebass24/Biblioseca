using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    public class Partner : Entity
    {
        public Partner()
        {
            loans = new List<Loan>();
        }

        public virtual IList<Loan> loans { get; set; }       
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string UserName { get; set; }        

        public static Partner Create(
            string firstName,
            string lastName,
            string userName
            )
        {
            Ensure.NotNull(firstName, "Nombre no peude ser nulo. ");
            Ensure.NotNull(lastName, "Apellido no puede ser nulo. ");
            Ensure.NotNull(userName, "Nombre e usuario no puede ser nulo. ");


            Partner partner = new Partner
            {
                FirstName = firstName,
                LastName=lastName,
                UserName=userName                
            };

            return partner;
        }


    }
}
