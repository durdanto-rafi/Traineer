<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Error.aspx.cs" Inherits="Traineer.Error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="page-content page-content-custom">
        <div class="col-lg-12 col-xs-12">
            <div class="row">
                <section class="content">
                    <div class="error-page">
                        <h2 class="headline text-info">Oops!</h2>
                        <div class="error-content">
                            <h3><i class="fa fa-warning text-yellow"></i>You dont have permission for this page.</h3>
                            <p>
                                Please contact to system administrator about the page you were looking for. 
                                Meanwhile, you may <a href="../../default.aspx">return to dashboard</a>.
                            </p>
                        </div>
                        <!-- /.error-content -->
                    </div>
                    <!-- /.error-page -->
                </section>
            </div>
        </div>
    </div>
</asp:Content>
