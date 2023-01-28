using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Academy
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            retrieveData();
        }


        void retrieveData()
        {
            Utils uObj = new Utils();

            string username = "";

            if (Session["username"] != null)
            {
                username = Session["username"].ToString();
            }

            // get instructor id
            string accountQuery = "select * from UserAccount where Username=@id";

            string instructorId = uObj.getSpecificData(username, accountQuery, "AccountId");

            // get enrollment data
            string query = "select u.FullName, u.Email, u.Profile_Img, c.Title, cc.Category,  e.TotalAmt, e.EnrollmentDate from EnrollCourse as e " +
                "inner join Courses as c on c.CourseId=e.CourseId " +
                "inner join CourseCategory as cc on cc.CourseCatId=c.Category " +
                "inner join UserAccount as u on u.AccountId=e.StudentId where e.InstructorId=@iid " +
                "order by e.EnrollmentDate desc";


            SqlParameter iid = new SqlParameter("@iid", SqlDbType.Int);
            iid.Value = Convert.ToInt32(instructorId);

            SqlParameter[] parameters = { iid };

            // reading instructor type user from db
            SqlDataReader sdr = uObj.DbAction(query, parameters);

            // Read the data and store it in the ViewState
            List<object[]> dataList = new List<object[]>();
            if (!sdr.HasRows)
            {
                ViewState["msg"] = "No courses enrolled yet!";
            }
            else
            {
                while (sdr.Read())
                {
                    object[] data = new object[7];
                    data[0] = sdr["FullName"];
                    data[1] = sdr["Email"];
                    data[2] = sdr["Profile_Img"];
                    data[3] = sdr["Title"];
                    data[4] = sdr["Category"];
                    data[5] = sdr["TotalAmt"];
                    data[6] = sdr["EnrollmentDate"];


                    System.Diagnostics.Debug.WriteLine("coming--------" + data[0]);

                    dataList.Add(data);
                }

                sdr.Close();
                ViewState["EnrollmentData"] = dataList;
            }
        }
    }
}