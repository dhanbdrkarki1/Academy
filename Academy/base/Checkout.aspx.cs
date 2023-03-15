using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Academy.@base
{
    public partial class Checkout : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            loadCourseData();
        }



        void loadCourseData()
        {
            string courseId = Request.QueryString["CourseId"];
            string stuId = Request.QueryString["StuId"];
            string query = "select * from Courses where CourseId=@courseId";
            string accountQuery = "select * from UserAccount where AccountId=@id";
            string categoryQuery = "select * from CourseCategory where CourseCatId=@id";


            Utils uObj = new Utils();


            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@courseId", courseId);
                SqlDataReader sdr = cmd.ExecuteReader();
                List<string> dataList = new List<string>();

                while (sdr.Read())
                {
                    int cid = Convert.ToInt32(sdr[0]);
                    string title = sdr[1].ToString();
                    string category = uObj.getSpecificData(sdr[2].ToString(), categoryQuery, "Category");
                    string overview = sdr[3].ToString();
                    string rate = sdr[4].ToString();
                    string instructor = uObj.getSpecificData(sdr[6].ToString(), accountQuery, "FullName");
                    string instructorId = sdr[6].ToString();

                    if (courseId == cid.ToString())
                    {
                        lblTitle.Text = title;
                        lblCategory.Text = category;
                        lblPrice.Text = rate;
                        lblInstructor.Text = instructor;

                        dataList.Add(rate);
                        dataList.Add(instructorId);
                    }
                }
                ViewState["checkoutData"] = dataList;
                sdr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        protected void btnCompleteOrder_Click(object sender, EventArgs e)
        {
            string courseId = Request.QueryString["courseId"];
            string stuId = Request.QueryString["StuId"];
            Utils uObj = new Utils();

            DateTime currentDate = DateTime.Now;
            var enrolledDate = currentDate.ToString("yyyy/MM/dd");

            List<string> checkOutData = (List<string>)ViewState["checkoutData"];

            String query = "insert into EnrollCourse(CourseId,StudentId,InstructorId,TotalAmt,EnrollmentDate) values(@cid,@sid,@iid,@ta,@ed)";
            
            //queries params
            SqlParameter pCid = new SqlParameter("@cid", SqlDbType.Int);
            pCid.Value = Convert.ToInt32(courseId);

            SqlParameter pSid = new SqlParameter("@sid", SqlDbType.Int);
            pSid.Value = Convert.ToInt32(stuId);

            SqlParameter pIid = new SqlParameter("@iid", SqlDbType.Int);
            pIid.Value = Convert.ToInt32(checkOutData[1]);

            SqlParameter pTa = new SqlParameter("@ta", SqlDbType.Int);
            pTa.Value = Convert.ToInt32(checkOutData[0]);

            SqlParameter pEd = new SqlParameter("@ed", SqlDbType.Date);
            pEd.Value = enrolledDate;

            SqlParameter[] parameters = { pCid, pSid, pIid, pTa, pEd };

            System.Diagnostics.Debug.WriteLine("all good................");
            // insert data into db
            if(uObj.DbAction(query, parameters) != null)
            {
                string accountQuery = "select * from UserAccount where AccountId=@id";

                string accType = uObj.getSpecificData(stuId, accountQuery, "AccountType");
                if(accType == "Student")
                {
                    Response.Redirect("../student/MyCourses.aspx");
                }
                else
                {
                    Response.Redirect("~/Index.aspx");
                }
            }



        }

    }
}