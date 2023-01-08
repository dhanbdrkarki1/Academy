<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="Academy.base.Checkout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Checkout</title>
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/css/bootstrap.min.css" integrity="sha384-Gn5384xqQ1aoWXA+058RXPxPg6fy4IWvTNh0E263XmFcJlSAwiGgFAW/dAiS6JXm" crossorigin="anonymous" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container mt-3 mb-3">
                <div class="row ">
                    <div class="col-md-8 p-2 bg-primary text-white mx-auto rounded-top rounded-bottom">
                        <h1>Checkout</h1>
                    </div>
                </div>
                <div class="row p-2">
                    <div class="col-md-8  mx-auto">
                        <h2>Course Summary</h2>
                        <table class="table table-striped">
                            <tbody>
                                <tr>
                                    <th>Course Title</th>
                                    <td>
                                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>

                                        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>Category</th>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <th>Instructor</th>
                                    <td>
                                        <asp:Label ID="lblInstructor" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <th>Amount</th>
                                    <td>$<asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-8  mx-auto">
                        <button class="btn btn-secondary float-left" type="button">Go to Dashboard</button>
                        <asp:Button ID="btnCompleteOrder" class="btn btn-success float-right" runat="server" Text="Complete Order" OnClick="btnCompleteOrder_Click" />
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
</html>
