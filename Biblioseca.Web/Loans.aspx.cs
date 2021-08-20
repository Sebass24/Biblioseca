using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Loans;
using Biblioseca.Services;

namespace Biblioseca.Web
{
    public partial class Loans : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoanDao loanDao = new LoanDao(Global.SessionFactory);
            LoanService loandService = new LoanService(loanDao);

            this.GridViewLoans.DataSource = loandService.ListLoans();
            this.GridViewLoans.DataBind();

        }
    }
}