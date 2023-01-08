using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.Remoting.Messaging;
using System.Reflection.Emit;
using System.Reflection;

namespace Academy
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // retrieve content data from the database and store it in the ViewState
                retrieveContentData();

            }

            if (IsPostBack)
            {
                showData();
            }

        }

        void showData()
        {
            int contentId = Convert.ToInt32(Request.Params["__EVENTARGUMENT"]);
            testId.Text = "Link was clicked with argument: " + contentId;
            ViewState["contentId"] = contentId;
            List<object[]> contentData = (List<object[]>)ViewState["ContentData"];


            foreach (object[] data in contentData)
            {
                int ccid = (int)data[0];
                int cid = (int)data[1];
                string contTitle = (string)data[2];
                string contText = (string)data[3];
                string contImage = (string)data[4];
                string contFile = (string)data[5];
                string contUrl = (string)data[6];
                if (contentId == cid)
                {
                    txtContentTitle.Text = contTitle;
                    txtCContent.Text= contText;

                }

            }
        }
        void retrieveContentData()
        {
            // get course id from url
            int cid = Convert.ToInt32(Request.QueryString["CId"].ToString());

            //search course content by cid in db
            String query = "select *  from CourseContent where CId='" + cid + "'";
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader sdr = cmd.ExecuteReader();

                // Read the data and store it in the ViewState
                List<object[]> dataList = new List<object[]>();

                while (sdr.Read())
                {
                    object[] data = new object[7];
                    data[0] = sdr["CourseContId"];
                    data[1] = sdr["CId"];
                    data[2] = sdr["ContTitle"];
                    data[3] = sdr["TextCont"];
                    data[4] = sdr["ImageCont"];
                    data[5] = sdr["FileCont"];
                    data[6] = sdr["ContentUrl"];
                    dataList.Add(data);
                }
                sdr.Close();
                ViewState["ContentData"] = dataList;
                ViewState["msg"] = "hello fuck you";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Set the ViewState variable when a content title is clicked
    }
}