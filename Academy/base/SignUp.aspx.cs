using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Academy
{
    public partial class SignUp : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        // validating email
        protected void IsValidEmail(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = FormCustomValidation.IsValidEmail(args.Value.Trim());

            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        //validating password
        protected void Validate_Password(object source, ServerValidateEventArgs args)
        {
            try
            {
                var password = args.Value.Trim();
                args.IsValid = FormCustomValidation.Validate_Password(password);
            }
            catch (Exception)
            {
                args.IsValid = false;
            }
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String query = "insert into UserAccount(FullName,Email,Username,AccountType,Password) values(@name, @email, @uname, @acType, @password)";

                //queries params
                SqlParameter name = new SqlParameter("@name", SqlDbType.Char);
                name.Value = FullName.Text;

                SqlParameter uname = new SqlParameter("@uname", SqlDbType.Char);
                uname.Value = Username.Text;


                SqlParameter email = new SqlParameter("@email", SqlDbType.Char);
                email.Value = Email.Text;

                SqlParameter acType = new SqlParameter("@acType", SqlDbType.Char);
                acType.Value = ddAccountType.SelectedValue;

                SqlParameter password = new SqlParameter("@password", SqlDbType.Char);
                password.Value = ConfirmPassword.Text;

                SqlParameter[] parameters = { name, email, uname, acType, password };
                Utils uObj = new Utils();

                try
                {
                    // insert data into db
                    if (uObj.DbAction(query, parameters) != null)
                    {
                        lblMsg.CssClass = "text-success";
                        lblMsg.Text = "Your account has been created successfully.";
                        // clearing fields
                        uObj.ClearField(FullName, Username, Email, Password, ConfirmPassword);

                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
            else
            {
                lblMsg.CssClass = "text-danger";
                lblMsg.Text = "Fill up all the fields.";
            }
        }

    }
}