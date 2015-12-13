<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Traineer._Default" %>

<%@ Import Namespace="System.Data" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        javascript: window.history.forward();
        javascript: window.history.forward(-1);
    </script>
</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!--BEGIN TITLE & BREADCRUMB PAGE-->

    <div class="page-title-breadcrumb" id="title-breadcrumb-option-demo">
        <div class="page-header pull-left">
            <div class="page-title">
                Dashboard                       
            </div>
        </div>
        <ol class="breadcrumb page-breadcrumb pull-right">
            <li><i class="fa fa-home"></i>&nbsp;<a href="Default.aspx">Home</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="hidden"><a href="#">Dashboard</a>&nbsp;&nbsp;<i class="fa fa-angle-right"></i>&nbsp;&nbsp;</li>
            <li class="active">Dashboard</li>
        </ol>
        <div class="clearfix">
        </div>
    </div>
    <div class="page-content">
        <div class="default-page">
        <div id="tab-general" style="display: none;">
            <div class="row mbl" id="sum_box">
                <div class="col-sm-6 col-md-3">
                    <div class="panel profit db mbm">
                        <div class="panel-body">
                            <p class="icon">
                                <i class="icon fa fa-shopping-cart"></i>
                            </p>
                            <h4 class="value">
                                <span data-duration="0" data-step="1" data-end="50" data-start="10" data-counter="">189</span><span>$</span>
                            </h4>
                            <p class="description">
                                Profit description
                            </p>
                            <div class="progress progress-sm mbn">
                                <div class="progress-bar progress-bar-success" style="width: 80%;" aria-valuemax="100" aria-valuemin="0" aria-valuenow="80" role="progressbar">
                                    <span class="sr-only">80% Complete (success)</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3">
                    <div class="panel income db mbm">
                        <div class="panel-body">
                            <p class="icon">
                                <i class="icon fa fa-money"></i>
                            </p>
                            <h4 class="value">
                                <span>812</span><span>$</span>
                            </h4>
                            <p class="description">
                                Income detail
                            </p>
                            <div class="progress progress-sm mbn">
                                <div class="progress-bar progress-bar-info" style="width: 60%;" aria-valuemax="100" aria-valuemin="0" aria-valuenow="60" role="progressbar">
                                    <span class="sr-only">60% Complete (success)</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3">
                    <div class="panel task db mbm">
                        <div class="panel-body">
                            <p class="icon">
                                <i class="icon fa fa-signal"></i>
                            </p>
                            <h4 class="value">
                                <span>155</span>
                            </h4>
                            <p class="description">
                                Task completed
                            </p>
                            <div class="progress progress-sm mbn">
                                <div class="progress-bar progress-bar-danger" style="width: 50%;" aria-valuemax="100" aria-valuemin="0" aria-valuenow="50" role="progressbar">
                                    <span class="sr-only">50% Complete (success)</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6 col-md-3">
                    <div class="panel visit db mbm">
                        <div class="panel-body">
                            <p class="icon">
                                <i class="icon fa fa-group"></i>
                            </p>
                            <h4 class="value">
                                <span>376</span>
                            </h4>
                            <p class="description">
                                Visitor description
                            </p>
                            <div class="progress progress-sm mbn">
                                <div class="progress-bar progress-bar-warning" style="width: 70%;" aria-valuemax="100" aria-valuemin="0" aria-valuenow="70" role="progressbar">
                                    <span class="sr-only">70% Complete (success)</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!--BEGIN DASHBORD BODY-->
        <%--<div class="row">
            <div class="col-lg-12">
                <ul class="list-group">
                    <% foreach (DataRow child in MenuData.Tables[1].Rows)
                       { %><li class="list-group-item-custom"><a href="<%= child[0].ToString() %>">
                           <img src="images/icons/<%= child[3].ToString() %>" />
                           <p>
                               <%= child[1].ToString() %>
                               <br />
                           </p>
                       </a>
                       </li>
                    <% } %>
                </ul>
            </div>
        </div>--%>
        <!--BEGIN DASHBORD BODY-->
        </div>
    </div>
    <!--END TITLE & BREADCRUMB PAGE-->
</asp:Content>

