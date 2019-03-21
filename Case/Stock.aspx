<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Stock.aspx.cs" Inherits="Case_Stock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="top_view clearfix column-12">
        <h1 class="column-3"><i class="fa fa-th"></i>
          Lager</h1>

        <div class="search column-6">
            <i class="fa fa-search"></i><input type="text" placeholder="Søg..." id="search"/>
        </div>
        <div class="topbuttons column-3">
            <div class="buttons_item selected">
        </div>
        </div>
    </div>
    <div class="listview column-12 clearfix">
        <asp:Literal ID="litStockList" runat="server" />
    </div>
</asp:Content>

