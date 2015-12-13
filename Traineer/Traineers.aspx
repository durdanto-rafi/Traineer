<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Traineers.aspx.cs" Inherits="Traineer.Traineers" %>


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
                <div class="col-lg-1 col-lg-offset-1">
                    <asp:Button Text="Add New" class="btn btn-primary" runat="server" ID="btnAddNew" OnClick="btnAddNew_Click" />
                </div>
                <div runat="server" class="col-lg-4 col-xs-3 col-xs-offset-0">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                            <asp:Label Text="" ID="lblMessage" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                        </Triggers>
                    </asp:UpdatePanel>
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
                                                    <ItemStyle HorizontalAlign="Center" CssClass="hidden" />
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" CssClass="hidden" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Traineer Name" DataField="TraineerName">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn HeaderText="Active" DataField="Active">
                                                    <ItemStyle HorizontalAlign="Center" />
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
                                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-lg-12 col-xs-12">
                                                        <div class="form-group form-group-custom">
                                                            <label class="col-lg-2 col-xs-1 control-label small">Traineer Name</label>
                                                            <div class="col-lg-9 col-xs-11">
                                                                <div class="input-icon right">
                                                                    <asp:TextBox runat="server" class="form-control input-sm" Style="resize: none" TextMode="MultiLine" placeholder="Traineer Name" ID="txtTraineerName" />
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ValidationGroup="validate" runat="server"
                                                                        ControlToValidate="txtTraineerName" Display="Dynamic" SetFocusOnError="true" ForeColor="Red"
                                                                        ErrorMessage="Traineer Name Required" Style="font-size: xx-small">
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
                                                            <label class="col-lg-2 col-xs-1 control-label small">Active</label>
                                                            <div class="col-lg-9 col-xs-11">
                                                                <div class="input-icon right">
                                                                    <asp:CheckBox runat="server" ID="chkActive" />
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
                                <asp:Label ID="lblPopupMessage" CssClass="text-center" runat="server" />
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
                    <asp:AsyncPostBackTrigger EventName="ItemCommand" ControlID="grdData" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <!--END MODAL-->
</asp:Content>
