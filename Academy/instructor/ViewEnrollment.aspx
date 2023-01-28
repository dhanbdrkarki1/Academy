<%@ Page Title="View Enrollment" Language="C#" MasterPageFile="InstructorPage.Master" AutoEventWireup="true" CodeBehind="ViewEnrollment.aspx.cs" Inherits="Academy.WebForm7" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main id="main" data-aos="fade-in" class="mt-5">

        <!-- ======= Courses Section ======= -->
        <section id="courses" class="courses">
            <div class="section-title text-center ">
                <p>Courses Enrolled By Students</p>
            </div>
            <div class="container" data-aos="fade-up">
                <div class="row" data-aos="zoom-in" data-aos-delay="100">


                    <table class="table align-middle mb-0 bg-white">
                        <thead class="bg-light">
                            <tr>
                                <th>Name</th>
                                <th>Course Title</th>
                                <th>Category</th>
                                <th>Amount</th>
                                <th>Enrolled Date</th>
                            </tr>
                        </thead>
                        <tbody>


                            <%
                                List<object[]> enrollmentData = (List<object[]>)ViewState["EnrollmentData"];

                                foreach (object[] data in enrollmentData)
                                {
                                    string fullName = data[0].ToString();
                                    string email = data[1].ToString();
                                    string profileImg = data[2].ToString();
                                    if (profileImg == "")
                                    {
                                        profileImg = "~/Images/Profile/user.png";
                                    }
                                    profileImg = Page.ResolveUrl(profileImg);
                                    string title = data[3].ToString();
                                    string category = data[4].ToString();
                                    string totalAmt = data[5].ToString();
                                    string orgEnrollDate = data[6].ToString();
                                    DateTime date = DateTime.Parse(orgEnrollDate);
                                    string enrollDate = date.ToString("dd-MMM-yy");

                            %>

                            <tr>
                                <td>
                                    <div class="d-flex align-items-center">
                                        <img
                                            src="<%= profileImg %>"
                                            alt=""
                                            style="width: 45px; height: 45px"
                                            class="rounded-circle" />
                                        <div class="ms-3">
                                            <p class="fw-bold mb-1"><%= fullName %></p>
                                            <p class="text-muted mb-0"><%= email %></p>
                                        </div>
                                    </div>
                                </td>
                                <td>
                                    <%= title %>
                                </td>
                                <td>
                                    <%= category %>
                                </td>
                                <td>$<%= totalAmt %></td>
                                <td>
                                    <%= enrollDate %>
                                </td>
                            </tr>

                            <%

                                }
                            %>
                        </tbody>
                    </table>

                </div>

            </div>
        </section>
        <!-- End Courses Section -->
    </main>
</asp:Content>
