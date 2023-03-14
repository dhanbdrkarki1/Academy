<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="Academy.admin.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto|Varela+Round">
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" href="../assets/css/Custom.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <%--<script src="../assets/js/Custom.js"></script>--%>
    <style>
        td > a:first-child,
        td > a:first-child {
            background-color: #4CAF50;
            border-radius: 5px;
            padding: 5px 10px;
            display: flex;
            flex-direction: row;
            align-items: center;
            align-content: center;
        }

        td > a:last-child {
            background-color: #f44336;
            border-radius: 5px;
            padding: 5px 10px;
            display: flex;
            flex-direction: row;
            align-items: center;
            align-content: center;
        }
    </style>
    <main id="main" data-aos="fade-in">

        <!-- ======= Breadcrumbs ======= -->
        <div class="breadcrumbs">
            <div class="container">
                <h2>Mana</h2>
                <p>Add more courses to earn more. The more courses you add, the more rich you go So hurry up and add more content.</p>
            </div>
        </div>
        <!-- End Breadcrumbs -->


        <div class="container">

            <div class="table-wrapper">

                <div class="table-title">
                    <div class="row">
                        <div class="col-sm-6">
                            <h2><b>User Accounts</b></h2>
                        </div>
                        <div class="col-sm-6">
                            <a href="#addEmployeeModal" data-target="#addEmployeeModal" class="btn btn-success" data-toggle="modal"><i class="material-icons">&#xE147;</i> <span>Add User</span></a>
                        </div>
                    </div>
                </div>

                <asp:GridView ID="gvManageCourse" runat="server" AutoGenerateColumns="false" DataKeyNames="CourseId" ShowHeaderWhenEmpty="true"
                    OnRowEditing="gvManageCourse_RowEditing" OnRowCancelingEdit="gvManageCourse_RowCancelingEdit" OnRowUpdating="gvManageCourse_RowUpdating"
                    OnRowDeleting="gvManageCourse_RowDeleting" OnRowDataBound="gvManageCourse_RowDataBound" class="table table-striped table-hover">

                    <Columns>
                        <asp:TemplateField HeaderText="Course ID">
                            <ItemTemplate>
                                <asp:Label ID="lblCourseId" Text='<%# Eval("CourseId") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label ID="lblTitle" Text='<%# Eval("Title") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtTitle" Text='<%# Eval("Title") %>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("Category") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlCat" AppendDataBoundItems="true" runat="server"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Overview">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("OverView") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOverView" Text='<%# Eval("OverView") %>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("Rate") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtRate" Text='<%# Eval("Rate") %>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Created Date">
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedAt" runat="server" Text='<%# Eval("CreatedAt") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCreatedAt" ReadOnly="true" runat="server" Text='<%# Eval("CreatedAt") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:CommandField ButtonType="Link" HeaderText="Course Actions" ShowEditButton="true" ShowDeleteButton="true" />
                        <asp:TemplateField HeaderText="Content Actions" ItemStyle-Width="150">
                            <ItemTemplate>
                                <asp:Button ID="lnkAddContent" OnClick="AddContent_Click" runat="server" CssClass="btn btn-primary" Text="Add Content" data-toggle="modal" data-target="#btnAddContent" />
                                <asp:Button ID="lnkUpdateContent" runat="server" CssClass="btn btn-info" Text="Update Content" data-toggle="modal" data-target="#btnUpdateContent" OnClick="lnkUpdateContent_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="lblSuccess" runat="server" Text="" ForeColor="Green"></asp:Label>
                <br />
                <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>

                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:dbconnection %>" SelectCommand="SELECT * FROM [Courses]" UpdateCommand="update Courses set Title=@Title,Category=@Category,OverView=@OverView, Rate=@Rate,CreatedAt='' where CourseId=@CourseId" DeleteCommand="Delete from Courses where CourseId=@CourseId"></asp:SqlDataSource>
            </div>
        </div>

    </main>

    <!-- courses Modal -->
    <div id="addEmployeeModal" class="modal fade" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Add Course</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form>
                        <div class="form-group">
                            <asp:Label ID="lblCourseTitle" class="col-form-label" runat="server" Text="Course Title"></asp:Label>
                            <asp:TextBox ID="txtCourseTitle" class="form-control" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="#CC3300" ErrorMessage="This field is required." Display="Dynamic" ControlToValidate="txtCourseTitle" ValidationGroup="ba"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblCategory" class="col-form-label" runat="server" Text="Category"></asp:Label>
                            <asp:DropDownList ID="ddCategory" class="form-select" runat="server">
                            </asp:DropDownList>

                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblOverView" class="col-form-label" runat="server" Text="Overview"></asp:Label>
                            <asp:TextBox ID="TextBox1" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="#CC3300" ErrorMessage="This field is required." Display="Dynamic" ValidationGroup="ba" ControlToValidate="txtOverView"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:Label ID="lblRate" class="col-form-label" runat="server" Text="Rate"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="#CC3300" ErrorMessage="This Field is required." ControlToValidate="txtRate" ValidationGroup="ba" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:TextBox ID="TextBox2" class="form-control" runat="server"></asp:TextBox>
                        </div>

                    </form>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCancel" class="btn btn-default" data-dismiss="modal" runat="server" Text="Cancel" />
                    <asp:Button ID="btnAdd" class="btn btn-success" runat="server" Text="Add" ValidationGroup="ba" OnClick="btnAddCourse" />
                </div>

            </div>
        </div>
    </div>


    <%--content modal--%>
    <div class="modal fade" id="btnAddContent" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Course Content</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:Label runat="server" for="txtInput" Text="Course Title:"></asp:Label>
                        <asp:TextBox ID="txtCTitle" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" for="txtInput" Text="Content Title:"></asp:Label>
                        <asp:TextBox ID="txtContentTitle" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Content Text:"></asp:Label>
                        <asp:TextBox ID="txtCContent" Rows="7" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Image:"></asp:Label>
                        <asp:FileUpload ID="ftImage" class="form-control" runat="server" />
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Content Url:"></asp:Label>
                        <asp:TextBox ID="txtUrl" class="form-control" runat="server" TextMode="Url"></asp:TextBox>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Attach Resource File:"></asp:Label>
                        <asp:FileUpload ID="ftFile" class="form-control" runat="server" />
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btnClose" class="btn btn-secondary" data-dismiss="modal" runat="server" Text="Close" />
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Save Changes" OnClick="btnAddCourseContent_Click" />
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
</asp:Content>
