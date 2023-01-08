using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;

namespace Academy
{
    public partial class login : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                string redirectUrl = Request.QueryString["redirectUrl"];
                string courseId = Request.QueryString["CourseId"];


                string username, password;
                username = Username.Text;
                password = Password.Text;

                Utils uObj = new Utils();
                string userRole = uObj.LoginUser(username, password);
                Session["username"] = username;
                if (userRole == "Instructor")
                {

                    Response.Redirect("../instructor/InstructorCourses.aspx");

                }
                else if (userRole == "Student")
                {
                    if (!string.IsNullOrEmpty(redirectUrl))
                    {
                        int studentId = getStudentId();
                        // The redirectUrl parameter is present, so redirect the user to the specified page.
                        Response.Redirect(redirectUrl + "?CourseId=" + courseId + "&StuId=" + studentId);
                    }
                    else
                    {
                        Response.Redirect("../student/Student.aspx");

                    }
                }
                else if (userRole == "Admin")
                {
                    Response.Redirect("../admin/Admin.aspx");
                }
                else
                {

                    lblMsg.CssClass = "text-danger";
                    lblMsg.CssClass = "../student/Student.aspx";
                    //lblMsg.Text = "Username or password is incorrect.";

                    ClearField();

                }
            }

            void ClearField()
            {
                Username.Text = Password.Text = "";
            }
        }


        // get instructor id from db
        int getStudentId()
        {
            int instructorId = -1;

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
                    return instructorId = sdr.GetInt32(0);
                }
            }
            return 0;
        }
    }
}