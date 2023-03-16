using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
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
                // user entered credentials
                string username, password;

                username = Username.Text;
                password = Password.Text;

                // if user want to enroll course without logging in
                string redirectUrl = Request.QueryString["redirectUrl"];
                string courseId = Request.QueryString["CourseId"];

                if (!string.IsNullOrEmpty(password) || !string.IsNullOrEmpty(username))
                {
                    System.Diagnostics.Debug.WriteLine("login page");
                    Utils uObj = new Utils();
                    try
                    {
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
                                Response.Redirect("../student/MyCourses.aspx");

                            }
                        }
                        else if (userRole == "Admin")
                        {
                            Response.Redirect("../admin/ManageUser.aspx");
                        }
                        else
                        {
                            lblMsg.Text = "Username or password is incorrect.";
                            lblMsg.CssClass = "text-danger";
                            Session.Clear();
                            System.Diagnostics.Debug.WriteLine(lblMsg.Text);
                            //lblMsg.Text = "Username or password is incorrect.";

                            ClearField();

                        }
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: ", ex.Message);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("login page errorr....");

                    lblMsg.Text = "Username or password is empty.";
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
            string stuId = string.Empty;

            string username = "";
            if (Session["username"] != null)
            {
                username = Session["username"].ToString();
            }

            Utils uObj = new Utils();
            string acQuery = "select AccountId from UserAccount where username=@id";
            try
            {
                stuId = uObj.getSpecificData(username, acQuery, "AccountId");
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Username not found...");
            }
            return Convert.ToInt32(stuId);

        }
    }
}