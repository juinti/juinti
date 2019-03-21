<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Filter.ascx.cs" Inherits="Controls_Filter" %>




<%-- <span class="fa-stack btn_open_modal_filter">
                <i class="fa fa-circle fa-stack-2x"></i>
                <i class="fa fa-filter fa-stack-1x fa-inverse"></i>
            </span>--%>


<div class="filter_search">



    <div class="filter contracts">
        <span class="filter_title">
            <asp:Literal Text="<%$ Resources:WaiveTexts, FilterContract %>" runat="server" />
        </span>
        <div class="control">
            <asp:DropDownList runat="server" ID="ddlContracts" />
        </div>
    </div>

    <div class="filter milestones">
        <span class="filter_title">
            <asp:Literal Text="<%$ Resources:WaiveTexts, FilterMilestone %>" runat="server" />
        </span>
        <div class="control">
            <asp:DropDownList runat="server" ID="ddlMilestones" />
        </div>
    </div>

    <%-- <div class="filter date_range">
            <span class="filter_title">
                <asp:Literal Text="<%$ Resources:WaiveTexts, FilterDeliveryDateRange %>" runat="server" />
            </span>
            <div class="control">
                <div class="from">
                    <asp:Label runat="server" ID="lblDateFrom" AssociatedControlID="txtFilterDateFrom" Text="<%$ Resources:WaiveTexts, FilterDateFrom %>" />
                    <asp:TextBox ID="txtFilterDateFrom" CssClass="#from" runat="server" />
                </div>
                <div class="to">
                    <asp:Label runat="server" ID="lblDateTo" AssociatedControlID="txtFilterDateTo" Text="<%$ Resources:WaiveTexts, FilterDateTo %>" />
                    <asp:TextBox ID="txtFilterDateTo" CssClass="#to" runat="server" />
                </div>
            </div>
        </div>--%>
    <asp:LinkButton ID="btnAddFilter" Text="Filter"  OnClick="btnAddFilter_Click" runat="server">
      
    </asp:LinkButton>
</div>
