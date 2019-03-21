<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Material.aspx.cs" Inherits="MaterialPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="top_view clearfix column-12 hastabs">
        <h1 class="column-6">
            <asp:Literal runat="server" ID="litHeader" /></h1>
        <div class="topbuttons column-6">
            <div class="buttons_item selected">
                <span class="fa-stack btn_open_editbox">
                    <i class="fa fa-circle fa-stack-2x"></i>
                    <i class="fa fa-pencil fa-stack-1x fa-inverse"></i>
                </span>
            </div>
        </div>

        <ul class="tabs-menu clearfix column-12">
            <li class="selected"><span>Detaljer</span></li>
            <li>
                <asp:HyperLink runat="server" ID="linkReport" Text="Rapport" /></li>
        </ul>
    </div>


    <div class="column-12 listview">    
        
        <table class="listview details">

            <tr>
                <th>Varenummer</th>
                <td>
                    <asp:Literal ID="litPartnumberDetails" runat="server" /></td>
            </tr>
            <tr>
                <th>Enhed</th>
                <td><asp:Literal ID="litUnitDetails" runat="server" /></td>
            </tr>
            <tr>
                <th>Pris/Enhed</th>
                <td><asp:Literal ID="litPriceUnitDetails" runat="server" /></td>
            </tr>
             <tr>
                <th>Type</th>
                <td><asp:Literal ID="litTypeDetails" runat="server" /></td>
            </tr>
             <tr>
                <th>Minimums bestilling</th>
                <td><asp:Literal ID="litMinimumOrderDetails" runat="server" /></td>
            </tr>
              <tr>
                <th>Leverandør</th>
                <td><asp:Literal ID="litCompanyDetails" runat="server" /></td>
            </tr>
        </table>
    </div>
    <div class="modal editbox">
        <div class="row minstock">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMaterialNumber" ID="Label1" Text="<%$ Resources:MaterialTexts, ModalLabelNumber %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtMaterialNumber" autocomplete="off" />
            </div>
        </div>


        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMaterialTitle" ID="lblMaterialTitle" Text="<%$ Resources:MaterialTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" CssClass="" ID="txtMaterialTitle" />
            </div>
        </div>
        <div class="row unit">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlMaterialUnit" ID="lblMaterialUnit" Text="<%$ Resources:MaterialTexts, ModalLabelMaterialUnit %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlMaterialUnit" />
            </div>
        </div>
        <div class="row price">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMaterialPrice" ID="lblMaterialPrice" Text="<%$ Resources:MaterialTexts, ModalLabelMaterialPrice %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtMaterialPrice" autocomplete="off" />
            </div>
        </div>

        <div class="row type">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlMaterialType" ID="lblMaterialType" Text="<%$ Resources:MaterialTexts, ModalLabelMaterialType %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlMaterialType" />
            </div>
        </div>

        <div class="row minstock">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMinStockAmount" ID="lblMinStockAmount" Text="<%$ Resources:MaterialTexts, ModalLabelMinStockAmount %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtMinStockAmount" autocomplete="off" />
            </div>
        </div>

        <div class="row type">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlCompany" ID="lblCompany" Text="<%$ Resources:MaterialTexts, ModalLabelMaterialCompany %>" />
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlCompany" />
            </div>
        </div>

        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-reset" ID="btnReset" OnClick="btnReset_Click" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-save" ID="btnSaveMaterial" OnClick="btnSaveMaterial_Click" Text="<%$ Resources:MaterialTexts, ModalBtnSave %>" />
        </div>
    </div>


</asp:Content>

