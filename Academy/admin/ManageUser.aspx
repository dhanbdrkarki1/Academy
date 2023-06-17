<%@ Page Title="Manage User" Language="C#" MasterPageFile="~/admin/AdminPage.Master" AutoEventWireup="true" CodeBehind="ManageUser.aspx.cs" Inherits="Academy.admin.WebForm3" %>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" data-aos="fade-in">

        <!-- ======= Breadcrumbs ======= -->
        <div class="breadcrumbs">
            <div class="container">
                <h2>Manage Users</h2>
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
                            <a href="#addUserModal" data-target="#addUserModal" class="btn btn-success" data-toggle="modal"><i class="material-icons">&#xE147;</i> <span>Add User</span></a>
                        </div>
                    </div>
                </div>


                <%--                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="AccountId" ShowHeaderWhenEmpty="true"
                    OnRowEditing="gvManageUserAccount_RowEditing" OnRowCancelingEdit="gvManageUserAccount_RowCancelingEdit" OnRowUpdating="gvManageUserAccount_RowUpdating"
                    OnRowDeleting="gvManageUserAccount_RowDeleting" OnRowDataBound="gvManageUserAccount_RowDataBound" class="table table-striped table-hover">--%>


                <asp:GridView ID="gvManageUserAccount" runat="server" AutoGenerateColumns="false" DataKeyNames="AccountId" ShowHeaderWhenEmpty="true"
                    OnRowEditing="gvManageUserAccount_RowEditing" OnRowCancelingEdit="gvManageUserAccount_RowCancelingEdit" OnRowUpdating="gvManageUserAccount_RowUpdating"
                    OnRowDeleting="gvManageUserAccount_RowDeleting"
                    OnRowDataBound="gvManageUserAccount_RowDataBound"
                    class="table table-striped table-hover">

                    <Columns>
                        <asp:TemplateField HeaderText="Account ID">
                            <ItemTemplate>
                                <asp:Label ID="lblAccountId" Text='<%# Eval("AccountId") %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Full Name">
                            <ItemTemplate>
                                <asp:Label ID="lblFullName" Text='<%# Eval("FullName") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtFullName" Text='<%# Eval("FullName") %>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Email">
                            <ItemTemplate>
                                <asp:Label ID="lblEmail" Text='<%# Eval("Email") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtEmail" Text='<%# Eval("Email") %>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Username">
                            <ItemTemplate>
                                <asp:Label ID="txtUsername" Text='<%# Eval("Username") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUsername" Text='<%# Eval("Username") %>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="AccountType">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("AccountType") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlAccountType" AppendDataBoundItems="true" runat="server"></asp:DropDownList>

                            </EditItemTemplate>
                        </asp:TemplateField>

                        <%--                        <asp:TemplateField HeaderText="AccountType">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("AccountType") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtAccountType" Text='<%# Eval("AccountType") %>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>--%>

                        <asp:TemplateField HeaderText="Password">
                            <ItemTemplate>
                                <asp:Label Text='<%# Eval("Password") %>' runat="server" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtPassword" Text='<%# Eval("Password") %>' runat="server" />
                            </EditItemTemplate>
                        </asp:TemplateField>


                        <asp:CommandField ButtonType="Link" HeaderText="Account Actions" ShowEditButton="true" ShowDeleteButton="true" />
                    </Columns>
                </asp:GridView>

                <br />
                <asp:Label ID="lblSuccess" runat="server" Text="" ForeColor="Green"></asp:Label>
                <br />
                <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>

                <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString="<%$ ConnectionStrings:dbconnection %>" SelectCommand="SELECT * FROM [UserAccount]" UpdateCommand="update UserAccount set FullName=@FullName,Email=@Email,Username=@Username, AccountType=@AccountType,Profile_Img='' where AccountId=@AccountId" DeleteCommand="Delete from UserAccount where AccountId=@AccountId"></asp:SqlDataSource>

            </div>
        </div>

    </main>

    <!-- user Modal -->
    <div id="addUserModal" class="modal fade" tabindex="-1" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">

                <div class="modal-header">
                    <h5 class="modal-title">Add User</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <form>
                        <div class="form-group">
                            <asp:Label ID="lblFullName" class="col-form-label" runat="server" Text="Full Name"></asp:Label>
                            <asp:TextBox ID="txtFullName" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="#CC3300" ErrorMessage="This field is required." Display="Dynamic" ControlToValidate="txtFullName" ValidationGroup="ba"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblEmail" class="col-form-label" runat="server" Text="Email"></asp:Label>
                            <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ForeColor="#CC3300" ErrorMessage="This field is required." Display="Dynamic" ControlToValidate="txtEmail" ValidationGroup="ba"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Invalid Email Address" OnServerValidate="IsValidEmail" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="ba" ValidateEmptyText="true" ForeColor="#CC3300"></asp:CustomValidator>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblUsername" class="col-form-label" runat="server" Text="Username"></asp:Label>
                            <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ForeColor="#CC3300" ErrorMessage="This field is required." Display="Dynamic" ControlToValidate="txtUsername" ValidationGroup="ba"></asp:RequiredFieldValidator>
                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblAccountType" class="col-form-label" runat="server" Text="Acount Type"></asp:Label>
                            <asp:DropDownList ID="ddUserAccountType" class="form-select" runat="server">
                            </asp:DropDownList>

                        </div>

                        <div class="form-group">
                            <asp:Label ID="lblPassword" runat="server" class="col-form-label" Text="Password"></asp:Label>
                            <asp:TextBox ID="txtPassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="This field is required." ValidationGroup="ba" Display="Dynamic" ControlToValidate="txtPassword" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Password must contain 8 or more characters." OnServerValidate="Validate_Password" ControlToValidate="txtPassword" ValidationGroup="vs" Display="Dynamic" ForeColor="#CC3300"></asp:CustomValidator>
                        </div>


                        <div class="form-group">
                            <asp:Label ID="lblConfirmPassword" runat="server" class="col-form-label" Text="Confirm Password"></asp:Label>
                            <asp:TextBox ID="txtConfirmPassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This field is required." ValidationGroup="ba" Display="Dynamic" ControlToValidate="txtConfirmPassword" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="Password must contain 8 or more characters." OnServerValidate="Validate_Password" ControlToValidate="txtConfirmPassword" ValidationGroup="ba" Display="Dynamic" ForeColor="#CC3300"></asp:CustomValidator>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password doesn't match." ControlToValidate="txtConfirmPassword" Display="Dynamic" ControlToCompare="txtPassword" ForeColor="#CC3300"></asp:CompareValidator>
                        </div>

                    </form>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnCancel" class="btn btn-default" data-dismiss="modal" runat="server" Text="Cancel" />
                    <asp:Button ID="btnAdd" class="btn btn-success" runat="server" Text="Add" ValidationGroup="ba" OnClick="btnAddUser_Click" />
                    <br />
                    <div class="small fw-bold mt-2 pt-1 mb-0">
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>
