﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Academy.student
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                System.Diagnostics.Debug.WriteLine("i'm normal....");
                if (ViewState["MyCoursesContentData"] != null)
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