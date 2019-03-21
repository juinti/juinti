<%@ Page Title="" Language="C#" MasterPageFile="~/Empty.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="logo">
     <%--   <asp:Image runat="server" ImageUrl="~/style/gfx/logo.svg" />--%>
    </div>
    <div class="login-box">
        <asp:TextBox ID="txtUsername" runat="server" placeholder="<%$ Resources:Login, Username %>" />
        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" placeholder="<%$ Resources:Login, Password %>" />
          <asp:Button ID="btnLogin" CssClass="btnlogin" Text="<%$ Resources:Login, BtnLogin %>" OnClick="btnLogin_Click" runat="server" />
           <asp:LinkButton ID="btnForgot" CssClass="btnforgot" Text="<%$ Resources:Login, BtnForgot %>" OnClick="btnForgot_Click" runat="server" />
      
    </div>
</asp:Content>
