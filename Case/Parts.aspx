<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="parts.aspx.cs" Inherits="PartsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top_view clearfix column-12">
        <h1 class="column-3"><i class="fa fa-puzzle-piece"></i>
            <asp:Literal ID="litHeader" runat="server" /></h1>

          <div class="search column-6">
            <i class="fa fa-search"></i><input type="text" placeholder="Søg..." id="search"/>
        </div>

        <div class="topbuttons column-3">
            <div class="buttons_item selected">
                <i class="fa fa-plus-circle btn_open_modal_create" title=""></i>
            </div>
        </div>
    </div>
    <div class="listview clearfix column-12">
        <asp:Literal runat="server" ID="litlistActivities" />
    </div>

    <div class="modal create part">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PartTexts, ModalCreatePartHeader %>" /></span>

        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtPartTitle" ID="Label1" Text="<%$ Resources:PartTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtPartTitle" />
            </div>
        </div>
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlPartStructure" ID="lblPartStructure" Text="<%$ Resources:PartTexts, ModalLabelPartStructure %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlPartStructure" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button2" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnCreatePart" OnClick="btnCreatePart_Click" Text="<%$ Resources:PartTexts, ModalBtnCreate %>" />
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hidPartID" />
    <asp:HiddenField runat="server" ID="hidActivityID" />

    <div class="modal delete delete_part">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PartTexts, ModalDeletePartText %>" /></span>

        <div class="row">
            <div class="text"><span class="part_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="btnCancel" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-delete" ID="btnDeletePart" OnClick="btnDeletePart_Click" Text="<%$ Resources:PartTexts, ModalBtnDeletePart %>" />
        </div>
    </div>
</asp:Content>

