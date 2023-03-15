using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Collections;
using System.Drawing;
using System.Net;
using System.Xml.Linq;
using System.Web.Security;

namespace Academy
{
    public partial class WebForm10 : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                retrieveData();
            }
            if (IsPostBack)
            {
                //showOverView();
            }

        }


        void retrieveData()
        {
            string accountQuery = "select * from UserAccount where AccountId=@id";
            string categoryQuery = "select * from CourseCategory where CourseCatId=@id";
            string imageQuery = "select top 1 * from CourseContent where CId=@id";

            string EnrolledCourseQuery = "SELECT Courses.CourseId, Courses.Title, Courses.Category, Courses.Overview, Courses.InstructorId FROM EnrollCourse " +
                "INNER JOIN Courses ON EnrollCourse.CourseId = Courses.CourseId " +
                "WHERE EnrollCourse.StudentId = @sid";

            Utils uObj = new Utils();
            string username = Session["username"].ToString();
            System.Diagnostics.Debug.WriteLine("heyyyyyy....", username);
            string stuId = uObj.getSpecificData(username, accountQuery, "FullName");
            System.Diagnostics.Debug.WriteLine("heyyyyyy....", stuId);

            SqlParameter sid = new SqlParameter("@sid", SqlDbType.Int);
            sid.Value = Convert.ToInt32(stuId);

            SqlParameter[] parameters = { sid };

            try
            {
                SqlDataReader sdr = uObj.DbAction(EnrolledCourseQuery, parameters);
                if(sdr.HasRows)
                {
                    System.Diagnostics.Debug.WriteLine("heyyyyyy....contain data");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("heyyyyyy....no data");
                }

                // Read the data and store it in the ViewState
                List<object[]> dataList = new List<object[]>();
                List<string> idList = new List<string>();

                while (sdr.Read())
                {
                    object[] data = new object[7];
                    data[0] = sdr["CourseId"];
                    data[1] = sdr["Title"];
                    data[2] = uObj.getSpecificData(sdr["Category"].ToString(), categoryQuery, "Category");
                    data[3] = sdr["OverView"];
                    data[5] = uObj.getSpecificData(sdr["CourseId"].ToString(), imageQuery, "ImageCont");
                    data[6] = uObj.getSpecificData(sdr["Instructorid"].ToString(), accountQuery, "FullName");
                    idList.Add(data[0].ToString());
                    System.Diagnostics.Debug.WriteLine("heyyyyyy....", data[0]);

                    dataList.Add(data);
                }
                sdr.Close();
                ViewState["MyCoursesData"] = dataList;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



        protected void btnEnrollCourse_Click(object sender, EventArgs e)
        {
            string courseId = ViewState["courseId"].ToString();

            if (User.Identity.IsAuthenticated)
            {
                // The user is logged in, so redirect them to the checkout page.
                Response.Redirect("Checkout.aspx?courseId=" + courseId);
            }
            else
            {
                // The user is not logged in, so redirect them to the login page.
                Response.Redirect("Login.aspx?redirectUrl=Checkout.aspx" + "&CourseId=" + courseId);
            }
        }
    }

}