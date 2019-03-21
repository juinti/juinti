<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Materials.aspx.cs" Inherits="MaterialsPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top_view clearfix column-12">
        <h1 class="column-3"><i class="fa fa-cubes"></i>
            <asp:Literal runat="server" ID="litHeader" /></h1>

        <div class="search column-6">
            <i class="fa fa-search"></i><input type="text" placeholder="Søg..." id="search"/>
        </div>
        <div class="topbuttons column-3">
            <div class="buttons_item selected">
                <i class="fa fa-plus-circle btn_open_modal_create" title=""></i>
            </div>
        </div>
    </div>
    <div class="listview column-12 clearfix">
        <asp:Literal ID="litMaterialList" runat="server" />
    </div>

    <div class="modal create material">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:MaterialTexts, ModalCreateMaterialHeader %>" /></span>
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMaterialTitle" ID="lblMaterialTitle" Text="<%$ Resources:MaterialTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtMaterialTitle" />
            </div>
        </div>
        <div class="row unit">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlMaterialUnit" ID="lblMaterialUnit" Text="<%$ Resources:MaterialTexts, ModalLabelMaterialUnit %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlMaterialUnit" />
            </div>
        </div>
        <div class="row price">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMaterialPrice" ID="lblMaterialPrice" Text="<%$ Resources:MaterialTexts, ModalLabelMaterialPrice %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtMaterialPrice" />
            </div>
        </div>

        <div class="row type">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlMaterialType" ID="lblMaterialType" Text="<%$ Resources:MaterialTexts, ModalLabelMaterialType %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlMaterialType" />
            </div>
        </div>
        <div class="row company">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlCompany" ID="lblCompany" Text="<%$ Resources:MaterialTexts, ModalLabelMaterialCompany %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlCompany" />
            </div>
        </div>


        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button2" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnCreateMaterial" OnClick="btnCreateMaterial_Click" Text="<%$ Resources:MaterialTexts, ModalBtnCreate %>" />
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hidMaterialID" />

    <div class="modal delete delete_material">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:MaterialTexts, ModalRemoveMaterialText %>" /></span>

        <div class="row">
            <div class="text"><span class="material_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="btnCancel" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-delete" ID="btnDeleteMaterial" OnClick="btnDeleteMaterial_Click" Text="<%$ Resources:MaterialTexts, ModalBtnDeleteMaterial %>" />
        </div>
    </div>


</asp:Content>

