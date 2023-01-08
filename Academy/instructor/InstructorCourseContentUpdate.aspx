<%@ Page Title="Update Content" Language="C#" MasterPageFile="InstructorPage.Master" AutoEventWireup="true" CodeBehind="InstructorCourseContentUpdate.aspx.cs" Inherits="Academy.WebForm9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script>
        $('#myTab a').on('click', function (e) {
            e.preventDefault()
            $(this).tab('show')
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" data-aos="fade-in">

        <!-- ======= Breadcrumbs ======= -->
        <div class="breadcrumbs">
            <div class="container">
                <h2>Your Content</h2>
                <p>Add more courses to earn more. The more courses you add, the more rich you go So hurry up and add more content.</p>
            </div>
        </div>
        <!-- End Breadcrumbs -->
    </main>


    <div class="container">
        <div class="row mt-5">
            <div class="col-3">
                <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                    <%
                        List<object[]> contentData = (List<object[]>)ViewState["ContentData"];
                        int index = 0;
                        foreach (object[] data in contentData)
                        {
                            int contentId = (int)data[0];
                            string contentTitle = (string)data[2];

                    %>
                    <a class="nav-link <%= index == 0 ? "active" : "" %>" id="v-pills-<%= contentId %>-tab" data-toggle="pill" href="#v-pills-<%= contentId %>" role="tab" aria-controls="v-pills-<%= contentId %>" aria-selected="<%= index == 0 ? "true" : "false" %>" onclick="SetViewState('<%= contentId %>')">
                        <%= contentTitle %>
                    </a>
                    <%--<asp:LinkButton ID="linkButton1" runat="server" CssClass="nav-link" Text='<%= contentTitle %>' CommandArgument='<%= contentId %>' CommandName='<%= index %>'></asp:LinkButton>--%>

                    <%
                            index++;
                        }
                    %>


                </div>
            </div>


            <asp:ScriptManager runat="server" />
            <h3><%= ViewState["contentId"] %></h3>
            <div class="col-9">
                <div class="tab-content" id="v-pills-tabContent">
                    <div class="tab-pane fade show <%= ViewState["contentId"] == null ? "active" : "" %>" id="v-pills-<%= ViewState["contentId"] %>" role="tabpanel" aria-labelledby="v-pills-<%= ViewState["contentId"] %>-tab">
                        <!-- update form -->
                        <div class="form-group">
                            <asp:Label runat="server" for="txtInput" Text="Course Title"></asp:Label>
                            <asp:TextBox ID="txtCTitle" class="form-control" runat="server" Text=""></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" for="txtInput" Text="Content Title:"></asp:Label>
                            <asp:TextBox ID="txtContentTitle" class="form-control" Text="" runat="server"></asp:TextBox>
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

                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Update" />
                        <asp:Button ID="btnClose" class="btn btn-secondary" runat="server" Text="Clear" />


                    </div>
                </div>
            </div>

            <asp:Label ID="testId" runat="server" Text="Label"></asp:Label>





            <%--            <div class="col-9">
                <div class="tab-content" id="v-pills-tabContent">
                    <%
                        index = 0;
                        foreach (object[] data in contentData)
                        {
                            int contentId = (int)data[0];
                            int cid = (int)data[1];
                            string contTitle = (string)data[2];
                            string contText = (string)data[3];
                            string contImage = (string)data[4];
                            string contFile = (string)data[5];
                            string contUrl = (string)data[6];
                    %>
                    <div class="tab-pane fade show <%= (string)ViewState["viewstate"] == contentId.ToString() ? "active" : "" %>" id="v-pills-<%= contentId %>" role="tabpanel" aria-labelledby="v-pills-<%= contentId %>-tab">
                        <!-- update form -->
                        <div class="form-group">
                            <asp:Label runat="server" for="txtInput" Text="Course Title"></asp:Label>
                            <asp:TextBox ID="txtCTitle" class="form-control" runat="server" Text=""></asp:TextBox>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" for="txtInput" Text="Content Title:"></asp:Label>
                            <asp:TextBox ID="txtContentTitle" class="form-control" runat="server" Text='<%# Eval("contTitle") %>'></asp:TextBox>
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

                        <asp:Button ID="btnClose" class="btn btn-secondary" runat="server" Text="Clear" />
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Update" />


                    </div>
                    <%
                            index++;
                        }
                    %>
                </div>
            </div>--%>
        </div>
    </div>

    <%--    <%
        List<object[]> dataList = (List<object[]>)ViewState["ContentData"];
        for (int i = 0; i < dataList.Count; i++)
        {
    %>
    <div>
        ID: <%= dataList[i][0] %>
            Cont Title: <%= dataList[i][1] %>
    </div>
    <%
        }
    %>--%>

    <script>
        function SetViewState(arg) {
            __doPostBack('', arg);
        }
    </script>


</asp:Content>
