using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using System.Xml.Linq;
using System.IO;

namespace Academy.student
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                retrieveUserData();
                loadUserProfilePic();
            }
        }

        void retrieveUserData()
        {
            string username;
            if (Session["username"] != null)
            {
                try
                {
                    username = Session["username"].ToString();
                    string query = "select FullName, Username,Email,Password from UserAccount where Username=@user";
                    SqlConnection con = new SqlConnection(connectionString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@user", username);

                    cmd.CommandType = CommandType.Text;

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        sdr.Read();
                        txtFullName.Text = sdr["FullName"].ToString();
                        txtUsername.Text = sdr["Username"].ToString();
                        txtEmail.Text = sdr["Email"].ToString();
                        txtPassword.Text = sdr["Password"].ToString();
                        txtConfirmPassword.Text = txtPassword.Text;
                    }
                    con.Close();
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }

            }
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

                if (!hasMinimum8Chars.IsMatch(password))
                {
                    args.IsValid = false;
                }
                else if (!isValidated)
                {
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

        //Update user profile
        protected void btnUpdate_Profile_Click(object sender, EventArgs e)
        {
            string folderPath = Server.MapPath("~/Images/Profile/");
            if (imgUpload.HasFile)
            {
                string fileName = imgUpload.FileName;
                string filePath = "~/Images/Profile/" + fileName;
                imgUpload.SaveAs(folderPath + Path.GetFileName(imgUpload.FileName));

                string username = Session["username"].ToString();
                string query = "update UserAccount set FullName=@FullName, Username=@Username,Email=@Email,Password=@Password, Profile_Img=@ProfileImg where Username=@Username";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                // Add the parameters for the query
                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);
                cmd.Parameters.AddWithValue("@Username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Password", txtConfirmPassword.Text);
                cmd.Parameters.AddWithValue("@ProfileImg", filePath);
                //Execute the query
                cmd.ExecuteNonQuery();

                //Display the Picture in Image control.
                loadUserProfilePic();
            }

        }

        void loadUserProfilePic()
        {
            try
            {
                string username = Session["username"].ToString();
                string query = "select * from UserAccount where username=@username";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.CommandType = CommandType.Text;

                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();
                    if (sdr["Profile_Img"].ToString() != null)
                    {

                        imgUserProfile.ImageUrl = sdr["Profile_Img"].ToString();
                    }
                    else
                    {
                        imgUserProfile.ImageUrl = "~/Images/Profile/user.png";
                    }
                }
                con.Close();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}