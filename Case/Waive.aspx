<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Waive.aspx.cs" Inherits="WaivePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="top_view clearfix column-12 hastabs">
        <h1 class="column-6">
            <asp:Literal ID="litHeader" runat="server" />
        </h1>

        <asp:Panel ID="panButtons" runat="server">
            <div class="topbuttons column-6">
                <div class="buttons_item selected">

                    <asp:Panel ID="panAddPackage" runat="server">
                        <i class="fa fa-plus-circle btn_open_modal_create" onclick="return addWaivePackage();"></i>
                    </asp:Panel>
                    <asp:Panel ID="panAddMaterial" runat="server">
                        <i class="fa fa-plus-circle btn_open_modal_create" onclick="return addWaiveMaterial();"></i>
                    </asp:Panel>
                    <span class="fa-stack btn_open_editbox">
                        <i class="fa fa-circle fa-stack-2x"></i>
                        <i class="fa fa-pencil fa-stack-1x fa-inverse"></i>
                    </span>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="panCancel" Visible="false">
            <div class="topbuttons column-6">
                <div class="buttons_item selected">
                    <asp:LinkButton ID="lnkCancel" OnClick="lnkCancel_Click" runat="server">
                      <span class="fa-stack">
                        <i class="fa fa-circle fa-stack-2x"></i>
                        <i class="fa fa-calendar-minus-o fa-stack-1x fa-inverse"></i>
                    </span>
                    </asp:LinkButton>

                </div>
            </div>
        </asp:Panel>
         <ul class="tabs-menu clearfix column-12">
        <li class="selected">
            <asp:HyperLink runat="server" ID="linkPackages" Text="Pakker" /></li>
        <li>
            <asp:HyperLink runat="server" ID="linkDetails" Text="Detaljer" /></li>
    </ul>
    </div>
   
    <div class="modal editbox">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:Global, Edit %>" /></span>
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtWaiveTitle" ID="lblWaiveTitle" Text="<%$ Resources:WaiveTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" CssClass="" ID="txtWaiveTitle" />
            </div>
        </div>
        <div class="row date">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlMilestone" ID="lblMilestone" Text="<%$ Resources:WaiveTexts, ModalLabelMilestone %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlMilestone" />
            </div>
        </div>

        <div class="row estdelivery">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtEstDeliveryDate" ID="lblEstDeliveryDate" Text="<%$ Resources:WaiveTexts, ModalLabelEstDeliveryDate %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" CssClass="datepicker" ID="txtEstDeliveryDate" />
            </div>
        </div>

        <div class="row location">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtLocation" ID="Label2" Text="<%$ Resources:WaiveTexts, ModalLabelLocation %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtLocation" />
            </div>
        </div>

        <div class="row text">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMessage" ID="lblMessage" Text="<%$ Resources:WaiveTexts, ModalLabelMessage %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtMessage" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-reset" ID="btnReset" OnClick="btnReset_Click" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnSaveWaive" OnClick="btnSaveWaive_Click" Text="<%$ Resources:Global, Save %>" />
        </div>
    </div>


    <div class="listview clearfix column-12">
        <asp:Literal ID="litPackagesList" runat="server" />
        <asp:Literal ID="litMaterialsList" runat="server" />
    </div>


    <div class="modal create add_package">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:WaiveTexts, ModalAddPackage %>" />
        </span>
        <div class="row">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlWaivePackage" ID="lblWaivePackage" Text="<%$ Resources:WaiveTexts, ModalLabelWaivePackage %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlWaivePackage" />
            </div>
        </div>
        <div class="row">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtAmount" ID="lblAmount" Text="<%$ Resources:WaiveTexts, ModalLabelAmount %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtAmount" autocomplete="off" />
            </div>
        </div>

        <div class="row">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtProductionsWeeks" ID="Label3" Text="<%$ Resources:WaiveTexts, ModalLabelProductionWeeks %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtProductionsWeeks" autocomplete="off" />
            </div>
        </div>


        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button2" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnAddPackage" OnClick="btnAddPackage_Click" Text="<%$ Resources:WaiveTexts, ModalBtnAddPackage %>" />
        </div>
    </div>

    <div class="modal add waive_add_material add_material">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PackageTexts, ModalAddMaterialAmountHeader %>" />
        </span>

        <div class="row material">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlMaterial" ID="lblMaterial" Text="<%$ Resources:MaterialTexts, Material %>" />
            </div>
            <div class="input">
                <asp:DropDownList CssClass="waive_add_material_ddl" runat="server" ID="ddlMaterial" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button3" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnAddMaterial" OnClick="btnAddMaterial_Click" Text="<%$ Resources:PackageTexts, ModalBtnAddAmount %>" />
        </div>
    </div>


    <asp:HiddenField runat="server" ID="hidMaterialID" />
    <asp:HiddenField runat="server" ID="hidLength" />
    <asp:HiddenField runat="server" ID="hidWidth" />
    <asp:HiddenField runat="server" ID="hidPackageID" />

    <div class="modal remove remove_package">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:WaiveTexts, ModalRemovePackageText %>" />
        </span>

        <div class="row">
            <div class="text"><span class="package_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="btnCancel" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-remove" ID="btnRemovePackage" OnClick="btnRemovePackage_Click" Text="<%$ Resources:WaiveTexts, ModalBtnRemovePackage %>" />
        </div>
    </div>

    <div class="modal edit edit_package_amount">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:WaiveTexts, ModalEditPackageAmount %>" />
        </span>
        <div class="row">
            <div class="label">
                <label><span class="package_title"></span></label>
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtEditAmount" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button1" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnEditAmount" OnClick="btnEditAmount_Click" Text="<%$ Resources:WaiveTexts, ModalBtnEditPackageAmount %>" />
        </div>
    </div>

    <div class="modal edit edit_package_week">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:WaiveTexts, ModalEditPackageWeeks %>" />
        </span>
        <div class="row">
            <div class="label">
                <label><span class="package_title"></span></label>
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtEditWeeks" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button6" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnEditWeeks" OnClick="btnEditWeeks_Click" Text="<%$ Resources:WaiveTexts, ModalBtnEditPackageAmount %>" />
        </div>
    </div>

    <div class="modal add add_material_amount">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PackageTexts, ModalAddMaterialAmountHeader %>" />
        </span>

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
                <asp:Label runat="server" AssociatedControlID="txtMaterialAmount" ID="Label1" Text="<%$ Resources:PackageTexts, Amount %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtMaterialAmount" />
            </div>
        </div>

        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button5" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnAddMaterialAmount" OnClick="btnAddMaterialAmount_Click" Text="<%$ Resources:PackageTexts, ModalBtnAddAmount %>" />
        </div>
    </div>

    <div class="modal add edit_material_amount">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:PackageTexts, ModalEditMaterialAmountHeader %>" />
        </span>

        <div class="row amount">

            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtEditMaterialAmount" ID="Label4" Text="<%$ Resources:PackageTexts, Amount %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtEditMaterialAmount" />
            </div>
        </div>

        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button4" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnEditMatrialAmount" OnClick="btnEditMatrialAmount_Click" Text="<%$ Resources:PackageTexts, ModalBtnEditAmount %>" />
        </div>
    </div>




</asp:Content>

