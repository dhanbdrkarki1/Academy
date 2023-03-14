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

namespace Academy
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                listDropDownCategory();
                populateGridView();
            }
        }


        // get instructor id from db
        string getInstructorId()
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


        //add courses in db
        protected void btnAddCourse(object sender, EventArgs e)
        {

            string instructorId = getInstructorId();
            Utils uObj = new Utils();
            var dateTimeVal = uObj.getDate();
            String query = "insert into Courses(Title,Category,OverView,Rate,CreatedAt, InstructorId) values(@title,@category,@overview,@rate,@createdAt,@id)";

            System.Diagnostics.Debug.WriteLine("Add course: " + ddCategory.SelectedValue);


            SqlParameter title = new SqlParameter("@title", SqlDbType.VarChar);
            title.Value = txtCourseTitle.Text;

            SqlParameter category = new SqlParameter("@category", SqlDbType.Int);
            category.Value = Convert.ToInt32(ddCategory.SelectedIndex + 1);

            SqlParameter overview = new SqlParameter("@overview", SqlDbType.VarChar);
            overview.Value = txtOverView.Text;

            SqlParameter rate = new SqlParameter("@rate", SqlDbType.Int);
            rate.Value = Convert.ToInt32(txtRate.Text);

            SqlParameter createdAt = new SqlParameter("@createdAt", SqlDbType.Date);
            createdAt.Value = dateTimeVal;

            SqlParameter id = new SqlParameter("@id", SqlDbType.Int);
            id.Value = Convert.ToInt32(instructorId);

            SqlParameter[] parameters = { title, category, overview, rate, createdAt, id };

            // insert data into db
            if (uObj.DbAction(query, parameters) != null)
            {
                ClearField();
                populateGridView();
                lblSuccess.Text = "New course added.";
            }
            else
            {
                lblError.Text = "Error found...";

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
            string instructorId = getInstructorId();
            string query = "select c.CourseId, c.Title, cc.Category, c.OverView, c.Rate, c.CreatedAt from Courses c join CourseCategory cc on c.Category = cc.CourseCatId where c.InstructorId='" + instructorId + "'";
            SqlConnection con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@iid", instructorId);
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
                System.Diagnostics.Debug.WriteLine(ex.Message);

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
            // Get the updated values for the row being edited
            int courseId = Convert.ToInt32(gvManageCourse.DataKeys[e.RowIndex].Value);
            string title = (gvManageCourse.Rows[e.RowIndex].FindControl("txtTitle") as TextBox).Text;
            string overview = (gvManageCourse.Rows[e.RowIndex].FindControl("txtOverView") as TextBox).Text;
            int rate = Convert.ToInt32((gvManageCourse.Rows[e.RowIndex].FindControl("txtRate") as TextBox).Text);
            DateTime createdAt = Convert.ToDateTime((gvManageCourse.Rows[e.RowIndex].FindControl("txtCreatedAt") as TextBox).Text);
            string category = (gvManageCourse.Rows[e.RowIndex].FindControl("ddlCat") as DropDownList).SelectedItem.Value;
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
            gvManageCourse.EditIndex = -1;

            // Rebind the GridView to show the updated data
            populateGridView();
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


                        ddList.Items.FindByText(selectedCategory).Selected = true;
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                }
            }

        }

        protected void AddContent_Click(object sender, EventArgs e)
        {
            // Get a reference to the button that was clicked
            Button btn = sender as Button;

            // Get a reference to the row that contains the button
            GridViewRow row = btn.NamingContainer as GridViewRow;
            // Get the title of the course
            Label lblTitle = row.FindControl("lblTitle") as Label;

            Label lblCourseId = row.FindControl("lblCourseId") as Label;
            //adding courseid as cookie
            HttpCookie cookie = new HttpCookie("CourseId");
            cookie.Value = lblCourseId.Text;
            Response.Cookies.Add(cookie);


            string title = lblTitle.Text;
            txtCTitle.Text = title;

            string script1 = "$(document).ready(function(){ $('#btnAddContent').modal('show'); });";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowModalScript", script1, true);
        }

        protected void btnAddCourseContent_Click(object sender, EventArgs e)
        {

            // fetching courseid from cookie
            string course_id = "";
            HttpCookie cookie = Request.Cookies["CourseId"];
            if (cookie != null)
            {
                course_id = cookie.Value;
            }
            Response.Write("<script>alert('text: ' + '" + txtCContent.Text + "')</script>");

            // saving image a& file locally
            string imgPath = "";
            string filePath = "";
            string imgfolderPath = Server.MapPath("~/Images/Content/");

            if (ftImage.HasFile)
            {
                string imgName = ftImage.FileName;
                imgPath = "~/Images/Content/" + imgName;
                ftImage.SaveAs(imgfolderPath + Path.GetFileName(ftImage.FileName));
            }

            if (ftFile.HasFile)
            {
                string fileName = ftFile.FileName;
                filePath = "~/Images/Content/" + fileName;
                ftImage.SaveAs(imgfolderPath + Path.GetFileName(ftFile.FileName));
            }

            //adding into database
            String query = "insert into CourseContent (CId, ContTitle, TextCont, ImageCont, ContentUrl, FileCont) values(@cid,@title,@text,@image,@url,@file)";
            SqlConnection con = new SqlConnection(connectionString);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@cid", course_id);
                cmd.Parameters.AddWithValue("@title", txtContentTitle.Text);
                cmd.Parameters.AddWithValue("@text", txtCContent.Text);
                cmd.Parameters.AddWithValue("@image", imgPath);
                cmd.Parameters.AddWithValue("@url", txtUrl.Text);
                cmd.Parameters.AddWithValue("@file", filePath);

                cmd.ExecuteNonQuery();
                txtContentTitle.Text = txtCContent.Text = txtUrl.Text = "";
                Response.Cookies["CourseId"].Expires = DateTime.Now.AddDays(-1);

                lblSuccess.Text = "Course content added.";
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }



        protected void lnkUpdateContent_Click(object sender, EventArgs e)
        {
            // Get a reference to the button that was clicked
            Button btn = sender as Button;

            // Get a reference to the row that contains the button
            GridViewRow row = btn.NamingContainer as GridViewRow;

            // Get the courseid of the course
            Label lblCourseId = row.FindControl("lblCourseId") as Label;

            //redirecting to update page with id
            int CourseId = Convert.ToInt32(lblCourseId.Text);
            Response.Redirect("../instructor/InstructorCourseContentUpdate.aspx?Cid=" + CourseId);
        }
    }
}