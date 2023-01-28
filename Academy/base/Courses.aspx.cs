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
    public partial class WebForm2 : System.Web.UI.Page
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


        void showOverView()
        {
            string script = "$(document).ready(function () { $('#overviewModal').modal('show'); });";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowModal", script, true);
            string courseId = Request.Params["__EVENTARGUMENT"];

            List<object[]> courseData = (List<object[]>)ViewState["CData"];


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
                ViewState["CData"] = dataList;
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