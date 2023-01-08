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


        protected void IsValidEmail(object source, ServerValidateEventArgs args)
        {
            var email = args.Value.Trim();
            {
                var trimmedEmail = email.Trim();

                if (trimmedEmail.EndsWith("."))
                {
                    args.IsValid = false;
                }
                try
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    args.IsValid = addr.Address == trimmedEmail;
                }
                catch
                {
                    args.IsValid = false;
                }
            }
        }

        protected void Validate_Password(object source, ServerValidateEventArgs args)
        {
            try
            {
                var password = args.Value.Trim();
                var hasNumber = new Regex(@"[0-9]+");
                var hasUpperChar = new Regex(@"[A-Z]+");
                var hasMinimum8Chars = new Regex(@".{8,}");
                var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");
                var isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasSymbols.IsMatch(password);

                if (!hasMinimum8Chars.IsMatch(password)){
                    args.IsValid = false;
                }
                else if(!isValidated){
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
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
                String query = "insert into UserAccount(FullName,Email,Username,AccountType,Password) values('" + FullName.Text + "','" + Email.Text + "','" + Username.Text + "','" + ddAccountType.SelectedValue + "','" + ConfirmPassword.Text + "')";
                
                SqlConnection con = new SqlConnection(connectionString);
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    lblMsg.CssClass = "text-success";
                    lblMsg.Text = "Your account has been created successfully.";
                    ClearField();
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


        void ClearField()
        {
            FullName.Text = Username.Text = Email.Text = Password.Text = ConfirmPassword.Text = "";
        }
    }
}