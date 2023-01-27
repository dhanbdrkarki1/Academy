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
    <asp:ScriptManager runat="server" ID="ScriptManager1" EnablePageMethods="true"></asp:ScriptManager>

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
            <%--            <div class="col-3">
                <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                    <%
                        List<object[]> contentData = (List<object[]>)ViewState["ContentData"];
                        int index = 0;
                        foreach (object[] data in contentData)
                        {
                            int contentId = (int)data[0];
                            string contentTitle = (string)data[2];

                    %>
                    <asp:HiddenField ID="hiddenContentId" runat="server" ClientIDMode="Static" />
                    <a class="nav-link <%= index == 0 ? "active" : "" %>" id="v-pills-<%= contentId %>-tab" data-toggle="pill" href="#v-pills-<%= contentId %>" role="tab" aria-controls="v-pills-<%= contentId %>" aria-selected="<%= index == 0 ? "true" : "false" %>" onclick="SetViewState('<%= contentId %>');return false;">
                        <%= contentTitle %>
                    </a>


                    <%
                            index++;
                        }
                    %>
                </div>
            </div>--%>

            <h3>Hlllo world</h3>
            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>

                    <div class="col-3">
                        <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                            <asp:Repeater ID="repeaterNavLinks" runat="server" OnItemCommand="repeaterNavLinks_ItemCommand">
                                <ItemTemplate>

                                    <asp:LinkButton ID="lnkNavLink" runat="server" CommandName="Update" CommandArgument='<%# Eval("[0]") %>' Text='<%# Eval("[2]") %>' CssClass='nav-link' />
                                </ItemTemplate>
                            </asp:Repeater>

                        </div>
                    </div>



                    <div class="col-9">
                        <div class="tab-content" id="v-pills-tabContent">
                            <!-- update form -->
                            <div class="form-group">
                                <asp:Label runat="server" for="txtInput" Text="Content Title:"></asp:Label>
                                <asp:TextBox ID="TextBox1" class="form-control" runat="server" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" Text="Content Text:"></asp:Label>
                                <asp:TextBox ID="TextBox2" Rows="7" class="form-control" runat="server" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" Text="Image:"></asp:Label>
                                <asp:FileUpload ID="FileUpload1" class="form-control" runat="server" ClientIDMode="Static" />
                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" Text="Content Url:"></asp:Label>
                                <asp:TextBox ID="TextBox3" class="form-control" runat="server" TextMode="Url" ClientIDMode="Static"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <asp:Label runat="server" Text="Attach Resource File:"></asp:Label>
                                <asp:FileUpload ID="FileUpload2" class="form-control" runat="server" ClientIDMode="Static" />
                            </div>

                            <asp:Button ID="Button1" class="btn btn-secondary" runat="server" Text="Clear" />
                            <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="Update" />
                        </div>
                    </div>
                </ContentTemplate>

                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="lnkNavLink" EventName="Command" />

                </Triggers>
            </asp:UpdatePanel>




            <%--            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                <ContentTemplate>
                    <div class="col-9">
                        <div class="tab-content" id="v-pills-tabContent">
                            <%
                                index = 0;
                                foreach (object[] data in contentData)
                                {
                                    int contentId = (int)data[0];
                                    string contTitle = (string)data[2];
                            %>
                            <div class="tab-pane fade show <%= (string)ViewState["viewstate"] == contentId.ToString() || (string)ViewState["hiddenContentId"] == contentId.ToString() ? "active" : "" %>" id="v-pills-<%= contentId %>" role="tabpanel" aria-labelledby="v-pills-<%= contentId %>-tab">

                                <!-- update form -->
                                <div class="form-group">
                                    <asp:Label runat="server" for="txtInput" Text="Content Title:"></asp:Label>
                                    <asp:TextBox ID="txtContentTitle" class="form-control" runat="server" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Content Text:"></asp:Label>
                                    <asp:TextBox ID="txtCContent" Rows="7" class="form-control" runat="server" TextMode="MultiLine" ClientIDMode="Static"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Image:"></asp:Label>
                                    <asp:FileUpload ID="ftImage" class="form-control" runat="server" ClientIDMode="Static" />
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Content Url:"></asp:Label>
                                    <asp:TextBox ID="txtUrl" class="form-control" runat="server" TextMode="Url" ClientIDMode="Static"></asp:TextBox>

                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" Text="Attach Resource File:"></asp:Label>
                                    <asp:FileUpload ID="ftFile" class="form-control" runat="server" ClientIDMode="Static" />
                                </div>

                                <asp:Button ID="btnClose" class="btn btn-secondary" runat="server" Text="Clear" />
                                <asp:Button ID="btnSave" class="btn btn-primary" runat="server" Text="Update" />


                            </div>

                            <%
                                    index++;
                                }
                            %>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="v-pills-tab" EventName="click" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </div>
    </div>

    <script>

        function SetViewState(contentId) {
            console.log(contentId);
            var hiddenContentId = document.getElementById("hiddenContentId");
            hiddenContentId.setAttribute("value", contentId);

            //__doPostBack('hiddenContentId', contentId);

            //console.log(document.getElementById("hiddenContentId").value);
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
    </script>



</asp:Content>
