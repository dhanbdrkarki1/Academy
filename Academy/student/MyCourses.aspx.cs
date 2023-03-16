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
                System.Diagnostics.Debug.WriteLine("my courses beginning....");
                retrieveData();
            }
            if (IsPostBack)
            {
                System.Diagnostics.Debug.WriteLine("redirection beginning....");
                redirectToCourseContent();

            }

        }


        void retrieveData()
        {
            string userQuery = "select * from UserAccount where Username=@id";
            string accountQuery = "select * from UserAccount where AccountId=@id";
            string categoryQuery = "select * from CourseCategory where CourseCatId=@id";
            string imageQuery = "select top 1 * from CourseContent where CId=@id";

            string EnrolledCourseQuery = "SELECT Courses.CourseId, Courses.Title, Courses.Category, Courses.Overview, Courses.InstructorId FROM EnrollCourse " +
                "INNER JOIN Courses ON EnrollCourse.CourseId = Courses.CourseId " +
                "WHERE EnrollCourse.StudentId = @sid";

            Utils uObj = new Utils();
            string username = Session["username"].ToString();
            string stuId = uObj.getSpecificData(username, userQuery, "AccountId");

            SqlParameter sid = new SqlParameter("@sid", SqlDbType.Int);
            sid.Value = Convert.ToInt32(stuId);

            SqlParameter[] parameters = { sid };

            try
            {
                SqlDataReader sdr = uObj.DbAction(EnrolledCourseQuery, parameters);
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

                    dataList.Add(data);
                }
                sdr.Close();
                ViewState["MyCoursesData"] = dataList;
                System.Diagnostics.Debug.WriteLine("Retreiving completed....");
            }
            catch (Exception ex)
            {
                Console.WriteLine("My courses page error:" + ex.Message);
            }
        }


        // redirecting to course content
        void redirectToCourseContent()
        {
            System.Diagnostics.Debug.WriteLine("before redreicting course id lol....: ");
            string courseId = Request.Params["__EVENTARGUMENT"];
            System.Diagnostics.Debug.WriteLine("before redreicting course id....: " + courseId);
            retrieveCourseContentData(courseId);
            System.Diagnostics.Debug.WriteLine("now redreicting....");
            Response.Redirect("CoursesDetail.aspx");

        }

        void retrieveCourseContentData(string userCourseId)
        {
            string userQuery = "select * from UserAccount where Username=@id";

            string courseContentQuery = "select * from CourseContent where  CId=@CId";

            System.Diagnostics.Debug.WriteLine("all retrieveCourseContentData................");
            Utils uObj = new Utils();
            string username = Session["username"].ToString();
            string stuId = uObj.getSpecificData(username, userQuery, "AccountId");
            SqlParameter cccid = new SqlParameter("@CId", SqlDbType.Int);
            cccid.Value = Convert.ToInt32(userCourseId);

            SqlParameter[] parameters = { cccid };

            try
            {
                SqlDataReader sdr = uObj.DbAction(courseContentQuery, parameters);

                // Read the data and store it in the ViewState
                List<object[]> dataList = new List<object[]>();

                while (sdr.Read())
                {
                    object[] data = new object[8];
                    data[0] = sdr["CourseContId"];
                    data[1] = sdr["CId"];
                    data[2] = sdr["ContTitle"];
                    data[3] = sdr["TextCont"];
                    data[4] = sdr["ImageCont"];
                    data[5] = sdr["FileCont"];
                    data[6] = sdr["ContentUrl"];
                    dataList.Add(data);
                }

                System.Diagnostics.Debug.WriteLine("just checking completed....: ");
                sdr.Close();
                ViewState["MyCoursesContentData"] = dataList;
                Session["MyCoursesContentData"] = dataList;

                System.Diagnostics.Debug.WriteLine("Retreiving content completed....");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }



    }

}