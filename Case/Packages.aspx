<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Packages.aspx.cs" Inherits="PackagesPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="top_view clearfix column-12">
        <h1 class="column-3"><i class="fa fa-cube"></i>
            <asp:Literal ID="litHeader" runat="server" />
        </h1>
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
        <asp:Literal runat="server" ID="litlistPackages" />
    </div>

    <div class="modal create package">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PackageTexts, ModalCreatePartHeader %>" /></span>
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtPackageTitle" ID="Label1" Text="<%$ Resources:PackageTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtPackageTitle" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button1" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="Button2" OnClick="btnCreatePackage_Click" Text="<%$ Resources:PackageTexts, ModalBtnCreate %>" />
        </div>
    </div>

    <asp:HiddenField ID="hidPackageID" runat="server" />

    <div class="modal delete delete_package">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PackageTexts, ModalRemovePackageText %>" /></span>

        <div class="row">
            <div class="text"><span class="package_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="btnCancel" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-delete" ID="btnDeletePackage" OnClick="btnDeletePackage_Click" Text="<%$ Resources:PackageTexts, ModalBtnDeletePackage %>" />
        </div>
    </div>

</asp:Content>

