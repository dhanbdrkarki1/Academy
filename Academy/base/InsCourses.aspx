<%@ Page Title="" Language="C#" MasterPageFile="Base.Master" AutoEventWireup="true" CodeBehind="InsCourses.aspx.cs" Inherits="Academy.InsCourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main">

        <!-- ======= Breadcrumbs ======= -->
        <div class="breadcrumbs" data-aos="fade-in">
            <div class="container">
                <h2><b>Instructor Courses </b></h2>
                <p>
                    <h4>Know about the courses taught by the instructor. </h4>
                </p>
            </div>
        </div>
        <!-- End Breadcrumbs -->



        <!-- ======= Courses Section ======= -->
        <section id="courses" class="courses">
            <div class="container" data-aos="fade-up">
                <div class="row mb-4" data-aos="zoom-in" data-aos-delay="100">

                    <%
                        List<object[]> contentData = (List<object[]>)ViewState["InstructorCourses"];
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

                            string rate = data[4].ToString();
                            string imgpath = data[5].ToString();
                            if (imgpath == "")
                            {
                                imgpath = "~/Images/Content/no-image-icon.png";
                            }
                            imgpath = Page.ResolveUrl(imgpath);
                            string instructor = data[6].ToString();
                    %>

                    <% if (ViewState["msg"].ToString() != string.Empty)
                        {
                    %>

                    <h3>No Courses Available!</h3>

                    <% 

                        } %>

                    <div class="col-lg-4 col-md-6 d-flex align-items-stretch" data-toggle="modal" data-target="#overviewModal" onclick="showCourseOverview(<%= courseid %>)">
                        <div class="course-item course-effect">
                            <img src='<%= imgpath %>' class="img-fluid" alt="...">
                            <div class="course-content">
                                <div class="d-flex justify-content-between align-items-center mb-3">
                                    <h4><%= category %></h4>
                                    <p class="price">$<%= rate %></p>
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
                </div>
            </div>
        </section>
        <!-- End Courses Section -->
    </main>
</asp:Content>
