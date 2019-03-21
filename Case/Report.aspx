<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Report.aspx.cs" Inherits="Case_Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/js/chart.bundle.min.js"></script>
    <script src="/js/report.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top_view clearfix column-12 hastabs">
        <h1 class="column-6"><i class="fa fa-bar-chart"></i>
            Rapport
        </h1>
        <ul class="tabs-menu clearfix column-12">
            <li><span>Forbrug</span></li>
        </ul>
    </div>

    <div class="column-12 listview">
        <div class="reporttab">
            <div class="row material">
                <div class="label">
                    <asp:Label runat="server" AssociatedControlID="ddlMaterial" ID="Label1" Text="<%$ Resources:DeliveryPlanTexts, ModalLabelMaterial %>" />
                </div>
                <div class="input">
                    <asp:DropDownList runat="server" ID="ddlMaterial" />
                </div>
            </div>

            <div class="row material">
                <div class="label">
                    <asp:Label runat="server" AssociatedControlID="ddlActivity" ID="Label2" Text="<%$ Resources:DeliveryPlanTexts, ModalLabelActivity %>" />
                </div>
                <div class="input">
                    <asp:DropDownList runat="server" ID="ddlActivity" />
                </div>
            </div>

            <div class="row material">
                <div class="label">
                    <asp:Label runat="server" AssociatedControlID="ddlPart" ID="Label3" Text="<%$ Resources:DeliveryPlanTexts, ModalLabelPart %>" />
                </div>
                <div class="input">
                    <asp:DropDownList runat="server" ID="ddlPart" />
                </div>
            </div>


            <div class="row datefrom">
                <div class="label">
                    <asp:Label runat="server" AssociatedControlID="txtDateFrom" ID="lblDateFrom" Text="<%$ Resources:DeliveryPlanTexts, LabelDateFrom %>" />
                </div>
                <div class="input">
                    <asp:TextBox runat="server" CssClass="datepicker" ID="txtDateFrom" />
                </div>
            </div>
            <div class="row dateto">
                <div class="label">
                    <asp:Label runat="server" AssociatedControlID="txtDateTo" ID="lblDateTo" Text="<%$ Resources:DeliveryPlanTexts, LabelDateTo %>" />
                </div>
                <div class="input">
                    <asp:TextBox runat="server" CssClass="datepicker" ID="txtDateTo" />
                </div>
            </div>
            <div class="buttons">
                <asp:Button runat="server" CssClass="btn ui-reset" ID="btnReset" OnClick="btnReset_Click" Text="<%$ Resources:Global, BtnCancelText %>" />
                <asp:Button runat="server" CssClass="btn ui-save" ID="btnGetReport" OnClick="btnGetReport_Click" Text="<%$ Resources:DeliveryPlanTexts, ModalBtnGetReport %>" />
            </div>


            <h2>
                <asp:Literal ID="litTitle" runat="server" /></h2>
            <div class="reporttable">
                <asp:Literal ID="litTotalPrices" runat="server" />
            </div>
            <div class="reportchart">
                <asp:Literal ID="litChartScript" runat="server" />
                <canvas id="reportMaterialChart" width="400" height="100"></canvas>
            </div>

        </div>

    </div>

    <script type="text/javascript">

        $("select").on("change", function () {
            var selVal = $(this).find("option:selected").val();
            $("select").val("-1");
            $(this).val(selVal);
        });

    </script>






</asp:Content>

