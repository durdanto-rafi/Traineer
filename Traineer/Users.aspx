<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Traineer.Users" %>

<%--<%@ Register TagPrefix="Ajax" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit, Version=3.0.20820.16598, Culture=neutral, PublicKeyToken=28f01b0e84b6d53e" %>--%>
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
                <div class="col-lg-1 col-lg-offset-1">
                    <asp:Button Text="Add New" class="btn btn-primary" runat="server" ID="btnAddNew" OnClick="btnAddNew_Click" />
                </div>
            </div>
            <div class="spacer-5"></div>
            <div class="row">
                <div class="col-lg-10 col-lg-offset-1">
                    <div class="panel panel-violet">
                    <div class="panel-heading panel-heading-custom ">User Record</div>
                    <div class="panel-body panel-body-custom">
                        <div class="table-responsive">
                            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <asp:DataGrid ID="grdData" class="table table-custom table-hover table-striped" runat="server" AutoGenerateColumns="false"
                                        GridLines="None" Font-Size="Small" OnItemCommand="grdData_ItemCommand" OnItemDataBound="grdData_ItemDataBound">
                                        <Columns>
                                            <asp:BoundColumn HeaderText="#">
                                                <ItemStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="ID" DataField="ID">
                                                <ItemStyle HorizontalAlign="Center" CssClass="hidden"/>
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" CssClass="hidden"/>
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="Name" DataField="Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn HeaderText="Username" DataField="username">
                                                <ItemStyle HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" Style="padding: 0px 16px 0 0;" runat="server" CausesValidation="false"
                                                        ImageUrl="~/images/icons/edit.png" CommandName="Edit" value="Edit" ToolTip="Edit" />
                                                   <asp:ImageButton ID="btndel" Style="padding: 0px 16px 0 0;" runat="server" CausesValidation="false"
                                                        CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record?');"
                                                        ImageUrl="~/images/icons/delete.png" value="Delete" ToolTip="Delete" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger EventName="Click" ControlID="btnPopUpSave" />
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

    <!--BEGIN MODAL-->
    <div class="modal fade" id="myModal" data-backdrop="static" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title">
                                <asp:Label ID="lblModalTitle" runat="server" ></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="panel-body pan ">
                                <div class="form-horizontal">
                                    <div class="form-body pal pal-custom">
                                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-lg-12 col-xs-12">
                                                        <div class="form-group form-group-custom">
                                                            <label for="inputName" class="col-lg-2 col-xs-1 control-label small">Name</label>
                                                            <div class="col-lg-9 col-xs-11">
                                                                <div class="input-icon right">
                                                                    <asp:TextBox runat="server" class="form-control input-sm" placeholder="Name" ID="txtName" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="validate" runat="server"
                                                                        ControlToValidate="txtName" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
                                                                        ErrorMessage="Name Required" Style="font-size: xx-small">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="ddlItem" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-lg-12 col-xs-12">
                                                        <div class="form-group form-group-custom">
                                                            <label for="inputName" class="col-lg-2 col-xs-1 control-label small">Username</label>
                                                            <div class="col-lg-9 col-xs-11">
                                                                <div class="input-icon right">
                                                                    <asp:TextBox runat="server" class="form-control input-sm" placeholder="Username" ID="txtUsername" />
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ValidationGroup="validate" runat="server"
                                                                        ControlToValidate="txtUsername" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
                                                                        ErrorMessage="Username Required" Style="font-size: xx-small">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-xs-12">
                                                        <div class="form-group form-group-custom">
                                                            <label for="inputName" class="col-lg-2 col-xs-1 control-label small">Password</label>
                                                            <div class="col-lg-9 col-xs-11">
                                                                <div class="input-icon right">
                                                                    <asp:TextBox runat="server" class="form-control input-sm" TextMode="Password"
                                                                        placeholder="password" ID="txtPassword" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ValidationGroup="validate" runat="server"
                                                                        ControlToValidate="txtPassword" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
                                                                        ErrorMessage="Password Required" Style="font-size: xx-small">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12 col-xs-12">
                                                        <div class="form-group form-group-custom">
                                                            <label for="inputName" class="col-lg-2 col-xs-1 control-label small">Confirm Password</label>
                                                            <div class="col-lg-9 col-xs-11">
                                                                <div class="input-icon right">
                                                                    <asp:TextBox runat="server" class="form-control input-sm" TextMode="Password"
                                                                        placeholder="Confirm Password" ID="txtConfirmPassword" />
                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ValidationGroup="validate" runat="server"
                                                                        ControlToValidate="txtConfirmPassword" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
                                                                        ErrorMessage="Confirm Password Required" Style="font-size: xx-small">
                                                                    </asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <%--<asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="ddlItem" />--%>
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="col-lg-offset-1 col-lg-7 ">
                                <asp:Label Text="text" ID="lblPopupMessage" CssClass="text-center" runat="server" />
                            </div>
                            <div class="col-lg-4">
                                <asp:Button Text="Save" class="btn btn-success" ID="btnPopUpSave" OnClick="btnPopUpSave_Click" runat="server" ValidationGroup="validate" UseSubmitBehavior="False" />
                                <button class="btn btn-primary" data-dismiss="modal" aria-hidden="true">Close</button>
                                <asp:Label runat="server" ID="lblID" CssClass="hidden"></asp:Label>
                                <asp:TextBox runat="server" class="form-control" placeholder="" ID="txtID" Visible="false" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
                <Triggers>
                    <%--<asp:AsyncPostBackTrigger EventName="Click" ControlID="btnAddRow" />--%>
                    <asp:AsyncPostBackTrigger EventName="ItemCommand" ControlID="grdData" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--END MODAL-->
</asp:Content>
