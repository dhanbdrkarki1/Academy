<%@ Page Title="Update Content" Language="C#" MasterPageFile="InstructorPage.Master" AutoEventWireup="true" CodeBehind="InstructorCourseContentUpdate.aspx.cs" Inherits="Academy.WebForm9" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js" integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js" integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl" crossorigin="anonymous"></script>
    <%--ajax--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.1/jquery.min.js"></script>
    <script>
        $('#myTab a').on('click', function (e) {
            e.preventDefault()
            $(this).tab('show')
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="ScriptManager1" EnablePageMethods="true"></asp:ScriptManager>

    <main id="main">

        <!-- ======= Breadcrumbs ======= -->
        <div class="breadcrumbs">
            <div class="container">
                <h2>Select content to Update 👇</h2>

            </div>
        </div>
        <!-- End Breadcrumbs -->


        <section id="features" class="features mt-5">
            <div class="container">
                <%--shows alert on update--%>
                <% if (ViewState["contMsg"] != null)
                    { %>
                <div class="alert alert-success" role="alert" id="myAlert">
                    <%=ViewState["contMsg"].ToString() %>
                </div>
                <% } %>


                <div class="row" data-aos="zoom-in" data-aos-delay="100">

                    <%
                        List<object[]> contentData = (List<object[]>)ViewState["ContentTitleList"];
                        int index = 1;
                        foreach (object[] data in contentData)
                        {
                            int contentId = (int)data[0];
                            string contentTitle = (string)data[2];

                    %>

                    <div class="col-lg-3 col-md-4">
                        <div class="icon-box">
                            <%--onclick="fillContentForm(<%= contentId %>)"--%>
                            <h3><a href="#" style="text-decoration: none;" onclick="fillContentForm(<%= contentId %>); return false;" data-toggle="modal" data-target="#btnUpdateContent"><%= index %>: <%= contentTitle %>    </a></h3>
                        </div>
                    </div>

                    <%
                            index++;
                        }


                    %>
                </div>
            </div>
        </section>

    </main>


    <%--content modal--%>
    <div class="modal fade" id="btnUpdateContent" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Course Content</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <asp:Label runat="server" for="txtInput" Text="Content Title:"></asp:Label>
                        <asp:TextBox ID="txtContentTitle" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Content Text:"></asp:Label>
                        <asp:TextBox ID="txtCContent" Rows="7" class="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Image:"></asp:Label>
                        <asp:FileUpload ID="ftImage" class="form-control" runat="server" />
                        <asp:Label ID="iPath" runat="server" Style="display: none;"></asp:Label>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Content Url:"></asp:Label>
                        <asp:TextBox ID="txtUrl" class="form-control" runat="server" TextMode="Url"></asp:TextBox>

                    </div>
                    <div class="form-group">
                        <asp:Label runat="server" Text="Attach Resource File:"></asp:Label>
                        <asp:FileUpload ID="ftFile" class="form-control" runat="server" />
                        <asp:Label ID="fPath" runat="server" Style="display: none;"></asp:Label>
                    </div>

                    <div class="modal-footer">
                        <asp:Button ID="btnClose" class="btn btn-secondary" data-dismiss="modal" runat="server" Text="Close" />
                        <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Update" OnClick="btnUpdateCourseContent_Click" />
                    </div>

                </div>

            </div>
        </div>
    </div>

    <script>

        setTimeout(function () {
            $('#myAlert').fadeOut('slow');
        }, 1000); // 2 seconds

        function fillContentForm(contentId) {
            var imgName = "";
            var fileName = "";
            $.ajax({
                type: "POST",
                url: "InstructorCourseContentUpdate.aspx/GetContentData",
                data: JSON.stringify({ contentId: contentId }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    console.log(data.d)
                    var cd = JSON.parse(data.d);
                    console.log(cd);
                    // cd as cookie
                    document.cookie = "contid=" + contentId;

                    // Update the modal content with the response data
                    $("#ContentPlaceHolder1_txtContentTitle").val(cd.title);
                    $("#ContentPlaceHolder1_txtCContent").val(cd.text);
                    $("#ContentPlaceHolder1_txtUrl").val(cd.url);

                    var imgPreview = document.getElementById("ContentPlaceHolder1_iPath");
                    var filePreview = document.getElementById("ContentPlaceHolder1_fPath");
                    imgName = cd.img;
                    fileName = cd.file;
                    if (imgName == "") {
                        imgPreview.style.display = "block";
                        imgPreview.innerHTML = "No image uploaded yet.";
                    } else {
                        imgPreview.style.display = "block";
                        imgPreview.innerHTML = imgName;
                    }

                    if (fileName == "") {
                        filePreview.style.display = "block";
                        filePreview.innerHTML = "No file uploaded yet.";
                    } else {
                        filePreview.style.display = "block";
                        filePreview.innerHTML = imgName;
                    }

                },
                error: function (xhr, status, error) {
                    console.error("AJAX request failed: " + error);
                }
            });




        }


        document.querySelector("#ContentPlaceHolder1_ftImage").addEventListener("change", function (event) {
            var file = event.target.files[0];
            preview.innerHTML = file;
            preview.style.display = "block";
        });

        //var fileInput = document.getElementById("#ContentPlaceHolder1_ftImage");
        //console.log(fileInput)
        //var preview = document.getElementById("preview");

        //fileInput.addEventListener("change", function () {
        //    var file = fileInput.files[0];
        //    var reader = new FileReader();

        //    reader.onload = function (event) {
        //        preview.innerHTML = event.target.result;
        //        preview.style.display = "block";
        //    };

        //    reader.readAsDataURL(file);
        //});
    </script>

    <%--//console.log(document.getElementById("hiddenContentId").value);--%>
    <%--            var cData = <%= new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(ViewState["ContentData"]) %>;
            

            for (var i = 0; i < cData.length; i++) {
                if (cData[i][0] == contentId) {
                    console.log(contentId);
                    // fill the form fields with the values from the content data
                    document.getElementById("txtContentTitle").value = cData[i][2];
                    document.getElementById("txtCContent").value = cData[i][3];
                    document.getElementById("txtUrl").value = cData[i][5];
                    break;
                }
            }--%>
        }




</asp:Content>
