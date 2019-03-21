<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Waives.aspx.cs" Inherits="WaivesPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%@ Register Src="~/Controls/Filter.ascx" TagPrefix="uc1" TagName="Filter" %>

    <div class="top_view clearfix column-12 hastabs">
        <h1 class="column-3"><i class="fa fa-truck"></i>
            <asp:Literal ID="litHeader" Text="<%$ Resources:WaiveTexts, Waives %>" runat="server" />
        </h1>
        <div class="search column-6">
            <i class="fa fa-search"></i>
            <input type="text" placeholder="Søg..." id="search" />
        </div>
        <div class="filter" style="display: none;">
            <uc1:Filter runat="server" ID="Filter" />
        </div>
        <div class="topbuttons column-3">
            <div class="buttons_item selected">
                <i class="fa fa-plus-circle btn_open_modal_create" title=""></i>
            </div>
        </div>
        <ul class="tabs-menu clearfix column-12">
            <li>
                <asp:HyperLink runat="server" ID="linkNotSend" Text="Ikke Sendt" /></li>
            <li>
                <asp:HyperLink runat="server" ID="linkSend" Text="Sendt" /></li>
            <li>
                <asp:HyperLink runat="server" ID="linkAll" Text="Alle" /></li>
        </ul>
    </div>


    <div class="listview column-12">
        <asp:Literal ID="litWaivesList" runat="server" />
    </div>

    <div class="modal create waive">
        <ul class="modal_tabs">
            <li class="selected">
                <asp:Literal runat="server" Text="<%$ Resources:WaiveTexts, ModalCreateWaiveHeader %>" />
            </li>
            <li>
                <asp:Literal runat="server" Text="<%$ Resources:WaiveTexts, ModalAddExtraHeader %>" />
            </li>
        </ul>
        <div class="tab-content">
            <div class="tab">
                <%--       <div class="row">
                    <div class="label">

                        <asp:Label runat="server" AssociatedControlID="ddlWaiveContract" ID="lblWaiveContract" Text="<%$ Resources:WaiveTexts, ModalLabelWaiveContract %>" />
                    </div>
                    <div class="input">
                        <asp:DropDownList runat="server" ID="ddlWaiveContract" />
                    </div>
                </div>--%>
                <div class="row">
                    <div class="label">
                        <asp:Label runat="server" AssociatedControlID="txtTitle" ID="lblTitle" Text="<%$ Resources:WaiveTexts, ModalLabelTitle %>" />
                    </div>
                    <div class="input">
                        <asp:TextBox runat="server" ID="txtTitle" />
                    </div>
                </div>
                <div class="row">
                    <div class="label">
                        <asp:Label runat="server" AssociatedControlID="ddlMilestone" ID="lblMilestone" Text="<%$ Resources:WaiveTexts, ModalLabelMilestone %>" />
                    </div>
                    <div class="input">
                        <asp:DropDownList runat="server" CssClass="datepicker" ID="ddlMilestone" />
                    </div>
                </div>
                <div class="row">
                    <div class="label">
                        <asp:Label runat="server" AssociatedControlID="txtWeeksFromMilestoneStart" ID="Label4" Text="<%$ Resources:WaiveTexts, ModalLabelWeeksFromMilestoneStart %>" />
                    </div>
                    <div class="input">
                        <asp:TextBox runat="server" ID="txtWeeksFromMilestoneStart" />
                    </div>
                </div>
                <div class="buttons">
                    <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button1" Text="<%$ Resources:Global, BtnCancelText %>" />
                    <asp:Button runat="server" CssClass="btn ui-create" ID="btnCreateWaive" OnClick="btnCreateWaive_Click" Text="<%$ Resources:WaiveTexts, ModalBtnCreate %>" />
                </div>
            </div>

            <div class="tab">
                <div class="row">
                    <div class="label">

                        <asp:Label runat="server" AssociatedControlID="ddlEkstraActivity" ID="Label1" Text="<%$ Resources:WaiveTexts, ModalLabelWaiveActivity %>" />
                    </div>
                    <div class="input">
                        <asp:DropDownList runat="server" ID="ddlEkstraActivity" />
                    </div>
                </div>
                <div class="row">
                    <div class="label">
                        <asp:Label runat="server" AssociatedControlID="txtEkstraTitle" ID="Label2" Text="<%$ Resources:WaiveTexts, ModalLabelTitle %>" />
                    </div>
                    <div class="input">
                        <asp:TextBox runat="server" ID="txtEkstraTitle" />
                    </div>
                </div>
                <div class="row">
                    <div class="label">
                        <asp:Label runat="server" AssociatedControlID="ddlMilestoneExtra" ID="Label3" Text="<%$ Resources:WaiveTexts, ModalLabelMilestone %>" />
                    </div>
                    <div class="input">
                        <asp:DropDownList runat="server" CssClass="datepicker" ID="ddlMilestoneExtra" />
                    </div>
                </div>
                <div class="buttons">
                    <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button2" Text="<%$ Resources:Global, BtnCancelText %>" />
                    <asp:Button runat="server" CssClass="btn ui-create" ID="btnAddEkstra" OnClick="btnAddEkstra_Click" Text="<%$ Resources:WaiveTexts, ModalAddEkstra %>" />
                </div>
            </div>
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hidWaiveID" />
    <div class="modal delete delete_waive">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:WaiveTexts, ModalDeleteWaive %>" />
        </span>

        <div class="row">
            <div class="text"><span class="waive_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="btnCancel" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-delete" ID="btnDeleteWaive" OnClick="btnDeleteWaive_Click" Text="<%$ Resources:WaiveTexts, ModalBtnDeleteWaive %>" />
        </div>
    </div>

</asp:Content>

