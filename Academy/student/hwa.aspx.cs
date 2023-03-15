using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Configuration;
using System.Data.SqlTypes;

namespace Academy.student
{
    public partial class CourseDetail : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if(ViewState["MyCoursesContentData"] != null)
                {
                    System.Diagnostics.Debug.WriteLine("i'm hereeeee....");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("oh.. no please....");
                }
            }
        }

    }
}