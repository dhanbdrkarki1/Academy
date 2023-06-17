<%@ Page Title="Available Courses" Language="C#" MasterPageFile="StudentPage.Master" AutoEventWireup="true" CodeBehind="AvailableCourses.aspx.cs" Inherits="Academy.student.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://fonts.googleapis.com/css?family=Roboto|Varela+Round">
    <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons">
    <link rel="stylesheet" href="../assets/css/Custom.css" />

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <script src="../assets/js/Custom.js"></script>
    <style>
        .course-effect {
            transition: .3s;
        }

            .course-effect:hover {
                box-shadow: rgb(0 0 0 / 24%) 0px 3px 8px;
                transform: scale(1.01,1.01);
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" data-aos="fade-in">

        <!-- ======= Breadcrumbs ======= -->
        <div class="breadcrumbs">
            <div class="container">
                <h2><b>Courses </b></h2>
                <p>
                    <h4>Learn the in-demand professional skills from the experts. Explore courses and get enrolled today.</h4>
                </p>
            </div>
        </div>
        <!-- End Breadcrumbs -->


        <!-- ======= Courses Section ======= -->
        <section id="courses" class="courses">
            <div class="container" data-aos="fade-up">
                <div class="row mb-4" data-aos="zoom-in" data-aos-delay="100">

                    <%
                        List<object[]> contentData = (List<object[]>)ViewState["AvailableCoursesData"];
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
    <!-- End #main -->

    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>


    <!-- Modal -->
    <asp:ScriptManager runat="server" />

    <div class="modal fade" id="overviewModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
        <div class="modal-dialog mw-100 w-50" role="document">
            <div class="modal-content">

                <div class="modal-header mb-2 bg-info text-white">
                    <h4 class="modal-title" id="exampleModalLongTitle">

                        <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label>
                    </h4>
                    <p class="btn btn-sm p-3 mb-2 bg-success text-white">
                        Price: $
                        <asp:Label ID="lblPrice" runat="server" Text=""></asp:Label>
                    </p>

                    <button type="button" class="close" data-dismiss="modal" aria-label="Close" onclick="hideModal()">
                        <span aria-hidden="true">&times;</span>
                    </button>

                </div>
                <div class="modal-body">
                    <asp:Button ID="btnAvailEnrollCourse" class="btn btn-primary btn-lg mb-2 " runat="server" Text="Enroll Now" OnClick="btnAvailEnrollCourse_Click" />

                    <h4 class="mb-2">About this Course</h4>
                    <p style="font-size: 1rem;" class="text-dark">

                        <asp:Label ID="lblDescription" runat="server" Text=""></asp:Label>

                    </p>
                </div>
            </div>
        </div>
    </div>

    <script>
        function showCourseOverview(arg) {
            __doPostBack('', arg);
        }
        function hideModal() {

            $('#overviewModal').modal('hide')
        }
    </script>
</asp:Content>
