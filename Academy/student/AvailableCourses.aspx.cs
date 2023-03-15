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
            if (!IsPostBack)
            {
                retrieveData();
            }
            if (IsPostBack)
            {
                showOverView();
            }

        }

        // reading courses info from database
        void retrieveData()
        {
            string query = "select * from Courses";
            string accountQuery = "select * from UserAccount where AccountId=@id";
            string categoryQuery = "select * from CourseCategory where CourseCatId=@id";
            string imageQuery = "select top 1 * from CourseContent where CId=@id";
            Utils uObj = new Utils();
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();
                // Read the data and store it in the ViewState
                List<object[]> dataList = new List<object[]>();
                List<string> idList = new List<string>();
                while (sdr.Read())
                {
                    object[] data = new object[7];
                    data[0] = sdr["Courseid"];
                    data[1] = sdr["Title"];
                    data[2] = uObj.getSpecificData(sdr["Category"].ToString(), categoryQuery, "Category");
                    data[3] = sdr["OverView"];
                    data[4] = sdr["Rate"];
                    data[5] = uObj.getSpecificData(sdr["Courseid"].ToString(), imageQuery, "ImageCont");
                    data[6] = uObj.getSpecificData(sdr["Instructorid"].ToString(), accountQuery, "FullName");
                    idList.Add(data[0].ToString());

                    dataList.Add(data);
                }
                sdr.Close();
                ViewState["AvailableCoursesData"] = dataList;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Something went wrong.: " + ex.Message);
            }
        }

        protected void btnAvailEnrollCourse_Click(object sender, EventArgs e)
        {
            Utils uObj = new Utils();
            string name = Session["username"].ToString();
            string accQuery = "select * from UserAccount where Username=@id";
            string stuId = uObj.getSpecificData(name, accQuery, "AccountId");
            System.Diagnostics.Debug.WriteLine("Comple order clicked.... ");

            string courseId = ViewState["courseId"].ToString();
            Response.Redirect("../base/Checkout.aspx?courseId=" + courseId + "&StuId=" + stuId);
        }

        // show course overview
        void showOverView()
        {
            string script = "$(document).ready(function () { $('#overviewModal').modal('show'); });";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
            string courseId = Request.Params["__EVENTARGUMENT"];

            List<object[]> courseData = (List<object[]>)ViewState["AvailableCoursesData"];


            foreach (object[] data in courseData)
            {
                int cid = Convert.ToInt32(data[0]);
                string title = data[1].ToString();
                string category = data[2].ToString();
                string overview = data[3].ToString();
                string rate = data[4].ToString();
                string imgpath = data[5].ToString();
                if (imgpath == "")
                {
                    imgpath = "~/Images/Profile/user.png";
                }
                imgpath = Page.ResolveUrl(imgpath);
                string instructor = data[6].ToString();
                if (courseId == cid.ToString())
                {
                    ViewState["courseId"] = cid;
                    lblTitle.Text = title;
                    lblDescription.Text = overview;
                    lblPrice.Text = rate;

                }

            }
        }


    }
}