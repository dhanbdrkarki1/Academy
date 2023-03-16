<%@ Page Title="" Language="C#" MasterPageFile="~/student/StudentPage.Master" AutoEventWireup="true" CodeBehind="CoursesDetail.aspx.cs" Inherits="Academy.student.WebForm3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script>
        $('#myTab a').on('click', function (e) {
            e.preventDefault()
            $(this).tab('show')
        })
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

        <%
            List<object[]> contentData = (List<object[]>)Session["MyCoursesContentData"];
            if (contentData == null || contentData.Count == 0)
            {
        %>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div class="text-center">
                        <h3 class="mt-2">No content added yet.</h3>
                    </div>
                </div>
            </div>
        </div>

        <%
            }
            else
            { %>

        <div class="row">
            <div class="col-3">
                <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist" aria-orientation="vertical">

                    <%
                        int index = 0;
                        foreach (object[] data in contentData)
                        {
                            int contentId = (int)data[0];
                            string contentTitle = (string)data[2];

                    %>
                    <a class="nav-link <%= index == 0 ? "active" : "" %>" id="v-pills-<%= contentId %>-tab" data-toggle="pill" href="#v-pills-<%= contentId %>" role="tab" aria-controls="v-pills-<%= contentId %>" aria-selected="<%= index == 0 ? "true" : "false" %>" onclick="SetViewState('<%= contentId %>')">
                        <%= contentTitle %>
                    </a>
                    <%
                            index++;
                        }
                    %>
                </div>
    </div>
            <div class="col-9">
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
                        <!-- content titlte -->
                        <h3><%= contTitle %></h3>

                        <%--content text--%>
                        <p><%= contText %></p>


                        <%--image content--%>
                        <%
                            if (contImage != null)
                            {
                        %>
                        <img src="<%= contImage %>" />
                        <%
                            }
                            else
                            {
                        %>
                        <p>Image not found.</p>
                        <%
                            }
                        %>


                        <%--video url--%>
                        <%
                            if (contImage != null)
                            {
                        %>
                        <h5>Tutorial Video</h5>
                        <div>
                            <a href="<%= contUrl %>" />
                        </div>
                        <%
                            }
                        %>


                        <%--resource File--%>
                        <%
                            if (contFile != null)
                            {
                        %>
                        <a class="btn btn-primary" href="<%= contFile %>">Resouce File</a>

                        <%
                            }
                        %>
                    </div>
                    <%
                                index++;
                            }
                        }

                    %>
                </div>
            </div>
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




    <%--    <div class="container">
        <div class="row">
            <div class="col-3">
                <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                    <%
                        List<object[]> dataList = (List<object[]>)ViewState["ContentData"];
                        for (int i = 0; i < dataList.Count; i++)
                        {
                    %>
                    <a class="nav-link" id="v-pills-profile-tab" data-toggle="pill" href="#v-pills-profile" role="tab" aria-controls="v-pills-profile" aria-selected="false"><%= dataList[i][1] %></a>
                    <%
                        }
                    %>
                </div>
            </div>
            <div class="col-9">
                <div class="tab-content" id="v-pills-tabContent">
                    <div class="tab-pane fade show active" id="v-pills-home" role="tabpanel" aria-labelledby="v-pills-home-tab">1</div>
                    <div class="tab-pane fade" id="v-pills-profile" role="tabpanel" aria-labelledby="v-pills-profile-tab">2</div>
                    <div class="tab-pane fade" id="v-pills-messages" role="tabpanel" aria-labelledby="v-pills-messages-tab">3</div>
                    <div class="tab-pane fade" id="v-pills-settings" role="tabpanel" aria-labelledby="v-pills-settings-tab">4</div>
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>








