<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Part.aspx.cs" Inherits="PartPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top_view clearfix">
        <h1>
            <asp:Literal runat="server" ID="litHeader" /></h1>

        <div class="topbuttons">
            <div class="buttons_item selected">
                <asp:Panel runat="server" ID="panButtons">
                    <span class="fa-stack btn_open_editbox">
                        <i class="fa fa-circle fa-stack-2x"></i>
                        <i class="fa fa-pencil fa-stack-1x fa-inverse"></i>
                    </span>
                    <i class="fa fa-plus-circle btn_open_modal_create" title=""></i>

                </asp:Panel>
            </div>
        </div>
    </div>
    <div class="modal editbox">
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtPartTitle" ID="lblPartTitle" Text="<%$ Resources:PartTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" CssClass="" ID="txtPartTitle" />
            </div>
        </div>
        <div class="row activity">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlPartStructure" ID="lblPartStructure" Text="<%$ Resources:PartTexts, ModalLabelPartStructure%>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlPartStructure" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-reset" ID="btnReset" OnClick="btnReset_Click" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnSavePart" OnClick="btnSavePart_Click" Text="<%$ Resources:Global, Save %>" />
        </div>
    </div>
    <div class="listview">
        <asp:Literal ID="litMaterialList" runat="server" />
    </div>

    <div class="modal create material">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PartTexts, ModalPartAddMaterialHeader %>" /></span>
        <div class="row material">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlMaterials" ID="lblMaterials" Text="<%$ Resources:MaterialTexts, Material %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlMaterials" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="btnCancel" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnAddMaterial" OnClick="btnAddMaterial_Click" Text="<%$ Resources:PartTexts, ModalBtnAddMaterial %>" />
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hidMaterialID" />
    <div class="modal remove remove_material">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PartTexts, ModalRemoveMaterialText %>" /></span>

        <div class="row">
            <div class="text"><span class="material_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button1" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-delete" ID="btnRemoveMaterial" OnClick="btnRemoveMaterial_Click" Text="<%$ Resources:PartTexts, ModalBtnRemoveMaterial %>" />
        </div>
    </div>


</asp:Content>

