<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="WaiveEmail.aspx.cs" Inherits="WaiveEmailPage" %>

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
                    <asp:LinkButton runat="server" ID="btnSendEmail" OnClick="btnSendEmail_Click">
                    <span class="fa-stack">
                        <i class="fa fa-circle fa-stack-2x"></i>
                        <i class="fa fa-envelope-o fa-stack-1x fa-inverse"></i>
                    </span></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="btnSend" OnClick="btnSend_Click">
                    <span class="fa-stack">
                        <i class="fa fa-circle fa-stack-2x"></i>
                        <i class="fa fa-calendar-check-o fa-stack-1x fa-inverse"></i>
                    </span></asp:LinkButton>
                    <span class="fa-stack btn_open_editbox">
                        <i class="fa fa-circle fa-stack-2x"></i>
                        <i class="fa fa-pencil fa-stack-1x fa-inverse"></i>
                    </span>


                    <asp:Button runat="server" CssClass="btn ui-save" ID="btnProduction" OnClick="btnProduction_Click" Text="<%$ Resources:WaiveTexts, ModalBtnProduction%>" />
                    <asp:Button runat="server" CssClass="btn ui-delete" ID="btnRemoveProduction" OnClick="btnRemoveProduction_Click" Text="<%$ Resources:WaiveTexts, ModalBtnRemoveProduction%>" />
                </div>
            </div>
        </asp:Panel>
        <asp:Panel runat="server" ID="panCancel" Visible="false">
            <div class="topbuttons column-6">
                <div class="buttons_item selected">
                    <asp:LinkButton ID="lnkCancel" runat="server" OnClick="lnkCancel_Click">
                    <span class="fa-stack">
                        <i class="fa fa-circle fa-stack-2x"></i>
                        <i class="fa fa-calendar-minus-o fa-stack-1x fa-inverse"></i>
                    </span>
                    </asp:LinkButton>

                </div>
            </div>
        </asp:Panel>
        <ul class="tabs-menu column-12">
            <li>
                <asp:HyperLink runat="server" ID="linkPackages" Text="Pakker" />
            </li>
            <li class="selected">
                <asp:HyperLink runat="server" ID="linkDetails" Text="Detaljer" />
            </li>
        </ul>
    </div>

    <div class="column-12 listview overviewbox emailwrap" id="copyarea">
        <asp:Button runat="server" Text="Gendan" ID="btnResetChanges" OnClick="btnResetChanges_Click" CssClass="btn ui-delete reset"/>
        <asp:Literal ID="litEmail" runat="server" />
    </div>

    <div class="modal editbox">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:Global, Edit %>" />
        </span>
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


    <div class="modal edit waiveamount">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:Global, Edit %>" />
        </span>
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlMaterial" ID="lblddlMaterial" Text="<%$ Resources:WaiveTexts, ModalLabelMaterial %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlMaterial" />
            </div>
        </div>

        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button5" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnSavePackage" Text="<%$ Resources:Global, Save %>" />
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hidPacakageID" />
    <asp:HiddenField runat="server" ID="hidMaterialID" />
    <asp:HiddenField runat="server" ID="hidLength" />
    <asp:HiddenField runat="server" ID="hidWidth" />

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

