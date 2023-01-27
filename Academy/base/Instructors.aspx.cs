using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Academy
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("all good................");
             
            if (!IsPostBack)
            {
                retrieveData();
                System.Diagnostics.Debug.WriteLine("all good................");

            }
        }


        void retrieveData()
        {
            string query = "SELECT * FROM UserAccount WHERE AccountType=@it";
           
            System.Diagnostics.Debug.WriteLine("all good................");


            Utils uObj = new Utils();


           
            SqlParameter it = new SqlParameter("@it", SqlDbType.Char);
            it.Value = "Instructor";

            SqlParameter[] parameters = { it };

            System.Diagnostics.Debug.WriteLine("all good................");
            
            SqlDataReader sdr = uObj.DbAction(query, parameters);

            // Read the data and store it in the ViewState
            List<object[]> instructorList = new List<object[]>();
            

            while (sdr.Read())
            {
                object[] data = new object[7];
                data[0] = sdr["FullName"];
                data[1] = sdr["Email"];
                data[2] = sdr["Profile_Img"];
                

                instructorList.Add(data);
            }
            sdr.Close();
            ViewState["InstructorData"] = instructorList;
        }

    }
}
}