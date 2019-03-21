<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Company.aspx.cs" Inherits="Contacts_Company" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="top_view clearfix">
        <h1>
            <asp:literal runat="server" id="litHeader" />
        </h1>
    </div>
    <div class="listview">
        <div class="row name">
            <div class="label">
                <asp:label runat="server" associatedcontrolid="txtName" id="lblName" text="<%$ Resources:ContactsTexts, ModalLabelName %>" />
            </div>
            <div class="input">
                <asp:textbox runat="server" cssclass="" id="txtName" />
            </div>
        </div>
        <div class="row email">
            <div class="label">
                <asp:label runat="server" associatedcontrolid="txtEmail" id="lblEmail" text="<%$ Resources:ContactsTexts, ModalLabelEmail %>" />
            </div>
            <div class="input">
                <asp:textbox runat="server" id="txtEmail" />
            </div>
        </div>

        <div class="row phone">
            <div class="label">
                <asp:label runat="server" associatedcontrolid="txtPhone" id="lblPhone" text="<%$ Resources:ContactsTexts, ModalLabelPhone %>" />
            </div>
            <div class="input">
                <asp:textbox runat="server" id="txtPhone" />
            </div>
        </div>

        <div class="row address">
            <div class="label">
                <asp:label runat="server" associatedcontrolid="txtAddress" id="lblAddress" text="<%$ Resources:ContactsTexts, ModalLabelAddress %>" />
            </div>
            <div class="input">
                <asp:textbox runat="server" id="txtAddress" />
            </div>
        </div>
        <div class="row address">
            <div class="label">
                <asp:label runat="server" associatedcontrolid="txtAddress2" id="lblAddress2" text="<%$ Resources:ContactsTexts, ModalLabelAddress2 %>" />
            </div>
            <div class="input">
                <asp:textbox runat="server" id="txtAddress2" />
            </div>
        </div>
        <div class="row zipcode">
            <div class="label">
                <asp:label runat="server" associatedcontrolid="txtZipCode" id="lblZipcCode" text="<%$ Resources:ContactsTexts, ModalLabelZipCode %>" />
            </div>
            <div class="input">
                <asp:textbox runat="server" id="txtZipCode" />
            </div>
        </div>
        <div class="row city">
            <div class="label">
                <asp:label runat="server" associatedcontrolid="txtCity" id="lblCity" text="<%$ Resources:ContactsTexts, ModalLabelCity %>" />
            </div>
            <div class="input">
                <asp:textbox runat="server" id="txtCity" />
            </div>
        </div>
        <div class="buttons clearfix">
            <asp:button runat="server" cssclass="btn ui-reset" id="btnReset" onclick="btnReset_Click" text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:button runat="server" cssclass="btn ui-save" id="btnSaveContact" onclick="btnSaveContact_Click" text="<%$ Resources:MaterialTexts, ModalBtnSave %>" />
        </div>
    </div>



      

</asp:Content>

