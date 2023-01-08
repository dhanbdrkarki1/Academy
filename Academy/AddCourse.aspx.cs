using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing;

namespace Academy
{
    public partial class AddCourse : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                listDropDownCategory();
                populateGridView();
            }
        }

        //add courses in db
        protected void btnAddCourse(object sender, EventArgs e)
        {
            var dateTime = DateTime.Now;
            var dateTimeVal = dateTime.ToString("yyyy/MM/dd");
            String query = "insert into Courses(Title,Category,OverView,Rate,CreatedAt) values('" + txtCourseTitle.Text + "','" + ddCategory.SelectedValue + "','" + txtOverView.Text + "','" + txtRate.Text + "','" + dateTimeVal + "')";

            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = query;
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                ClearField();
                populateGridView();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        //clears field
        void ClearField()
        {
            txtCourseTitle.Text = txtOverView.Text = txtRate.Text = "";
        }

        // populate all the data from db in gridview
        void populateGridView()
        {
            //String query = "select Courses.Title, CourseCategory.Category, OverView, Rate, CreatedAt from Courses inner join CourseCategory on Courses.Category=CourseCategory.CourseCatId";
            string query = "select * from Courses";
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    gvManageCourse.DataSource = dt;
                    gvManageCourse.DataBind();
                }
                else
                {
                    dt.Rows.Add(dt.NewRow());
                    gvManageCourse.DataSource = dt;
                    gvManageCourse.DataBind();
                    gvManageCourse.Rows[0].Cells.Clear();
                    gvManageCourse.Rows[0].Cells.Add(new TableCell());
                    gvManageCourse.Rows[0].Cells[0].ColumnSpan = dt.Columns.Count;
                    gvManageCourse.Rows[0].Cells[0].Text = "No data found...";
                    gvManageCourse.Rows[0].Cells[0].HorizontalAlign = HorizontalAlign.Center;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong.");
            }
        }

        // triggers when edit btn is pressed
        protected void gvManageCourse_RowEditing(object sender, GridViewEditEventArgs e)
        {
            listDropDownCategory();
            gvManageCourse.EditIndex = e.NewEditIndex;

            populateGridView();
        }

        //triggers when cancel btn is pressed
        protected void gvManageCourse_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvManageCourse.EditIndex = -1;
            populateGridView();
        }

        //triggers on clicking update btn and update value in database
        protected void gvManageCourse_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            String query = "update Courses set Title=@Title, Category=@Category, OverView=@OverView, Rate=@Rate, CreatedAt=@CreatedAt where CourseId=@id";
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Title", (gvManageCourse.Rows[e.RowIndex].FindControl("Title") as TextBox).Text.Trim());
                cmd.Parameters.AddWithValue("@Category", (gvManageCourse.Rows[e.RowIndex].FindControl("Category") as TextBox).Text.Trim());
                cmd.Parameters.AddWithValue("@OverView", (gvManageCourse.Rows[e.RowIndex].FindControl("OverView") as TextBox).Text.Trim());
                cmd.Parameters.AddWithValue("@Rate", (gvManageCourse.Rows[e.RowIndex].FindControl("Rate") as TextBox).Text.Trim());
                cmd.Parameters.AddWithValue("@CreatedAt", (gvManageCourse.Rows[e.RowIndex].FindControl("CreatedAt") as TextBox).Text.Trim());

                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvManageCourse.DataKeys[e.RowIndex].Value.ToString()));
                cmd.ExecuteNonQuery();
                gvManageCourse.EditIndex = -1;
                populateGridView();
                lblSuccess.Text = "Selected record updated";
                lblError.Text = "Got error";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong.");

            }
        }

        // delete item from db
        protected void gvManageCourse_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            String query = "DELETE from Courses where CourseID=@id";
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(gvManageCourse.DataKeys[e.RowIndex].Value.ToString()));
                cmd.ExecuteNonQuery();
                populateGridView();
                lblSuccess.Text = "Selected item deleted.";
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }


        // bind all types of category from database
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

        // bind all types of categories from database into gridview of edittemplate
        protected void gvManageCourse_RowDataBound(object sender, GridViewRowEventArgs e)
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
                        ddList.Items.FindByValue(selectedCategory).Selected = true;
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
            }
        }
    }
}