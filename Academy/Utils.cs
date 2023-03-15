using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Drawing.Imaging;
using System.Security.Authentication;
using System.Web.UI.WebControls;

namespace Academy
{
    public class Utils
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;



        // verifying user details
        public string LoginUser(string username, string password)
        {
            User user_obj = new User();
            user_obj.Username = username;
            user_obj.Password = password;

            String accountQuery = "select AccountType from UserAccount where Username=@id";
            String userCountQuery = "select count(*) as userCount from UserAccount where Username=@user and Password=@pswd";
            // get user account type
            String user_role = getSpecificData(user_obj.Username, accountQuery, "AccountType");

            SqlParameter user = new SqlParameter("@user", SqlDbType.Char);
            user.Value = user_obj.Username;

            SqlParameter pswd = new SqlParameter("@pswd", SqlDbType.Char);
            pswd.Value = password;

            SqlParameter[] parameters = { user, pswd };

            try
            {
                SqlDataReader sdr = DbAction(userCountQuery, parameters);
                while (sdr.Read())
                {
                    if (sdr["userCount"].ToString() == "1")
                    {
                        System.Diagnostics.Debug.WriteLine("user login checking");

                        if (user_role == "Instructor")
                        {
                            return user_role;
                        }
                        else if (user_role == "Student")
                        {
                            return user_role;
                        }
                        else
                        {
                            return user_role;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Something went wrong.");
            }
            return null;
        }

        // get current time
        public string getDate()
        {
            var dateTime = DateTime.Now;
            var dateTimeVal = dateTime.ToString("yyyy/MM/dd");
            return dateTimeVal;
        }


        // return only single needed column
        public string getSpecificData(string searchBy, string query, string column)
        {
            string result = "";

            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", searchBy);
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    result = sdr[column].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


        // takes query and use it to retrieve/insert/delete data using params from db
        public SqlDataReader DbAction(string query, SqlParameter[] param = null)
        {
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                System.Diagnostics.Debug.WriteLine("query= " + query + param);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                // executing query with  parameters
                if (param != null)
                {
                    cmd.Parameters.AddRange(param);
                    try
                    {
                        return cmd.ExecuteReader();
                    }
                    catch (SqlException ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    System.Diagnostics.Debug.WriteLine("passed................");
                }

                // executing query with no parameters
                try
                {
                    return cmd.ExecuteReader();
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }


        // clearing empty fields
        public void ClearField(params TextBox[] textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Text = "";
            }
        }
    }
}