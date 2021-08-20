using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Biblioseca.Model
{
    public class Category : Entity
    {        
        public virtual string Name { get; set; }
        
        public static Category Create(string name)
        {
            Ensure.NotNull(name, "El nombre de la categoria no puede ser nulo");

            Category category = new Category
            {
                Name = name
            };
            return category;
        }
    }
}
