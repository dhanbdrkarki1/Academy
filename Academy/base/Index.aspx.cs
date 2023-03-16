using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Common;
using System.Collections;
using System.Web.Configuration;

namespace Academy
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("all good................");

            if (!IsPostBack)
            {
                retrieveData();
            }
            else
            {
                showInstructorCourses();
            }
        }

        void showInstructorCourses()
        {
            string accountId = Request.Params["__EVENTARGUMENT"];
            System.Diagnostics.Debug.WriteLine("all good................" + accountId);

            Response.Redirect("InsCourses.aspx?accountId=" + accountId);

        }
        void retrieveData()
        {
            string ciq = "select count(*) as InstructorCount from UserAccount where AccountType=@it";
            string siq = "select count(*) as StudentCount from UserAccount where AccountType=@st";
            string coriq = "select count(*) as CoursesCount from Courses";

            string popularquery = "SELECT TOP 3 c.CourseId, c.Title, c.Category, c.OverView, c.Rate, c.InstructorId, COUNT(ec.CourseID) AS EnrollmentCount FROM Courses AS c.INNER JOIN EnrollCourse AS ec ON c.CourseID = ec.CourseID " +
                "GROUP BY c.CourseId, c.Title, c.Category, c.OverView, c.Rate, c.InstructorId" +
                "ORDER BY EnrollmentCount DESC";
            string accountQuery = "select * from UserAccount where AccountId=@id";
            string categoryQuery = "select * from CourseCategory where CourseCatId=@id";
            string imageQuery = "select top 1 * from CourseContent where CId=@id";
            Utils uObj = new Utils();

            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(coriq, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {

                    string sc = sdr["CoursesCount"].ToString();
                    ViewState["CoursesCount"] = sc;

                }
                sdr.Close();

                SqlCommand cmd1 = new SqlCommand(popularquery, con);
                SqlDataReader sdr1 = cmd1.ExecuteReader();

                List<object[]> dataList = new List<object[]>();
                List<string> idList = new List<string>();
                while (sdr1.Read())
                {
                    object[] data = new object[7];
                    data[0] = sdr1["Courseid"];
                    data[1] = sdr1["Title"];

                    data[2] = uObj.getSpecificData(sdr1["Category"].ToString(), categoryQuery, "Category");
                    data[3] = sdr1["OverView"];
                    data[4] = sdr1["Rate"];
                    data[5] = uObj.getSpecificData(sdr1["Courseid"].ToString(), imageQuery, "ImageCont");
                    data[6] = uObj.getSpecificData(sdr1["Instructorid"].ToString(), accountQuery, "FullName");
                    idList.Add(data[0].ToString());

                    dataList.Add(data);

                }
                sdr1.Close();
                ViewState["PopularCourseData"] = dataList;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Something went wrong.: " + ex.Message);
            }

            //couting instructor and student
            SqlParameter it = new SqlParameter("@it", SqlDbType.Char);
            it.Value = "Instructor";

            SqlParameter st = new SqlParameter("@st", SqlDbType.Char);
            st.Value = "Student";

            SqlParameter[] p1 = { it };
            SqlParameter[] s1 = { st };

            SqlDataReader instructor_sdr = uObj.DbAction(ciq, p1);

            SqlDataReader student_sdr = uObj.DbAction(siq, s1);

            while (instructor_sdr.Read())
            {
                string ic = instructor_sdr["InstructorCount"].ToString();
                ViewState["ins_count"] = ic;
            }

            instructor_sdr.Close();

            while (student_sdr.Read())
            {
                string sc = student_sdr["StudentCount"].ToString();
                ViewState["std_count"] = sc;

            }
            student_sdr.Close();



        }
    }
}
