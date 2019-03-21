<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NextWaives.ascx.cs" Inherits="Controls_NextWaives" %>
<div class="boxwrap">
    <div class="listbox">
        <h2>
            <asp:Literal ID="litHeader" Text="<%$ Resources:WaiveTexts, NextWaivesBoxHeader %>" runat="server" /></h2>

        <asp:Literal ID="litWaives" runat="server" />
    </div>
</div>
