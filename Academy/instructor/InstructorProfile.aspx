<%@ Page Title="My Profile" Language="C#" MasterPageFile="InstructorPage.Master" AutoEventWireup="true" CodeBehind="InstructorProfile.aspx.cs" Inherits="Academy.WebForm11" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .profile-pic {
            max-width: 200px;
            max-height: 200px;
            margin: 0 auto;
            border-radius: 50%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section style="background-color: #eee;">
        <div class="container">

            <br>
            <div class="row">
                <div class="col-md-3">
                    <div class="card card-body">
                        <a class="btn btn-warning" href="#">&#8592; Back to Course</a>
                        <hr>
                        <h3 style="text-align: center">Account Settings</h3>
                        <hr>
                        <asp:Image ID="imgUserProfile" runat="server" ImageUrl='<%# Eval("Profile_Img") %>' />
                    </div>
                </div>
                <div class="col-md-9">
                    <div class="card card-body">

                        <div class="row justify-content-center">
                            <div class="col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1">
                                <form class="mx-1 mx-md-4">
                                    <div class="d-flex flex-row align-items-center mb-3">
                                        <i class="fas fa-user fa-lg me-3 fa-fw"></i>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:Label ID="Label1" runat="server" class="form-label" Text="Full Name"></asp:Label>
                                            <asp:TextBox ID="txtFullName" class="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="d-flex flex-row align-items-center mb-3">
                                        <i class="fas fa-envelope fa-lg me-3 fa-fw"></i>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:Label ID="Label2" class="form-label" runat="server" Text="Username"></asp:Label>
                                            <asp:TextBox ID="txtUsername" class="form-control" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This field is required." ControlToValidate="txtUsername" ValidationGroup="vs" Display="Dynamic" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>

                                    <div class="d-flex flex-row align-items-center mb-3">
                                        <i class="fas fa-envelope fa-lg me-3 fa-fw"></i>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:Label ID="Label3" class="form-label" runat="server" Text="Email"></asp:Label>
                                            <asp:TextBox ID="txtEmail" class="form-control" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This field is required." ValidationGroup="vs" Display="Dynamic" ControlToValidate="txtEmail" ForeColor="#CC3300"></asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Invalid Email Address" OnServerValidate="IsValidEmail" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="vs" ValidateEmptyText="true" ForeColor="#CC3300"></asp:CustomValidator>
                                        </div>
                                    </div>


                                    <div class="d-flex flex-row align-items-center mb-3">
                                        <i class="fas fa-envelope fa-lg me-3 fa-fw"></i>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:Label ID="lblImage" runat="server" CssClass="form-label" Text="Profile Image"></asp:Label>
                                            <asp:FileUpload ID="imgUpload" CssClass="form-control" runat="server" />
                                        </div>
                                    </div>


                                    <div class="d-flex flex-row align-items-center mb-3">
                                        <i class="fas fa-lock fa-lg me-3 fa-fw"></i>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:Label ID="Label4" runat="server" class="form-label" Text="Password"></asp:Label>
                                            <asp:TextBox ID="txtPassword" class="form-control" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="This field is required." ValidationGroup="vs" Display="Dynamic" ControlToValidate="txtPassword" ForeColor="#CC3300"></asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Password must contain 8 or more characters." OnServerValidate="Validate_Password" ControlToValidate="txtPassword" ValidationGroup="vs" Display="Dynamic" ForeColor="#CC3300"></asp:CustomValidator>
                                        </div>
                                    </div>

                                    <div class="d-flex flex-row align-items-center mb-3">
                                        <i class="fas fa-key fa-lg me-3 fa-fw"></i>
                                        <div class="form-outline flex-fill mb-0">
                                            <asp:Label ID="Label5" runat="server" class="form-label" Text="Confirm Password"></asp:Label>
                                            <asp:TextBox ID="txtConfirmPassword" class="form-control" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="This field is required." ValidationGroup="vs" Display="Dynamic" ControlToValidate="txtConfirmPassword" ForeColor="#CC3300"></asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="Password must contain 8 or more characters." OnServerValidate="Validate_Password" ControlToValidate="txtConfirmPassword" ValidationGroup="vs" Display="Dynamic" ForeColor="#CC3300"></asp:CustomValidator>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password doesn't match." ControlToValidate="txtConfirmPassword" Display="Dynamic" ControlToCompare="txtPassword" ForeColor="#CC3300"></asp:CompareValidator>
                                        </div>
                                    </div>

                                    <div class="text-center text-lg-start mt-2 pt-2">
                                        <asp:Button ID="btnUpdateProfile" runat="server" Text="Update" class="btn btn-primary btn-lg" Style="padding-left: 2.5rem; padding-right: 2.5rem;" ValidationGroup="vs" OnClick="btnUpdate_Profile_Click" />


                                    </div>


                                </form>

                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col-lg-12 col-xl-11">
                </div>
            </div>
        </div>
    </section>
</asp:Content>
