<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CaseMilestone.aspx.cs" Inherits="MilestonePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top_view clearfix">
        <h1>
            <asp:Literal runat="server" ID="litHeader" />
        </h1>
        <table class="details">
            <tr>
                <th>
                    <asp:Literal ID="litDetailsLabelDateFrom" Text="<%$ Resources:MilestoneTexts, DetailsLabelDateFrom %>" runat="server" /></th>
                <td>
                    <asp:Literal ID="litDetailsLabelDateFromValue" runat="server" /></td>
            </tr>
            <tr>
                <th>
                    <asp:Literal ID="litDetailsLabelDateTo" Text="<%$ Resources:MilestoneTexts, DetailsLabelDateTo %>" runat="server" /></th>
                <td>
                    <asp:Literal ID="litDetailsLabelDateToValue" runat="server" /></td>
            </tr>
        </table>
        <div class="buttons_item">
            <span class="fa-stack btn_open_editbox">
                <i class="fa fa-circle fa-stack-2x"></i>
                <i class="fa fa-pencil fa-stack-1x fa-inverse"></i>
            </span>
        </div>
    </div>
    <div class="modal editbox">
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMilestoneTitle" ID="lblMilestoneTitle" Text="<%$ Resources:MilestoneTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtMilestoneTitle" />
            </div>
        </div>
        <div class="row datestart">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMilestoneDateStart" ID="lblMilestoneDateStart" Text="<%$ Resources:MilestoneTexts, ModalLabelDateStart %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" CssClass="datepicker" ID="txtMilestoneDateStart" />
            </div>
        </div>
        <div class="row datend">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMilestoneDateEnd" ID="lblMilestoneDateEnd" Text="<%$ Resources:MilestoneTexts, ModalLabelDateEnd %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" CssClass="datepicker" ID="txtMilestoneDateEnd" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-reset" ID="btnReset" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnSaveMilestone" OnClick="btnSaveMilestone_Click" Text="<%$ Resources:MilestoneTexts, ModalBtnSave %>" />
        </div>
    </div>


</asp:Content>

