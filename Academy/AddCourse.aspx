<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCourse.aspx.cs" Inherits="Academy.AddCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Course</title>
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto|Varela+Round">
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css">
    <link rel="stylesheet" href="assets/css/Custom.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
    <script src="assets/js/Custom.js"></script>
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
</head>
<body>

    <form id="form1" runat="server">
        <div class="container">

            <div class="table-wrapper">

                <div class="table-title">
                    <div class="row">
                        <div class="col-sm-6">
                            <h2>My <b>Courses</b></h2>
                        </div>
                        <div class="col-sm-6">
                            <a href="#addEmployeeModal" class="btn btn-success" data-toggle="modal"><i class="material-icons">&#xE147;</i> <span>Add New Course</span></a>
                            <asp:LinkButton ID="btnGoBack" class="btn btn-info" runat="server"><i class="material-icons">arrow_back</i> <span>Go Back</span></asp:LinkButton>

                        </div>
                    </div>
                </div>



                <asp:GridView ID="gvManageCourse" runat="server" AutoGenerateColumns="false" DataKeyNames="CourseID"
                    ShowHeaderWhenEmpty="true" OnRowEditing="gvManageCourse_RowEditing" OnRowCancelingEdit="gvManageCourse_RowCancelingEdit"
                    OnRowUpdating="gvManageCourse_RowUpdating" OnRowDeleting="gvManageCourse_RowDeleting" OnRowDataBound="gvManageCourse_RowDataBound" class="table table-striped table-hover">

                    <Columns>
                        <asp:TemplateField HeaderText="Title">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("Title") %>' runat="server" />
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

                        <asp:CommandField ButtonType="Link" HeaderText="Actions" ShowEditButton="true" ShowDeleteButton="true"
                            ItemStyle-Width="150" />
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Label ID="lblSuccess" runat="server" Text="" ForeColor="Green"></asp:Label>
                <br />
                <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>

                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:dbconnection %>" SelectCommand="SELECT * FROM [Courses]" UpdateCommand="update Courses set Title=@Title,Category=@Category,OverView=@OverView, Rate=@Rate,CreatedAt='' where CourseId=@CourseId" DeleteCommand="Delete from Courses where CourseId=@CourseId"></asp:SqlDataSource>


            </div>
        </div>





        <!-- Edit/Update/Delete courses -->
        <div id="addEmployeeModal" class="modal fade">
            <div class="modal-dialog">
                <div class="modal-content">
                    <form>
                        <div class="modal-header">
                            <h4 class="modal-title">Add Course</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:Label ID="lblCourseTitle" runat="server" Text="Course Title"></asp:Label>
                                <asp:TextBox ID="txtCourseTitle" class="form-control" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="#CC3300" ErrorMessage="This field is required." Display="Dynamic" ControlToValidate="txtCourseTitle" ValidationGroup="ba"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblCategory" runat="server" Text="Category"></asp:Label>
                                <asp:DropDownList ID="ddCategory" class="form-select" runat="server">
                                </asp:DropDownList>

                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblOverView" runat="server" Text="Overview"></asp:Label>
                                <asp:TextBox ID="txtOverView" runat="server" TextMode="MultiLine"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ForeColor="#CC3300" ErrorMessage="This field is required." Display="Dynamic" ValidationGroup="ba" ControlToValidate="txtOverView"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblRate" runat="server" Text="Rate"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ForeColor="#CC3300" ErrorMessage="This Field is required." ControlToValidate="txtRate" ValidationGroup="ba" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtRate" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="modal-footer">
                            <asp:Button ID="btnCancel" class="btn btn-default" data-dismiss="modal" runat="server" Text="Cancel" />
                            <asp:Button ID="btnAdd" class="btn btn-success" runat="server" Text="Add" ValidationGroup="ba" OnClick="btnAddCourse" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </form>

</body>
</html>
