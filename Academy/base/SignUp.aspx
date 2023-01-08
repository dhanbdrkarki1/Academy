<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="Academy.SignUp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sign Up</title>
    <link href="../assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <section class="vh-100" style="background-color: #eee;">
        <div class="container h-100">
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col-lg-12 col-xl-11">
                    <div class="card text-black" style="border-radius: 25px;">
                        <div class="card-body p-md-5">
                            <div class="row justify-content-center">
                                <div class="col-md-10 col-lg-6 col-xl-5 order-2 order-lg-1">

                                    <p class="text-center h1 fw-bold mb-3 mx-1 mx-md-4 mt-4">Sign up Academy</p>

                                    <form class="mx-1 mx-md-4" runat="server">

                                        <div class="d-flex flex-row align-items-center mb-3">
                                            <i class="fas fa-user fa-lg me-3 fa-fw"></i>
                                            <div class="form-outline flex-fill mb-0">
                                                <asp:Label ID="Label1" runat="server" class="form-label" Text="Full Name"></asp:Label>
                                                <asp:TextBox ID="FullName" class="form-control" runat="server"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="d-flex flex-row align-items-center mb-3">
                                            <i class="fas fa-envelope fa-lg me-3 fa-fw"></i>
                                            <div class="form-outline flex-fill mb-0">
                                                <asp:Label ID="Label2" class="form-label" runat="server" Text="Username"></asp:Label>
                                                <asp:TextBox ID="Username" class="form-control" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This field is required." ControlToValidate="Username" ValidationGroup="vs" Display="Dynamic" ForeColor="#CC3300"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="d-flex flex-row align-items-center mb-3">
                                            <i class="fas fa-envelope fa-lg me-3 fa-fw"></i>
                                            <div class="form-outline flex-fill mb-0">
                                                <asp:Label ID="Label3" class="form-label" runat="server" Text="Email"></asp:Label>
                                                <asp:TextBox ID="Email" class="form-control" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This field is required." ValidationGroup="vs" Display="Dynamic" ControlToValidate="Email" ForeColor="#CC3300"></asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Invalid Email Address" OnServerValidate="IsValidEmail" ControlToValidate="Email" Display="Dynamic" ValidationGroup="vs" ValidateEmptyText="true" ForeColor="#CC3300"></asp:CustomValidator>
                                            </div>
                                        </div>

                                        <div class="d-flex flex-row align-items-center mb-3">
                                            <i class="fas fa-envelope fa-lg me-3 fa-fw"></i>
                                            <div class="form-outline flex-fill mb-0">
                                                <asp:Label ID="AccountType" class="form-label" runat="server" Text="AccountType"></asp:Label>
                                                <asp:DropDownList ID="ddAccountType" class="form-select" runat="server">
                                                    <asp:ListItem Text="Student" Value="Student" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="Instructor" Text="Instructor"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="d-flex flex-row align-items-center mb-3">
                                            <i class="fas fa-lock fa-lg me-3 fa-fw"></i>
                                            <div class="form-outline flex-fill mb-0">
                                                <asp:Label ID="Label4" runat="server" class="form-label" Text="Password"></asp:Label>
                                                <asp:TextBox ID="Password" class="form-control" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="This field is required." ValidationGroup="vs" Display="Dynamic" ControlToValidate="Password" ForeColor="#CC3300"></asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="Password must contain 8 or more characters." OnServerValidate="Validate_Password" ControlToValidate="Password" ValidationGroup="vs" Display="Dynamic" ForeColor="#CC3300"></asp:CustomValidator>
                                            </div>
                                        </div>

                                        <div class="d-flex flex-row align-items-center mb-3">
                                            <i class="fas fa-key fa-lg me-3 fa-fw"></i>
                                            <div class="form-outline flex-fill mb-0">
                                                <asp:Label ID="Label5" runat="server" class="form-label" Text="Confirm Password"></asp:Label>
                                                <asp:TextBox ID="ConfirmPassword" class="form-control" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="This field is required." ValidationGroup="vs" Display="Dynamic" ControlToValidate="ConfirmPassword" ForeColor="#CC3300"></asp:RequiredFieldValidator><asp:CustomValidator ID="CustomValidator3" runat="server" ErrorMessage="Password must contain 8 or more characters." OnServerValidate="Validate_Password" ControlToValidate="ConfirmPassword" ValidationGroup="vs" Display="Dynamic" ForeColor="#CC3300"></asp:CustomValidator>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password doesn't match." ControlToValidate="ConfirmPassword" Display="Dynamic" ControlToCompare="Password" ForeColor="#CC3300"></asp:CompareValidator>
                                            </div>
                                        </div>

                                        <div class="text-center text-lg-start mt-2 pt-2">
                                            <asp:Button ID="btnSignUp" runat="server" Text="Register" class="btn btn-primary btn-lg" Style="padding-left: 2.5rem; padding-right: 2.5rem;" ValidationGroup="vs" OnClick="btnSignUp_Click" />
                                            <p class="small fw-bold mt-2 pt-1 mb-0">
                                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                                            </p>
                                            <p class="small fw-bold mt-2 pt-1 mb-0">
                                                Already have an account? <a href="Login.aspx"
                                                    class="info">Login</a>
                                            </p>

                                        </div>


                                    </form>

                                </div>
                                <div class="col-md-10 col-lg-6 col-xl-7 d-flex align-items-center order-1 order-lg-2">

                                    <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-registration/draw1.webp"
                                        class="img-fluid" alt="Sample image" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</body>
</html>
