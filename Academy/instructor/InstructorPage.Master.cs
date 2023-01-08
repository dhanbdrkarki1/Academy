using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Academy
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["username"] == null)
            {
                Response.Redirect("../base/Login.aspx");

            }
            else
            {
                string name = Session["username"].ToString();
                lblUser.Text = "Hi, " + char.ToUpper(name[0]) + name.Substring(1);
            }

        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            Session.Remove("username");
            Response.Redirect("../base/Index.aspx");
        }
    }
}