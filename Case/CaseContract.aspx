<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CaseContract.aspx.cs" Inherits="Case_CaseContract" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="top_view clearfix">
        <h1>
            <asp:Literal runat="server" ID="litHeader" />
        </h1>
         <div class="buttons_item selected">
        <span class="fa-stack btn_open_editbox">
            <i class="fa fa-circle fa-stack-2x"></i>
            <i class="fa fa-pencil fa-stack-1x fa-inverse"></i>
        </span>
    </div>
    </div>
   
   

    <div class="modal editbox">
        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtContractTitle" ID="lblContractTitle" Text="<%$ Resources:ContractTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtContractTitle" />
            </div>
        </div>      
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button4" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnSaveContract" OnClick="btnSaveContract_Click" Text="<%$ Resources:Global, Save%>" />
        </div>
    </div>
</asp:Content>

