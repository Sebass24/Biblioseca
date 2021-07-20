using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioseca.Model
{
    public class Loan
    {
        public virtual Book Book { get; set; }
        public virtual Partner Partner { get; set; }
        public virtual DateTime Start { get; set; }
        public virtual DateTime Finish { get; set; }
        public virtual int Id { get; set; }


    }
}
