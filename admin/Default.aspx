<%@ Page Title="" Language="C#" MasterPageFile="~/admin/AdminMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:TextBox runat="server" placeholder="Name" ID="txtName"/>
    <asp:TextBox runat="server" placeholder="ContactPerson" ID="txtContactPerson"/>
    <asp:TextBox runat="server" placeholder="Phonenumber" ID="txtPhonenumber"/>
    <asp:TextBox ID="txtEmail" placeholder="Email" runat="server"/>
    <asp:TextBox ID="txtPassword" placeholder="Password" runat="server" />

    <asp:Button Text="CREATE USER" runat="server" OnClick="btn_CreateCustomer_Click" ID="btn_CreateCustomer"/>
</asp:Content>

