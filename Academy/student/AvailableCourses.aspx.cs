using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Academy.student
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // Get student Id from the data base
        int studentid()
        {
            int studentid = -1;

            string username = "";
            if (Session["username"] != null)
            {
                username = Session["username"].ToString();
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("select AccountId from UserAccount where username='" + username + "'", connection);
                command.Parameters.AddWithValue("@username", username);

                connection.Open();
                SqlDataReader sdr = command.ExecuteReader();

                if (sdr.Read())
                {
                    return studentid = sdr.GetInt32(0);
                }
            }
            return 0;
        }

        string getUsername()
        {
            string username = "";
            if (Session["username"] != null)
            {
                username = Session["username"].ToString();
                return username;
            }
            else
            {
                Response.Redirect("../base/Login.aspx");
            }
            return null;
        }
        protected void btnCl_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked");
            string courseId = ViewState["courseId"].ToString();
            Response.Redirect("Checkout.aspx?courseId=" + courseId);


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Clicked");
            string courseId = ViewState["courseId"].ToString();
            Response.Redirect("Checkout.aspx?courseId=" + courseId);
        }
    }
}