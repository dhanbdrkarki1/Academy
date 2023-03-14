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
               
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Something went wrong.: " + ex.Message);
            }

            SqlParameter it = new SqlParameter("@it", SqlDbType.Char);
            it.Value = "Instructor";

            SqlParameter st = new SqlParameter("@st", SqlDbType.Char);
            st.Value = "Student";

            SqlParameter[] p1 = { it };
            SqlParameter[] s1 = { st };


            // reading instructor type user from db
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
