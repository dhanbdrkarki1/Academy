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

namespace Academy
{
    public class Utils
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

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


        public string LoginUser(string username, string password)
        {
            User user_obj = new User();
            user_obj.Username = username;
            user_obj.Password = password;

            string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            String query = "select count(*) from UserAccount where Username='" + user_obj.Username + "' and Password='" + user_obj.Password + "' ";
            String ac_type = "select AccountType from UserAccount where username = '" + user_obj.Username + "'";
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlCommand cmdName = new SqlCommand(ac_type, con);

                string user_role = cmdName.ExecuteScalar().ToString();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                cmd.ExecuteNonQuery();
                if (dt.Rows[0][0].ToString() == "1")
                {

                    if (user_role == "Instructor")
                    {
                        return user_role;
                    }
                    else if(user_role == "Student") {
                       return user_role;
                    }
                    else
                    {
                        return user_role;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong.");
            }

            return null;

        }

        public string getDate()
        {
            var dateTime = DateTime.Now;
            var dateTimeVal = dateTime.ToString("yyyy/MM/dd");
            return dateTimeVal;
        }


        // takes query and use it to retrieve/insert data from db
        public SqlDataReader DbAction(string query, SqlParameter[] param)
        {
            System.Diagnostics.Debug.WriteLine("Connecting diag................");


            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                System.Diagnostics.Debug.WriteLine("query= " + query + param);

                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                System.Diagnostics.Debug.WriteLine("Connecting sql................");
                if (param != null){
                    cmd.Parameters.AddRange(param);
                    System.Diagnostics.Debug.WriteLine("Connected sql................");

                }

                try
                {
                    return cmd.ExecuteReader();
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);

                    // Return null if there was an exception
                    return null;
                }

            }
            catch(Exception ex) { 
                Console.WriteLine(ex.Message); 
            }
            return null;
        }


    }
}