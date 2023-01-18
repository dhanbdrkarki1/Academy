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
using System.Web.Script.Serialization;
using System.Xml.Linq;

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
            else
            {
                string contentId = Request.Params["__EVENTARGUMENT"];
                System.Diagnostics.Debug.WriteLine("cccccid..: " + contentId);
                fillFormData(contentId);
            }


        }



        void fillFormData(string contId)
        {
            int contentId = Convert.ToInt32(contId);

            System.Diagnostics.Debug.WriteLine("formdata..: " + contentId);
            //ViewState["contentId"] = contentId;
            
            List<object[]> contentData = (List<object[]>)ViewState["ContentData"];
            foreach (object[] data in contentData)
            {
                if (Convert.ToInt16( data[0]) == contentId)
                
                {
                    System.Diagnostics.Debug.WriteLine("if..: " + contentId);
                    txtContentTitle.Text = (string)data[2];
                    txtCContent.Text = (string)data[3];
                    txtUrl.Text = (string)data[5];
                    break;
                }

            }
        }




        //[System.Web.Services.WebMethod]
        //public static void SetViewState(int contentId, string cData)
        //{
        //    List<object[]> contentData = new JavaScriptSerializer().Deserialize<List<object[]>>(cData);
        //    System.Diagnostics.Debug.WriteLine("hiddenValue..: " + contentId);
        //    foreach (object[] data in contentData)
        //    {
        //        if ((int)data[0] == contentId)
        //        {
        //            string contentTitle = (string)data[2];
        //            string contentText = (string)data[3];
        //            string contentUrl = (string)data[5];
        //            Page page = HttpContext.Current.CurrentHandler as Page;

        //            TextBox txtContentTitle = (TextBox)page.FindControl("txtContentTitle");
        //            TextBox txtCContent = (TextBox)page.FindControl("txtCContent");
        //            TextBox txtUrl = (TextBox)page.FindControl("txtUrl");
        //            // Set the value of the textbox controls
        //            txtContentTitle.Text = contentTitle;
        //            txtCContent.Text = contentText;
        //            txtUrl.Text = contentUrl;
        //            break;

        //        }

        //    }

        //}
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        // Set the ViewState variable when a content title is clicked
    }
}