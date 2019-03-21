<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DeliveryPlan.aspx.cs" Inherits="Case_DeliveryPlan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top_view clearfix column-12 hastabs">
        <h1 class="column-6"><i class="fa fa-home"></i>
            Overblik
        </h1>
         <ul class="tabs-menu clearfix column-12">
        <li>
            <asp:HyperLink NavigateUrl="~/case/case.aspx?caseid=5&pagetype=overview#tabindex-0" runat="server" Text="Detaljer" /></li>
        <li>
            <asp:HyperLink NavigateUrl="~/case/case.aspx?caseid=5&pagetype=overview#tabindex-1" runat="server" Text="Arbejde" /></li>
        <li>
            <asp:HyperLink NavigateUrl="~/case/case.aspx?caseid=5&pagetype=overview#tabindex-2" runat="server" Text="Milepæle" /></li>
        <li class="selected"><span>Mængdeudtræk</span></li>
    </ul>
    </div>
   

    <div class="column-12 overviewbox listview">
        <div class="filter_search">
            <div class="row company">
                <div class="label">
                    <asp:Label runat="server" AssociatedControlID="ddlCompany" ID="lblCompany" Text="<%$ Resources:MaterialTexts, ModalLabelMaterialCompany %>" />
                </div>
                <div class="input">
                    <asp:DropDownList runat="server" ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" />
                </div>
            </div>

            <div class="row material">
                <div class="label">
                    <asp:Label runat="server" AssociatedControlID="ddlMaterial" ID="Label1" Text="<%$ Resources:DeliveryPlanTexts, ModalLabelMaterial %>" />
                </div>
                <div class="input">
                    <asp:DropDownList runat="server" ID="ddlMaterial" />
                </div>
            </div>
            <div class="row datefrom">
                <div class="label">
                    <asp:Label runat="server" AssociatedControlID="txtDateFrom" ID="lblDateFrom" Text="<%$ Resources:DeliveryPlanTexts, LabelDateFrom %>" />
                </div>
                <div class="input">
                    <asp:TextBox runat="server" CssClass="datepicker" autocomplete="off" ID="txtDateFrom" />
                </div>
            </div>
            <div class="row dateto">
                <div class="label">
                    <asp:Label runat="server" AssociatedControlID="txtDateTo" ID="lblDateTo" Text="<%$ Resources:DeliveryPlanTexts, LabelDateTo %>" />
                </div>
                <div class="input">
                    <asp:TextBox runat="server" CssClass="datepicker" autocomplete="off" ID="txtDateTo" />
                </div>
            </div>
            <div class="buttons">
                <asp:Button runat="server" CssClass="btn ui-reset" ID="btnReset" OnClick="btnReset_Click" Text="<%$ Resources:Global, BtnCancelText %>" />
                <asp:Button runat="server" CssClass="btn ui-save" ID="btnGetList" OnClick="btnGetList_Click" Text="<%$ Resources:DeliveryPlanTexts, ModalBtnGetList %>" />
            </div>
        </div>

        <asp:Literal ID="litList" runat="server" />
    </div>
</asp:Content>

