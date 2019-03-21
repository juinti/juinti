<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Contacts.aspx.cs" Inherits="Contacts_Contacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="top_view clearfix">
        <h1>
            <asp:Literal ID="litHeader" runat="server" /></h1>
        <div class="buttons_item selected">
            <i class="fa fa-plus-circle btn_open_modal_create" title=""></i>
        </div>
    </div>
    <div class="listview">
        <asp:Literal ID="litListContacts" runat="server" />
    </div>

    <div class="modal create contact">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:ContactsTexts, ModalAddPersonHeader %>" /></span>

        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtName" ID="lblName" Text="<%$ Resources:ContactsTexts, ModalLabelName %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtName" />
            </div>
        </div>
        <div class="row email">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtEmail" ID="lblEmail" Text="<%$ Resources:ContactsTexts, ModalLabelEmail %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtEmail" />
            </div>
        </div>
        <div class="row phone">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtPhone" ID="lblPhone" Text="<%$ Resources:ContactsTexts, ModalLabelPhone %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtPhone" />
            </div>
        </div>

        <div class="row company">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlCompanies" ID="blCompanies" Text="<%$ Resources:ContactsTexts, ModalLabelCompany %>" />
                <i class="fa fa-plus-circle create_new" onclick="return modalCreateSwitch(this);"></i>
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlCompanies" />
                <asp:TextBox CssClass="crete_new_input" ID="txtCompanyName" runat="server" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button2" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnCreatePerson" OnClick="btnCreatePerson_Click" Text="<%$ Resources:ContactsTexts, ModalBtnAddPerson %>" />
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hidContactID" />
    <div class="modal delete delete_contact">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:ContactsTexts, ModalDeleteContactText %>" /></span>

        <div class="row">
            <div class="text"><span class="person_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="btnCancel" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-delete" ID="btnDeleteContact" OnClick="btnDeleteContact_Click" Text="<%$ Resources:ContactsTexts, ModalBtnDeleteContact %>" />
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hidCompanyID" />
    <div class="modal delete delete_company">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:ContactsTexts, ModalDeleteCompanyText %>" /></span>

        <div class="row">
            <div class="text"><span class="company_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button1" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-delete" ID="btnDeleteCompany" OnClick="btnDeleteCompany_Click" Text="<%$ Resources:ContactsTexts, ModalBtnDeleteCompany %>" />
        </div>
    </div>

</asp:Content>

