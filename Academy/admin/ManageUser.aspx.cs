using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace Academy.admin
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listDropDownAccountType();
                populateGridView();
            }
        }


        string getAdminId()
        {

            Utils uObj = new Utils();

            string username = "";

            if (Session["username"] != null)
            {
                username = Session["username"].ToString();
            }
            string insQ = "select AccountId from UserAccount where username = @id";
            string instructorId = uObj.getSpecificData(username, insQ, "AccountId");
            System.Diagnostics.Debug.WriteLine("Instructor id................", instructorId);

            return instructorId;
        }



        // populating gridview
        void populateGridView()
        {
            Utils uObj = new Utils();

            string username = "";

            if (Session["username"] != null)
            {
                username = Session["username"].ToString();
            }
            string adminId = getAdminId();
            string query = "select AccountId, FullName, Email, Username, AccountType, Password from UserAccount where not Username=@uname";
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(query, con);
                    sda.SelectCommand.Parameters.AddWithValue("@uname", username);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        gvManageUserAccount.DataSource = dt;
                        gvManageUserAccount.DataBind();
                    }
                    else
                    {
                        dt.Rows.Add(dt.NewRow());
                        gvManageUserAccount.DataSource = dt;
                        gvManageUserAccount.DataBind();
                        gvManageUserAccount.Rows[0].Cells.Clear();
                        gvManageUserAccount.Rows[0].Cells.Add(new TableCell());
                        gvManageUserAccount.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                        gvManageUserAccount.Rows[0].Cells[0].Text = "No data found...";
                        gvManageUserAccount.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }


        // bind all types of category from database
        void listDropDownAccountType()
        {
            String query = "SELECT DISTINCT AccountType FROM UserAccount";
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                ddUserAccountType.DataSource = cmd.ExecuteReader();
                ddUserAccountType.DataTextField = "AccountType";
                ddUserAccountType.DataValueField = "AccountType";
                ddUserAccountType.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong.");
            }
        }



        //triggers when edit btn is pressed
        protected void gvManageUserAccount_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvManageUserAccount.EditIndex = e.NewEditIndex;

            populateGridView();
        }

        //triggers when cancel btn is pressed
        protected void gvManageUserAccount_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvManageUserAccount.EditIndex = -1;
            populateGridView();
        }



        //triggers on clicking update btn and update value in database
        protected void gvManageUserAccount_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get the updated values for the row being edited
            int accountId = Convert.ToInt32(gvManageUserAccount.DataKeys[e.RowIndex].Value);
            string fullName = (gvManageUserAccount.Rows[e.RowIndex].FindControl("txtFullName") as TextBox).Text;
            string email = (gvManageUserAccount.Rows[e.RowIndex].FindControl("txtEmail") as TextBox).Text;
            string username = (gvManageUserAccount.Rows[e.RowIndex].FindControl("txtUsername") as TextBox).Text;

            string accountType = (gvManageUserAccount.Rows[e.RowIndex].FindControl("ddlAccountType") as DropDownList).SelectedItem.Value;
            //string accountType = (gvManageUserAccount.Rows[e.RowIndex].FindControl("txtAccountType") as TextBox).Text;
            string password = (gvManageUserAccount.Rows[e.RowIndex].FindControl("txtPassword") as TextBox).Text;
            //string profileImg = (gvManageUserAccount.Rows[e.RowIndex].FindControl("txtProfileImg") as TextBox).Text;

            //string category = (gvManageUserAccount.Rows[e.RowIndex].FindControl("ddlCat") as DropDownList).SelectedItem.Value;
            //System.Diagnostics.Debug.WriteLine("all good:-------" + category + "rate: " + rate.ToString());

            // Update the database with the updated values"
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "UPDATE UserAccount SET FullName=@f, Email=@e, Username=@u, AccountType=@ac, Password=@p WHERE AccountId=@aid";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@f", fullName);
                    cmd.Parameters.AddWithValue("@e", email);
                    cmd.Parameters.AddWithValue("@u", username);
                    cmd.Parameters.AddWithValue("@ac", accountType);
                    cmd.Parameters.AddWithValue("@p", password);
                    cmd.Parameters.AddWithValue("@aid", accountId);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                //Response.Redirect(Request.Url.AbsoluteUri);
            }

            // Cancel the edit mode for the row
            gvManageUserAccount.EditIndex = -1;

            // Rebind the GridView to show the updated data
            populateGridView();
            lblSuccess.Text = "Selected item updated.";
        }


        // delete item from db
        protected void gvManageUserAccount_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String query = "DELETE from UserAccount where AccountId=@id";
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvManageUserAccount.DataKeys[e.RowIndex].Value.ToString()));
                cmd.ExecuteNonQuery();
                populateGridView();
                lblSuccess.Text = "Selected item deleted.";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        //bind all types of categories from database into gridview of edittemplate
        protected void gvManageUserAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {


                    String query = "SELECT DISTINCT AccountType FROM UserAccount";
                    DropDownList ddList = (DropDownList)e.Row.FindControl("ddlAccountType");
                    SqlConnection con = new SqlConnection(connectionString);
                    try
                    {

                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ddList.DataSource = dt;
                        ddList.DataTextField = "AccountType";
                        ddList.DataValueField = "AccountType";
                        ddList.DataBind();
                        string selectedCategory = DataBinder.Eval(e.Row.DataItem, "AccountType").ToString();
                        System.Diagnostics.Debug.WriteLine("selected" + selectedCategory);

                        ddList.Items.FindByText(selectedCategory).Selected = true;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Something went wrong.: " + ex.Message);
                    }
                }
            }

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


        // adding user to db
        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                String query = "insert into UserAccount(FullName,Email,Username,AccountType,Password) values(@name, @email, @uname, @acType, @password)";

                //queries params
                SqlParameter name = new SqlParameter("@name", SqlDbType.Char);
                name.Value = txtFullName.Text;

                SqlParameter uname = new SqlParameter("@uname", SqlDbType.Char);
                uname.Value = txtUsername.Text;


                SqlParameter email = new SqlParameter("@email", SqlDbType.Char);
                email.Value = txtEmail.Text;

                SqlParameter acType = new SqlParameter("@acType", SqlDbType.Char);
                acType.Value = ddUserAccountType.SelectedValue;

                SqlParameter password = new SqlParameter("@password", SqlDbType.Char);
                password.Value = txtConfirmPassword.Text;

                SqlParameter[] parameters = { name, email, uname, acType, password };
                Utils uObj = new Utils();

                try
                {
                    // insert data into db
                    if (uObj.DbAction(query, parameters) != null)
                    {
                        lblSuccess.CssClass = "text-success";
                        lblSuccess.Text = "Your account has been created successfully.";
                        // clearing fields
                        uObj.ClearField(txtFullName, txtUsername, txtEmail, txtPassword, txtConfirmPassword);
                        populateGridView();
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