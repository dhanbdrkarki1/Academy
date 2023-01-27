using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

namespace Academy
{
    public partial class InsCourses : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                retrieveCourseData();
            }
        }



        void retrieveCourseData()
        {
            string accountId = Request.QueryString["accountId"];
            System.Diagnostics.Debug.WriteLine("--------", accountId);

            string query = "select * from Courses";
            string accountQuery = "select * from UserAccount where AccountId=@id";
            string categoryQuery = "select * from CourseCategory where CourseCatId=@id";
            string imageQuery = "select top 1 * from CourseContent where CId=@id";


            Utils uObj = new Utils();

            SqlParameter iid = new SqlParameter("@it", SqlDbType.Int);
            iid.Value = Convert.ToInt32(accountId);





            SqlParameter[] parameters = { iid };

            // reading instructor type user from db
            SqlDataReader sdr = uObj.DbAction(query, parameters);

            // Read the data and store it in the ViewState
            List<object[]> dataList = new List<object[]>();
            List<string> idList = new List<string>();
            if (!sdr.HasRows)
            {
                ViewState["msg"] = "No courses available!";
            }
            else
            {
                while (sdr.Read())
                {
                    if (accountId == sdr["Instructorid"].ToString())
                    {
                        object[] data = new object[7];
                        data[0] = sdr["Courseid"];
                        data[1] = sdr["Title"];
                        data[2] = uObj.getSpecificData(sdr["Category"].ToString(), categoryQuery, "Category");
                        data[3] = sdr["OverView"];
                        data[4] = sdr["Rate"];
                        data[5] = uObj.getSpecificData(sdr["Courseid"].ToString(), imageQuery, "ImageCont");
                        data[6] = uObj.getSpecificData(sdr["Instructorid"].ToString(), accountQuery, "FullName");

                        System.Diagnostics.Debug.WriteLine("coming--------" + data[0]);

                        idList.Add(data[0].ToString());

                        dataList.Add(data);
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("Winter is coming--------");
                    }


                }

                sdr.Close();
                ViewState["InstructorCourses"] = dataList;
            }

            
        }

    }
}