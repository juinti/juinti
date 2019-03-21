<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Person.aspx.cs" Inherits="Contacts_Person" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


    <div class="top_view clearfix">
        <h1>
            <asp:Literal runat="server" ID="litHeader" /></h1>     
    </div>
    <div class="itemview">
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtName" ID="lblName" Text="<%$ Resources:ContactsTexts, ModalLabelName %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" CssClass="" ID="txtName" />
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
                <asp:Label runat="server" AssociatedControlID="ddlCompany" ID="Label1" Text="<%$ Resources:ContactsTexts, ModalLabelSelectedCompany %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlCompany" />
            </div>
        </div>   

        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-reset" ID="btnReset" OnClick="btnReset_Click" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnSaveContact" OnClick="btnSaveContact_Click" Text="<%$ Resources:MaterialTexts, ModalBtnSave %>" />
        </div>
    </div>


</asp:Content>

