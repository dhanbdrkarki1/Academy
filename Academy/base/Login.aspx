<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Academy.login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="../assets/vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../assets/css/login.css" rel="stylesheet" />
</head>
<body>
    <section class="vh-100">
        <div class="container-fluid h-custom">
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col-md-9 col-lg-6 col-xl-5">
                    <img src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-login-form/draw2.webp"
                        class="img-fluid" alt="Sample image" />
                </div>
                <div class="col-md-8 col-lg-6 col-xl-4 offset-xl-1">

                    <form runat="server">

                        <p class="text-center h1 fw-bold mb-5 mx-1 mx-md-4 mt-4">Login Academy</p>

                        <!-- Username input -->
                        <div class="form-outline mb-4">
                            <asp:Label ID="Label1" class="form-label" runat="server" Text="Username"></asp:Label>
                            <asp:TextBox ID="Username" class="form-control form-control-lg" runat="server"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="This field is required." Display="Dynamic" ForeColor="#CC3300" ControlToValidate="Username" ValidationGroup="vl"></asp:RequiredFieldValidator>
                        </div>

                        <!-- Password input -->
                        <div class="form-outline mb-3">
                            <asp:Label ID="Label2" runat="server" class="form-label" Text="Password"></asp:Label>
                            <asp:TextBox ID="Password" class="form-control form-control-lg" runat="server" TextMode="Password"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="This field is required." ValidationGroup="vl" ForeColor="#CC3300" ControlToValidate="Password" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>

                        <div class="d-flex justify-content-between align-items-center">
                            <a href="#!" class="text-body">Forgot password?</a>
                        </div>

                        <div class="text-center text-lg-start mt-4 pt-2">
                            <asp:Button ID="btnLogin" runat="server" class="btn btn-primary btn-lg"
                                Style="padding-left: 2.5rem; padding-right: 2.5rem;" Text="Login" OnClick="btnLogin_Click" />
                            <p class="small fw-bold mt-2 pt-1 mb-0">
                                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                            </p>
                            <p class="small fw-bold mt-2 pt-1 mb-0">
                                Don't have an account? <a href="SignUp.aspx"
                                    class="info">Register</a>
                            </p>
                        </div>

                    </form>
                </div>
            </div>
        </div>
    </section>

</body>
</html>