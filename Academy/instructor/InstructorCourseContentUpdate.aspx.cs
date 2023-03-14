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
using System.IO;
using System.Web.UI.HtmlControls;

namespace Academy
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                retrieveContentData();
            }
            //if(IsPostBack)
            //{
            //    string contentId = Request.Params["__EVENTARGUMENT"];
            //    System.Diagnostics.Debug.WriteLine("cccccid..: " + contentId);
            //}
        }


        [WebMethod]
        public static string GetContentData(string contentId)
        {
            string title = "";
            string text = "";
            string img = "";
            string file = "";
            string url = "";
            string contid = "";

            Utils uObj = new Utils();
            String query = "select * from CourseContent where CourseContId=@cid";

            //queries params
            SqlParameter cid = new SqlParameter("@cid", SqlDbType.Int);
            cid.Value = Convert.ToInt32(contentId);

            SqlParameter[] parameters = { cid };

            System.Diagnostics.Debug.WriteLine("all good................");
            SqlDataReader sdr = uObj.DbAction(query, parameters);
            while (sdr.Read())
            {
                contid = contentId;
                title = sdr["ContTitle"].ToString();
                text = sdr["TextCont"].ToString();
                img = sdr["ImageCont"].ToString();
                file = sdr["FileCont"].ToString();
                url = sdr["ContentUrl"].ToString();
            }


            string contentData = "{\"contid\":\"" + contentId + "\",\"title\":\"" + title + "\", \"text\":\"" + text + "\", \"img\":\"" + img + "\", \"file\":\"" + file + "\", \"url\":\"" + url + "\"}";
            return contentData;

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
                    object[] data = new object[3];
                    data[0] = sdr["CourseContId"];
                    data[1] = sdr["CId"];
                    data[2] = sdr["ContTitle"];
                    dataList.Add(data);


                }
                sdr.Close();

                ViewState["ContentTitleList"] = dataList;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("errrrroor...");
            }
        }


        protected void btnUpdateCourseContent_Click(object sender, EventArgs e)
        {
            string contid = Request.Cookies["contid"].Value.ToString();
            System.Diagnostics.Debug.WriteLine("all right..." + contid);

            string courseId = Request.QueryString["Cid"];
            System.Diagnostics.Debug.WriteLine("all right..." + courseId);
            string query = "update CourseContent set ContTitle=@title, TextCont=@text, ImageCont=@img, FileCont=@file, ContentUrl=@url where CourseContId=@cid";
            Utils uObj = new Utils();

            SqlParameter cid = new SqlParameter("@cid", SqlDbType.Int);
            cid.Value = Convert.ToInt32(contid);

            SqlParameter title = new SqlParameter("@title", SqlDbType.Char);
            title.Value = txtContentTitle.Text;

            SqlParameter txt = new SqlParameter("@text", SqlDbType.Char);
            txt.Value = txtCContent.Text;

            // saving image a& file locally
            string imgPath = "";
            string filePath = "";
            string imgfolderPath = Server.MapPath("~/Images/Content/");

            if (ftImage.HasFile)
            {
                string imgName = ftImage.FileName;
                imgPath = "~/Images/Content/" + imgName;
                ftImage.SaveAs(imgfolderPath + Path.GetFileName(ftImage.FileName));
            }
            else
            {
                if (iPath.Text == "No image uploaded yet.")
                {
                    imgPath = "";
                }
                else
                {
                    imgPath = iPath.Text;
                }
            }

            if (ftFile.HasFile)
            {
                string fileName = ftFile.FileName;
                filePath = "~/Images/Content/" + fileName;
                ftImage.SaveAs(imgfolderPath + Path.GetFileName(ftFile.FileName));
            }
            else
            {
                if (fPath.Text == "No file uploaded yet.")
                {
                    filePath = "";
                }
                else
                {
                    filePath = fPath.Text;
                }
            }

            System.Diagnostics.Debug.WriteLine("img: " + imgPath);

            SqlParameter img = new SqlParameter("@img", SqlDbType.Char);
            img.Value = imgPath;
            SqlParameter file = new SqlParameter("@file", SqlDbType.Char);
            file.Value = filePath;
            SqlParameter url = new SqlParameter("@url", SqlDbType.Char);
            url.Value = txtUrl.Text;

            SqlParameter[] parameters = { cid, title, txt, img, file, url };

            if (uObj.DbAction(query, parameters) != null)
            {
                ViewState["contMsg"] = "Content updated successfully.";
                System.Diagnostics.Debug.WriteLine("yooooooooo" + courseId);
                GetContentData(contid);
            }


        }



    }
}