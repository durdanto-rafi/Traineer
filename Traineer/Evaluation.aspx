<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Evaluation.aspx.cs" Inherits="Traineer.Evaluation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        javascript: window.history.forward();
        javascript: window.history.forward(-1);
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!--BEGIN TITLE & BREADCRUMB PAGE-->
    <div id="title-breadcrumb-option-demo" class="page-title-breadcrumb">
        <div class="page-header pull-left">
            <div class="page-title">
                <asp:Label ID="lblPageTitle" runat="server"></asp:Label>
            </div>
        </div>
        <ol class="breadcrumb page-breadcrumb pull-right">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Default.aspx">Home</a>&nbsp;&nbsp;</li>
        </ol>
        <div class="clearfix">
        </div>
    </div>

    <!--END TITLE & BREADCRUMB PAGE-->

    <!--BEGIN CONTENT-->
    <div class="page-content page-content-custom">
        <div class="col-lg-12">
            <div class="row">
                <div class="col-lg-2 col-lg-offset-2">
                    <asp:Button Text="New" class="btn btn-primary" runat="server" ID="btnNew" OnClick="btnNew_Click" />
                    <asp:Button Text="Save" class="btn btn-success" runat="server" ID="btnSave" OnClick="btnSave_OnClick" />
                    <asp:Button Text="Cancel" class="btn btn-danger" runat="server" ID="btnCancel" />
                </div>
                <div runat="server" class="col-lg-4 col-xs-3 col-xs-offset-0">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                        <ContentTemplate>
                            <asp:Label Text="" ID="lblMessage" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="spacer-20"></div>
            <div class="panel-body pan ">
                <div class="form-horizontal">
                    <div class="form-body pal pal-custom">
                        <div class="row">
                            <div class="col-lg-9 col-xs-3">
                                <div class="form-group form-group-custom">
                                    <label class="col-lg-1 col-lg-offset-4 control-label small">
                                        Traineer</label>
                                    <div class="col-lg-7">
                                        <div class="input-icon right">
                                            <asp:DropDownList runat="server" class="form-control input-sm" ID="ddlTraineer" AutoPostBack="True" OnSelectedIndexChanged="ddlTraineer_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="spacer-5"></div>
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2">
                    <div class="panel panel-violet">
                        <div class="panel-heading panel-heading-custom ">Question Set</div>
                        <div class="panel-body panel-body-custom">
                            <div class="table-responsive">
                                <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                    <ContentTemplate>
                                        <asp:DataGrid ID="grdData" class="table table-custom table-hover table-striped" runat="server" AutoGenerateColumns="false"
                                            GridLines="None" Font-Size="Small" OnItemDataBound="grdData_OnItemDataBound">
                                            <Columns>
                                                <asp:BoundColumn HeaderText="#">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Question" DataField="QuestionName">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlMarks" SelectedValue='<%# Bind("Marks") %>'>
                                                            <asp:ListItem Text="-- Select --" Value="0" />
                                                            <asp:ListItem Text="Good" Value="3" />
                                                            <asp:ListItem Text="Average" Value="2" />
                                                            <asp:ListItem Text="Poor" Value="1" />
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn HeaderText="ID" DataField="ID">
                                                    <ItemStyle HorizontalAlign="Center" CssClass="hidden" />
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" CssClass="hidden" />
                                                </asp:BoundColumn>
                                            </Columns>
                                        </asp:DataGrid>
                                    </ContentTemplate>
                                    <Triggers>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:Label Text="" ID="lblDeleteIDList" CssClass="hidden" runat="server" />
    <!--END CONTENT-->

</asp:Content>
