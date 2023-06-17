<%@ Page Title="My Courses" Language="C#" MasterPageFile="StudentPage.Master" AutoEventWireup="true" CodeBehind="MyCourses.aspx.cs" Inherits="Academy.WebForm10" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" />
    <h3>Student 00</h3>
    <main id="main" data-aos="fade-in">

        <!-- ======= Breadcrumbs ======= -->
        <div class="breadcrumbs">
            <div class="container">
                <h2>My Courses</h2>
                <p>Learn the in-demand professional skills from the experts. Explore courses and get enrolled today.</p>
            </div>
        </div>
        <!-- End Breadcrumbs -->

        <!-- ======= Courses Section ======= -->
        <section id="courses" class="courses">
            <div class="container" data-aos="fade-up">

                <div class="row" data-aos="zoom-in" data-aos-delay="100">
                    <%

                        List<object[]> contentData = (List<object[]>)ViewState["MyCoursesData"];
                        foreach (object[] data in contentData)
                        {
                            string courseid = data[0].ToString();
                            string title = data[1].ToString();
                            string category = data[2].ToString();
                            string overview = data[3].ToString();
                            string[] words = overview.Split(' ');
                            string limitText = string.Join(" ", words.Take(25));

                            if (words.Length > 25)
                            {
                                limitText += "...";
                            }

                            //string rate = data[4].ToString();
                            string imgpath = data[5].ToString();
                            if (imgpath == "")
                            {
                                imgpath = "~/Images/Content/no-image-icon.png";
                            }
                            imgpath = Page.ResolveUrl(imgpath);
                            string instructor = data[6].ToString();
                    %>

                    <div class="col-lg-4 col-md-6 d-flex align-items-stretch"  onclick="showCourseDetail(<%= courseid %>)">
                        <div class="course-item course-effect">
                            <img src='<%= imgpath %>' class="img-fluid" alt="...">
                            <div class="course-content">
                                <div class="d-flex justify-content-between align-items-center mb-3">
                                    <h4><%= category %></h4>
                                </div>
                                <h3><a href="#"><%= title %></a></h3>
                                <p><%= limitText %></p>
                                <div class="trainer d-flex justify-content-between align-items-center">
                                    <div class="trainer-profile d-flex align-items-center">
                                        <img src="assets/img/trainers/trainer-1.jpg" class="img-fluid" alt="">
                                        <span><%= instructor %></span>
                                    </div>
                                    <div class="trainer-rank d-flex align-items-center">
                                        <i class="bx bx-user"></i>&nbsp;50
                    &nbsp;&nbsp;
                    <i class="bx bx-heart"></i>&nbsp;65
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- End Course Item-->

                    <%

                        }
                    %>
    </main>
    <script>
        function showCourseDetail(arg) {
            console.log("clicked me ahhh");
            __doPostBack('', arg);
        }
    </script>
</asp:Content>
