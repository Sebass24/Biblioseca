using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Partners;
using Biblioseca.Services;

namespace Biblioseca.Web
{
    public partial class Partners : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PartnerDao partnerDao = new PartnerDao(Global.SessionFactory);
            PartnerService partnerService = new PartnerService(partnerDao);

            this.GridViewPartners.DataSource = partnerService.ListPartners();
            this.GridViewPartners.DataBind();

        }
    }
}