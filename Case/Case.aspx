<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="case.aspx.cs" Inherits="_Case" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="top_view clearfix column-12 hastabs">
        <h1 class="column-6"><i class="fa fa-home"></i>
            Overblik
        </h1>

        <div class="topbuttons column-6">
            <div class="buttons_item selected">
                <%--<asp:LinkButton CssClass="btn_open_modal_delete btn btn ui-delete" OnClientClick="return DeleteCase();" Text="<%$ Resources:CaseTexts, BtnDeleteCase %>" runat="server" />--%>
                <span class="fa-stack btn_open_editbox">
                    <i class="fa fa-circle fa-stack-2x"></i>
                    <i class="fa fa-pencil fa-stack-1x fa-inverse"></i>
                </span>
            </div>
            <div class="buttons_item">
                <i class="fa fa-plus-circle open_create_modal milestone" onclick="openModalCreateActivity();" title=""></i>
            </div>
            <div class="buttons_item">
                <i class="fa fa-plus-circle open_create_modal milestone" onclick="openModalCreateMilestone();" title=""></i>
            </div>
        </div>
        <ul class="tabs-menu clearfix column-12">
            <li><span>Detaljer</span></li>
            <li><span>Arbejde</span></li>
            <li><span>Milepæle</span></li>
            <li>
                <asp:HyperLink runat="server" ID="linkDeliveryPlan" Text="Mængdeudtræk" /></li>
        </ul>
    </div>



    <div class="column-12 listview">
        <div class="tab selected">
            <table class="listview details">
                <tr>
                    <th>
                        <asp:Literal ID="litDetailsLabelCaseNumber" Text="<%$ Resources:CaseTexts, DetailsLabelCaseNumber %>" runat="server" />
                    </th>
                    <td>
                        <asp:Literal ID="litDetailsLabelCaseNumberValue" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="litDetailsLabelAddress" Text="<%$ Resources:CaseTexts, DetailsLabelAddress %>" runat="server" />
                    </th>
                    <td>
                        <asp:Literal ID="litDetailsLabelAddressValue" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="litDetailsLabelStartDate" Text="<%$ Resources:CaseTexts, DetailsLabelStartDate %>" runat="server" />
                    </th>
                    <td>
                        <asp:Literal ID="litDetailsLabelStartDateValue" runat="server" />
                    </td>
                </tr>
                <tr>
                    <th>
                        <asp:Literal ID="litDetailsLabelEndDate" Text="<%$ Resources:CaseTexts, DetailsLabelEndDate %>" runat="server" />
                    </th>
                    <td>
                        <asp:Literal ID="litDetailsLabelEndDateValue" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="tab">
            <asp:Literal ID="litListActivities" runat="server" />
        </div>
        <div class="tab">
            <asp:Literal ID="litListMilestones" runat="server" />
        </div>
    </div>

    <div class="modal create milestone">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:MilestoneTexts, ModalCreateMilestoneHeader %>" />
        </span>

        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMilestoneTitle" ID="lblMilestoneTitle" Text="<%$ Resources:MilestoneTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtMilestoneTitle" />
            </div>
        </div>
        <div class="row datestart">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMilestoneDateStart" ID="lblMilestoneDateStart" Text="<%$ Resources:MilestoneTexts, ModalLabelDateStart %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" autocomplete="off" CssClass="datepicker" ID="txtMilestoneDateStart" />
            </div>
        </div>
        <div class="row datend">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtMilestoneDateEnd" ID="lblMilestoneDateEnd" Text="<%$ Resources:MilestoneTexts, ModalLabelDateEnd %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" CssClass="datepicker" autocomplete="off" ID="txtMilestoneDateEnd" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button1" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnCreateMilestone" OnClick="btnCreateMilestone_Click" Text="<%$ Resources:Global, btnCreate %>" />
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hidMilestoneID" />

    <div class="modal delete delete_milestone">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:MilestoneTexts, ModalRemoveMilestoneText %>" />
        </span>

        <div class="row">
            <div class="text"><span class="milestone_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button3" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-delete" ID="btnDeleteMilestone" OnClick="btnDeleteMilestone_Click" Text="<%$ Resources:Global, btnDelete %>" />
        </div>
    </div>

    <div class="modal create activity">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:ActivityTexts, ModalCreateActivityHeader %>" />
        </span>

        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtActivityTitle" ID="lblActivityTitle" Text="<%$ Resources:ActivityTexts, ModalLabelTitle %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtActivityTitle" />
            </div>
        </div>
        <div class="row contract">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="ddlActivityContract" ID="Label2" Text="<%$ Resources:ActivityTexts, ModalLabelContract %>" />
                <i class="fa fa-plus-circle create_new" onclick="return modalCreateSwitch(this);"></i>
            </div>
            <div class="input">
                <asp:DropDownList runat="server" ID="ddlActivityContract" />
                <asp:TextBox CssClass="crete_new_input" ID="txtContractTitle" runat="server" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button4" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-create" ID="btnCreateActivity" OnClick="btnCreateActivity_Click" Text="<%$ Resources:Global, btnCreate %>" />
        </div>
    </div>

    <asp:HiddenField runat="server" ID="hidActivityID" />

    <div class="modal delete delete_activity">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:ActivityTexts, ModalRemoveActivityText %>" />
        </span>

        <div class="row">
            <div class="text"><span class="activity_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button5" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-delete" ID="btnDeleteActivity" OnClick="btnDeleteActivity_Click" Text="<%$ Resources:Global, btnDelete %>" />
        </div>
    </div>



    <div class="modal editbox case">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:CaseTexts, ModalEditCaseHeader %>" />
        </span>

        <div class="row name">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtCaseName" ID="lblCaseName" Text="<%$ Resources:CaseTexts, ModalLabelName %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtCaseName" />
            </div>
        </div>
        <div class="row number">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtCaseNumber" ID="lblCaseNumber" Text="<%$ Resources:CaseTexts, ModalLabelNumber %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtCaseNumber" />
            </div>
        </div>
        <div class="row street">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtCaseStreet" ID="lblCaseStreet" Text="<%$ Resources:CaseTexts, ModalLabelStreet %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtCaseStreet" />
            </div>
        </div>
        <div class="row zipcode">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtCaseZipCode" ID="lblCaseZipCode" Text="<%$ Resources:CaseTexts, ModalLabelZipCode %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtCaseZipCode" />
            </div>
        </div>
        <div class="row city">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtCaseCity" ID="lblCaseCity" Text="<%$ Resources:CaseTexts, ModalLabelCity %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtCaseCity" />
            </div>
        </div>
        <div class="row country">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtCaseCountry" ID="lblCaseCountry" Text="<%$ Resources:CaseTexts, ModalLabelCountry %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" ID="txtCaseCountry" />
            </div>
        </div>
        <div class="row datestart">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtCaseDateStart" ID="lblCaseDateStart" Text="<%$ Resources:CaseTexts, ModalLabelDateStart %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" autocomplete="off" CssClass="datepicker" ID="txtCaseDateStart" />
            </div>
        </div>
        <div class="row datend">
            <div class="label">
                <asp:Label runat="server" AssociatedControlID="txtCaseDateEnd" ID="lblCaseDateEnd" Text="<%$ Resources:CaseTexts, ModalLabelDateEnd %>" />
            </div>
            <div class="input">
                <asp:TextBox runat="server" CssClass="datepicker" autocomplete="off" ID="txtCaseDateEnd" />
            </div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="Button2" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button ID="btnSaveCase" CssClass="btn ui-save" OnClick="btnSaveCase_Click" ValidationGroup="grpCreateCase" Text="<%$ Resources:CaseTexts, ModalBtnSave %>" runat="server" />
        </div>
    </div>


    <div class="modal delete delete_case">
        <span class="header">
            <asp:Literal runat="server" Text="<%$ Resources:CaseTexts, ModalDeleteCase %>" />
        </span>

        <div class="row">
            <div class="text"><span class="case_title"></span></div>
        </div>
        <div class="buttons">
            <asp:Button runat="server" CssClass="btn ui-cancel modal_cancel" ID="btnCancel" Text="<%$ Resources:Global, BtnCancelText %>" />
            <asp:Button runat="server" CssClass="btn ui-delete" ID="btnDeleteCase" OnClick="btnDeleteCase_Click" Text="<%$ Resources:CaseTexts, ModalBtnDeleteCase %>" />
        </div>
    </div>
</asp:Content>

