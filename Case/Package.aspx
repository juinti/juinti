<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Package.aspx.cs" Inherits="PackagePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="top_view clearfix column-12 hastabs">
        <h1>
            <asp:Literal ID="litHeader" runat="server" /></h1>



        <div class="topbuttons">
            <div class="buttons_item">
                     <span class="fa-stack btn_open_editbox">
                        <i class="fa fa-circle fa-stack-2x"></i>
                        <i class="fa fa-pencil fa-stack-1x fa-inverse"></i>
                    </span>
            </div>
            <div class="buttons_item">
                <asp:Panel ID="panEdit" runat="server">
               
                </asp:Panel>
                <asp:LinkButton ID="lnkAddPart" runat="server"><i class="fa fa-plus-circle btn_open_modal_create" title=""></i></asp:LinkButton>
                <asp:LinkButton ID="lnkCopyPackage" CssClass="btn" Text="<%$ Resources:PackageTexts, ModalCopyPackageHeader %>" OnClick="lnkCopyPackage_Click" runat="server" />
            </div>
        </div>
        <ul class="tabs-menu clearfix column-12">
            <li><span>Detaljer</span></li>
            <li><span>Produktionsdele / mængder</span></li>
        </ul>
    </div>


    <div class="column-12 listview">
        <div class="tab">
            <table class="listview details">
                <tr>
                    <th>
                        <asp:Literal ID="litDetailsPriceLabel" Text="<%$ Resources:PackageTexts, DetailsLabelPrice %>" runat="server" /></th>
                    <td>
                        <asp:Literal ID="litDetailsPriceValue" runat="server" /></td>
                </tr>
                <asp:Panel ID="panRevison" runat="server">
                    <tr>
                        <th>
                            <asp:Literal ID="litDetailsRevisionLabel" Text="<%$ Resources:PackageTexts, DetailsLabelRevision %>" runat="server" /></th>
                        <td>
                            <asp:Literal ID="litRevision" runat="server" /></td>
                    </tr>
                </asp:Panel>
            </table>
        </div>
        <div class="tab">
            <asp:Literal ID="litListParts" runat="server" />
        </div>
    </div>
    <div class="modal editbox">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:Global, Edit %>" /></span>
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtPackageTitle" ID="lblPackageTitle" Text="<%$ Resources:PackageTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" CssClass="" ID="txtPackageTitle" autocomplete="off" />
            </div>
        </div>

        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button5" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnSavePackage" OnClick="btnSavePackage_Click" Text="<%$ Resources:Global, Save %>" />
        </div>
    </div>


    <asp:HiddenField runat="server" ID="hidPartID" />
    <asp:HiddenField runat="server" ID="hidMaterialID" />
    <asp:HiddenField runat="server" ID="hidLength" />
    <asp:HiddenField runat="server" ID="hidWidth" />

    <div class="modal create add_part">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PackageTexts, ModalAddPartHeader %>" /></span>
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlParts" ID="lblParts" Text="<%$ Resources:PartTexts, Part %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlParts" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button2" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnAddPart" OnClick="btnAddPart_Click" Text="<%$ Resources:PackageTexts, ModalBtnAddPart %>" />
        </div>
    </div>


    <div class="modal remove remove_part">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PackageTexts, ModalRemovePartHeader %>" /></span>
        <div class="row">
            <div class="text"><span class="part_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button3" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnRemovePart" OnClick="btnRemovePart_Click" Text="<%$ Resources:PackageTexts, ModalBtnRemovePart %>" />
        </div>
    </div>


    <div class="modal add add_material_amount">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PackageTexts, ModalAddMaterialAmountHeader %>" /></span>

        <div class="row length hide">

            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtLength" ID="lblLength" Text="<%$ Resources:PackageTexts, Length %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtLength" CssClass="add_material_amount_length" />
            </div>
        </div>
        <div class="row width hide">

            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtWidth" ID="lblWidth" Text="<%$ Resources:PackageTexts, Width %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtWidth" CssClass="add_material_amount_width" />
            </div>
        </div>

        <div class="row amount">

            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtAmount" ID="Label1" Text="<%$ Resources:PackageTexts, Amount %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtAmount" autocomplete="off" />
            </div>
        </div>

        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button1" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnAddMaterialAmount" OnClick="btnAddMaterialAmount_Click" Text="<%$ Resources:PackageTexts, ModalBtnAddAmount %>" />
        </div>
    </div>

    <div class="modal add edit_material_amount">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PackageTexts, ModalEditMaterialAmountHeader %>" /></span>

        <div class="row amount">

            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtEditAmount" ID="Label4" Text="<%$ Resources:PackageTexts, Amount %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtEditAmount" autocomplete="off" />
            </div>
        </div>

        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button4" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnEditMatrialAmount" OnClick="btnEditMatrialAmount_Click" Text="<%$ Resources:PackageTexts, ModalBtnEditAmount %>" />
        </div>
    </div>



</asp:Content>

