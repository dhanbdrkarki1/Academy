<%@ Page Title="Home" Language="C#" MasterPageFile="Base.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Academy.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- ======= Hero Section ======= -->
    <section id="hero" class="d-flex justify-content-center align-items-center">
        <div class="container position-relative" data-aos="zoom-in" data-aos-delay="100">
            <h1>Learning Today,<br>
                Leading Tomorrow</h1>
            <h2>Global Netwroking Platform To Learn and Grow
            </h2>
            <a href="Courses.aspx" class="btn-get-started">Get Started</a>
        </div>
    </section>
    <!-- End Hero -->

    <main id="main">

        <!-- ======= About Section ======= -->
        <section id="about" class="about">
            <div class="container" data-aos="fade-up">

                <div class="row">
                    <div class="col-lg-6 order-1 order-lg-2" data-aos="fade-left" data-aos-delay="100">
                        <img src="assets/img/about.jpg" class="img-fluid" alt="">
                    </div>
                    <div class="col-lg-6 pt-4 pt-lg-0 order-2 order-lg-1 content">

                        <p class="fst-italic">
                            <h1 style="color: mediumaquamarine; font-weight: bold">LOOKING FOR YOUR DESIRED COURSE?</h1>
                        </p>
                        <ul>
                            <h3>
                                <li><i class="bi bi-check-circle"></i>Learn from Professionals.</li>
                                <li><i class="bi bi-check-circle"></i>Competitive with people all around the globe.</li>
                                <li><i class="bi bi-check-circle"></i>Get preofessional certificate of the completed courses.</li>
                            </h3>
                        </ul>
                        <p>
                            <h4>Start learning the courses according to your time flexibility and your pace. Start today, enroll and obtain all the creative skills you to achieve your goals in terms of personal and career path. Connect to the global community. Expland networks.</h4>
                        </p>

                    </div>
                </div>

            </div>
        </section>
        <!-- End About Section -->

        <!-- ======= Counts Section ======= -->
        <section id="counts" class="counts section-bg">
            <div class="container">

                <div class="row counters">

                    <div class="col-lg-3 col-6 text-center">
                        <span data-purecounter-start="0" data-purecounter-end="<%= ViewState["ins_count"].ToString() %>" data-purecounter-duration="1" class="purecounter"></span>
                        <p>Instructors</p>
                    </div>

                    <div class="col-lg-3 col-6 text-center">
                        <span data-purecounter-start="0" data-purecounter-end="<%= ViewState["std_count"].ToString() %>" data-purecounter-duration="1" class="purecounter"></span>
                        <p>Students</p>
                    </div>

                    <div class="col-lg-3 col-6 text-center">
                        <span data-purecounter-start="0" data-purecounter-end="<%= ViewState["CoursesCount"].ToString() %>" data-purecounter-duration="1" class="purecounter"></span>
                        <p>Courses</p>
                    </div>

                </div>

            </div>
        </section>
        <!-- End Counts Section -->

        <!-- ======= Why Us Section ======= -->
        <section id="why-us" class="why-us">
            <div class="container" data-aos="fade-up">

                <div class="row">
                    <div class="col-lg-4 d-flex align-items-stretch">
                        <div class="content">
                            <h3>Why Choose Academy?</h3>
                            <p>
                                Academy believes in building networks to every corner of the globe inspiring everyone to learn the subject of their interest. Explore the topic and develop understanding at your convinent pace.
                            </p>
                            <div class="text-center">
                                <a href="about.html" class="more-btn">Learn More <i class="bx bx-chevron-right"></i></a>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-8 d-flex align-items-stretch" data-aos="zoom-in" data-aos-delay="100">
                        <div class="icon-boxes d-flex flex-column justify-content-center">
                            <div class="row">
                                <div class="col-xl-4 d-flex align-items-stretch">
                                    <div class="icon-box mt-4 mt-xl-0">
                                        <i class="bx bx-receipt"></i>
                                        <h4>Learn from the best</h4>
                                        <p>Get ideas from mentors expert in that field. Ask your queries and learn new skills.</p>
                                    </div>
                                </div>
                                <div class="col-xl-4 d-flex align-items-stretch">
                                    <div class="icon-box mt-4 mt-xl-0">
                                        <i class="bx bx-cube-alt"></i>
                                        <h4>Come out of your comfort zone</h4>
                                        <p>Share your ideas and skills with people from all over the world. Build Network.</p>
                                    </div>
                                </div>
                                <div class="col-xl-4 d-flex align-items-stretch">
                                    <div class="icon-box mt-4 mt-xl-0">
                                        <i class="bx bx-images"></i>
                                        <h4>See yourself in bigger picture</h4>
                                        <p>Do not wait for the perfect time. Start today and see yourself in the position of your dreams.</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- End .content-->
                    </div>
                </div>

            </div>
        </section>
        <!-- End Why Us Section -->

        <!-- ======= Features Section ======= -->
        <section id="features" class="features">
            <div class="container" data-aos="fade-up">

                <div class="row" data-aos="zoom-in" data-aos-delay="100">
                    <div class="col-lg-3 col-md-4">
                        <div class="icon-box">
                            <i class="ri-store-line" style="color: #ffbb2c;"></i>
                            <h3><a href="">Marketing and Business</a></h3>
                        </div>
                    </div>


                    <div class="col-lg-3 col-md-4 mt-4 mt-md-0">
                        <div class="icon-box">
                            <i class="ri-bar-chart-box-line" style="color: #5578ff;"></i>
                            <h3><a href="">Data Analysis</a></h3>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4 mt-4 mt-md-0">
                        <div class="icon-box">
                            <i class="ri-calendar-todo-line" style="color: #e80368;"></i>
                            <h3><a href="">Project Management </a></h3>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4 mt-4 mt-lg-0">
                        <div class="icon-box">
                            <i class="ri-paint-brush-line" style="color: #e361ff;"></i>
                            <h3><a href="">UI/UX Design</a></h3>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4 mt-4">
                        <div class="icon-box">
                            <i class="ri-database-2-line" style="color: #47aeff;"></i>
                            <h3><a href="">Database Management</a></h3>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4 mt-4">
                        <div class="icon-box">
                            <i class="ri-gradienter-line" style="color: #ffa76e;"></i>
                            <h3><a href="">Digital Illustration</a></h3>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4 mt-4">
                        <div class="icon-box">
                            <i class="ri-file-list-3-line" style="color: #11dbcf;"></i>
                            <h3><a href="">Creative Writing</a></h3>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4 mt-4">
                        <div class="icon-box">
                            <i class="ri-price-tag-2-line" style="color: #4233ff;"></i>
                            <h3><a href="">FreeLance and Enterpreneurship </a></h3>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4 mt-4">
                        <div class="icon-box">
                            <i class="ri-anchor-line" style="color: #b2904f;"></i>
                            <h3><a href="">Animation</a></h3>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4 mt-4">
                        <div class="icon-box">
                            <i class="ri-disc-line" style="color: #b20969;"></i>
                            <h3><a href="">Music and Recordings</a></h3>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4 mt-4">
                        <div class="icon-box">
                            <i class="ri-base-station-line" style="color: #ff5828;"></i>
                            <h3><a href="">Network Security</a></h3>
                        </div>
                    </div>
                    <div class="col-lg-3 col-md-4 mt-4">
                        <div class="icon-box">
                            <i class="ri-fingerprint-line" style="color: #29cc61;"></i>
                            <h3><a href="">Robotics</a></h3>
                        </div>
                    </div>
                </div>

            </div>
        </section>
        <!-- End Features Section -->



    </main>
    <!-- End #main -->
</asp:Content>
