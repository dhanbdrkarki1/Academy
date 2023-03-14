using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Academy.admin
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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




        void populateGridView()
        {
            string adminId = getAdminId();
            string query = "select AccountId, FullName, Email, Username, AccountType, Password from UserAccount";
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
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


        void listDropDownCategory()
        {
            String query = "select * from CourseCategory";
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.Text;

                ddCategory.DataSource = cmd.ExecuteReader();
                ddCategory.DataTextField = "Category";
                ddCategory.DataValueField = "CourseCatId";
                ddCategory.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong.");
            }
        }


        // triggers when edit btn is pressed
        protected void gvManageUserAccount_RowEditing(object sender, GridViewEditEventArgs e)
        {
            listDropDownCategory();
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
            int courseId = Convert.ToInt32(gvManageUserAccount.DataKeys[e.RowIndex].Value);
            string title = (gvManageUserAccount.Rows[e.RowIndex].FindControl("txtTitle") as TextBox).Text;
            string overview = (gvManageUserAccount.Rows[e.RowIndex].FindControl("txtOverView") as TextBox).Text;
            int rate = Convert.ToInt32((gvManageUserAccount.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text);
            DateTime createdAt = Convert.ToDateTime((gvManageUserAccount.Rows[e.RowIndex].FindControl("txtCreatedAt") as TextBox).Text);
            string category = (gvManageUserAccount.Rows[e.RowIndex].FindControl("ddlCat") as DropDownList).SelectedItem.Value;
            System.Diagnostics.Debug.WriteLine("all good:-------" + category + "rate: " + rate.ToString());

            // Update the database with the updated values"
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = "UPDATE Courses SET Title = @Title, Category = @Category, OverView = @OverView, Rate = @Rate, CreatedAt = @CreatedAt WHERE CourseId = @CourseId";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Title", title);
                    cmd.Parameters.AddWithValue("@Category", category);
                    cmd.Parameters.AddWithValue("@OverView", overview);
                    cmd.Parameters.AddWithValue("@Rate", rate);
                    cmd.Parameters.AddWithValue("@CreatedAt", createdAt);
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    cmd.ExecuteNonQuery();
                }
                con.Close();
                //Response.Redirect(Request.Url.AbsoluteUri);
            }

            // Cancel the edit mode for the row
            gvManageUserAccount.EditIndex = -1;

            // Rebind the GridView to show the updated data
            populateGridView();
        }


        // delete item from db
        protected void gvManageUserAccount_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String query = "DELETE from Courses where CourseID=@id";
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

        // bind all types of categories from database into gridview of edittemplate
        protected void gvManageUserAccount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    String query = "select * from CourseCategory";

                    SqlConnection con = new SqlConnection(connectionString);
                    try
                    {
                        DropDownList ddList = (DropDownList)e.Row.FindControl("ddlCat");
                        con.Open();
                        SqlCommand cmd = new SqlCommand(query, con);
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        cmd.ExecuteNonQuery();
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        ddList.DataSource = dt;
                        ddList.DataTextField = "Category";
                        ddList.DataValueField = "CourseCatId";
                        ddList.DataBind();
                        string selectedCategory = DataBinder.Eval(e.Row.DataItem, "Category").ToString();


                        ddList.Items.FindByText(selectedCategory).Selected = true;
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine("Something went wrong.: " + ex.Message);
                    }
                }
            }

        }





    }
}