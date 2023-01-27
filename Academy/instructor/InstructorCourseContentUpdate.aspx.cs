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
using System.Web.Services;

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

                List<object[]> contentData = (List<object[]>)ViewState["ContentData"];


                // retrieve content data from the database and store it in the ViewState
                retrieveContentData();
                repeaterNavLinks.DataSource = contentData;
                System.Diagnostics.Debug.WriteLine("contentData[2].Value: " + contentData[2]);
                repeaterNavLinks.DataBind();




            }
            //else
            //{
            //    string contentId = Request.Params["__EVENTARGUMENT"];
            //    System.Diagnostics.Debug.WriteLine("cccccid..: " + contentId);
            //    fillFormData(contentId);
            //}


        }



        void retrieveContentData()
        {
            // get course id from url
            int cid = Convert.ToInt32(Request.QueryString["CId"].ToString());
            System.Diagnostics.Debug.WriteLine("instructor is heerrre. ");
            //search course content by cid in db
            String query = "select *  from CourseContent where CId=@cid";
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("cid", cid);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    System.Diagnostics.Debug.WriteLine("Rows got affected...");
                }

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
                    data[7] = true;
                    dataList.Add(data);


                }
                sdr.Close();

                ViewState["ContentData"] = dataList;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("errrrroor...");
            }
        }


        protected void lnkNavLink_Click(object sender, EventArgs e)
        {
            LinkButton lnkNavLink = (LinkButton)sender;
            int contentId = Convert.ToInt32(lnkNavLink.CommandArgument);
            fillFormData(contentId);
            UpdatePanel1.Update();
        }


        void fillFormData(int contId)
        {
            int contentId = contId;


            System.Diagnostics.Debug.WriteLine("formdata..: " + contentId);
            //ViewState["contentId"] = contentId;

            TextBox txtContentTitle = (TextBox)UpdatePanel1.FindControl("txtContentTitle");
            TextBox txtCContent = (TextBox)UpdatePanel1.FindControl("txtCContent");
            TextBox txtUrl = (TextBox)UpdatePanel1.FindControl("txtUrl");


            List<object[]> contentData = (List<object[]>)ViewState["ContentData"];
            foreach (object[] data in contentData)
            {

                if (Convert.ToInt16(data[0]) == contentId)

                {
                    System.Diagnostics.Debug.WriteLine("if..: " + contentId);
                    txtContentTitle.Text = (string)data[2];
                    txtCContent.Text = (string)data[3];
                    txtUrl.Text = (string)data[5];
                    break;
                }

            }
        }

        protected void repeaterNavLinks_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                // Retrieve the contentId from the CommandArgument
                int contentId = Convert.ToInt32(e.CommandArgument);

                // Fill the form data
                fillFormData(contentId);

                // Trigger the UpdatePanel
                UpdatePanel1.Update();
            }
        }




        //protected void pills_tab_Click(object sender, EventArgs e)
        //{
        //    // retrieve the ID of the clicked button
        //    LinkButton clickedButton = (LinkButton)sender;
        //    int contentId = Convert.ToInt32(clickedButton.ID.Split('-')[2]);

        //    // update the hidden field with the ID
        //    HiddenField hiddenContentId = (HiddenField)FindControl("hiddenContentId");
        //    hiddenContentId.Value = contentId.ToString();

        //    // fill the form with the corresponding data
        //    fillFormData(contentId.ToString());

        //    // update the UpdatePanel
        //    UpdatePanel1.Update();
        //}






        [System.Web.Services.WebMethod]
        public static void SetViewState(int contentId, string cData)
        {
            List<object[]> contentData = new JavaScriptSerializer().Deserialize<List<object[]>>(cData);
            System.Diagnostics.Debug.WriteLine("hiddenValue..: " + contentId);
            foreach (object[] data in contentData)
            {
                if ((int)data[0] == contentId)
                {
                    string contentTitle = (string)data[2];
                    string contentText = (string)data[3];
                    string contentUrl = (string)data[5];
                    Page page = HttpContext.Current.CurrentHandler as Page;

                    TextBox txtContentTitle = (TextBox)page.FindControl("txtContentTitle");
                    TextBox txtCContent = (TextBox)page.FindControl("txtCContent");
                    TextBox txtUrl = (TextBox)page.FindControl("txtUrl");
                    // Set the value of the textbox controls
                    txtContentTitle.Text = contentTitle;
                    txtCContent.Text = contentText;
                    txtUrl.Text = contentUrl;
                    break;

                }

            }

        }



        // Set the ViewState variable when a content title is clicked
    }
}