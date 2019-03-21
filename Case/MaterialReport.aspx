<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MaterialReport.aspx.cs" Inherits="MaterialReportPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top_view clearfix column-12 hastabs">
        <h1 class="column-6">
            <asp:Literal runat="server" ID="litHeader" />
        </h1>
        <div class="topbuttons column-6">
            <div class="buttons_item selected">
            </div>
        </div>

        <ul class="tabs-menu clearfix column-12">
            <li>
                <asp:HyperLink runat="server" ID="linkDetails" Text="Detaljer" />
            </li>
            <li class="selected">
                <span>Rapport</span></li>
        </ul>
    </div>


    <div class="column-12 listview">
        <div class="reporttab">
            <asp:Literal ID="litChartScript" runat="server" />

            <script type="text/javascript">
                
                   </script>

            <div class="chart-wrap column-12">
                <h2>Antal pr. måned</h2>
                <canvas id="reportMaterialChart" style="width:100%!important; height:100px!important;"></canvas>
            </div>
        </div>
    </div>

</asp:Content>

