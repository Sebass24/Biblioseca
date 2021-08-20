using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Biblioseca.DataAccess.Authors;
using Biblioseca.Services;

namespace Biblioseca.Web
{
    public partial class Authors : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthorDao authorDao = new AuthorDao(Global.SessionFactory);
            AuthorService authorService = new AuthorService(authorDao);

            this.GridViewAuthors.DataSource = authorService.ListAuthors();
            this.GridViewAuthors.DataBind();



        }
    }
}