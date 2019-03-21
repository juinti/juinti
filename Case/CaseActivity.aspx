<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CaseActivity.aspx.cs" Inherits="Case_Activity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="top_view clearfix">
        <h1>
            <asp:Literal runat="server" ID="litHeader" />
            <p class="subinfo"> - 
                <asp:Literal ID="litDetailsLabelContractValue" runat="server" /></p>
        </h1>
        <div class="buttons_item selected">
            <span class="fa-stack btn_open_editbox">
                <i class="fa fa-circle fa-stack-2x"></i>
                <i class="fa fa-pencil fa-stack-1x fa-inverse"></i>
            </span>
            <i class="fa fa-plus-circle btn_open_modal_create" title=""></i>
        </div>
    </div>

    <asp:Literal ID="litListStructures" runat="server" />


    <div class="modal editbox">
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtActivityTitle" ID="lblActivityTitle" Text="<%$ Resources:ActivityTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtActivityTitle" />
            </div>
        </div>
        <div class="row contract">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlActivityContract" ID="lblActivityContract" Text="<%$ Resources:ActivityTexts, ModalLabelContract %>" />
                <i class="fa fa-plus-circle create_new" onclick="return modalCreateSwitch(this);"></i>
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlActivityContract" />
                <asp:TextBox CssClass="crete_new_input" ID="txtContractTitle" runat="server" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button4" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnCreateActivity" OnClick="btnSaveActivity_Click" Text="<%$ Resources:Global, Save%>" />
        </div>
    </div>
    <div class="modal create structure">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:ActivityTexts, ModalActivityAddStructureHeader %>" /></span>
        <div class="row material">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtStructureTitle" ID="lblTitle" Text="<%$ Resources:ActivityTexts, Structure %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtStructureTitle" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="btnCancel" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnAddStructure" OnClick="btnAddStructure_Click" Text="<%$ Resources:Global, btnCreate %>" />
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hidStructureID" />

    <div class="modal delete delete_structure">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:ActivityTexts, ModalDeleteStructureText %>" /></span>

        <div class="row">
            <div class="text"><span class="structure_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button3" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-delete" ID="btnDeleteStructure" OnClick="btnDeleteStructure_Click" Text="<%$ Resources:Global, btnDelete %>" />
        </div>
    </div>
</asp:Content>

