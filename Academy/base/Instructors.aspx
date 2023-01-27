<%@ Page Title="" Language="C#" MasterPageFile="Base.Master" AutoEventWireup="true" CodeBehind="Instructors.aspx.cs" Inherits="Academy.WebForm4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" data-aos="fade-in">

        <!-- ======= Breadcrumbs ======= -->
        <div class="breadcrumbs">
            <div class="container">
                <h2><b>Trainers</b></h2>
                <p>
                    <h4>Trainers from around the globe teach students here in Academy. Get to know them better. </h4>
                </p>
            </div>
        </div>
        <!-- End Breadcrumbs -->

        <!-- ======= Instructors Section ======= -->
    <asp:ScriptManager runat="server" />

        <section id="trainers" class="trainers">
            <div class="container" data-aos="fade-up">

                <div class="row" data-aos="zoom-in" data-aos-delay="100">

                   

                    <%
                        List<object[]> instructorData = (List<object[]>)ViewState["InstructorData"];

                        foreach (object[] data in instructorData)
                        {
                            string accountId = data[0].ToString();

                            string fullName = data[1].ToString();
                            string email = data[2].ToString();
                            string profileImg = data[3].ToString();

                            if (profileImg == "")
                            {
                                profileImg = "~/Images/Profile/user.png";
                            }
                            profileImg = Page.ResolveUrl(profileImg);
                    %>

                    <div class="col-lg-4 col-md-6 d-flex align-items-stretch" onclick="showInstructorCourse(<%= accountId %>)">
                        <div class="member">
                            <img src="<%= profileImg %>" class="img-fluid" alt="<%= fullName %>">
                            <div class="member-content">
                                <h4><%=fullName %></h4>
                                <span><%= email %></span>
                                <div class="social">
                                    <a href="#"><i class="bi bi-twitter"></i></a>
                                    <a href="#"><i class="bi bi-facebook"></i></a>
                                    <a href="#"><i class="bi bi-instagram"></i></a>
                                    <a href="#"><i class="bi bi-linkedin"></i></a>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%

                        }
                    %>
                </div>

            </div>
        </section>
    </main>
    <!-- End Instructors Section -->

    <script>
        function showInstructorCourse(arg) {
            console.log("hello sir " + arg);

            __doPostBack('', arg);
        }
    </script>
</asp:Content>
