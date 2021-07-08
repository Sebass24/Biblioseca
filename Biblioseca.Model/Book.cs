using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    class Book
    {
        public virtual string Title { get; set; }
        public virtual string Author { get; set; } //ver esto
        public virtual string Description { get; set; }
        public virtual string Category { get; set; }
        public int ISBN { get; set; }
        public virtual int Id { get; set; }

    }
}


