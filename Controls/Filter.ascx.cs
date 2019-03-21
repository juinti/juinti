using Juinti.Case;
using Juinti.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_Filter : System.Web.UI.UserControl
{
    Int64 caseID = 0;
    string pageType = "";
    string filtertext = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        caseID = Convert.ToInt64(Request.QueryString["caseid"]);
        pageType = Request.QueryString["pagetype"];
        filtertext = Request.QueryString["filtertext"];

        if (!Page.IsPostBack)
        {
            FillDdlWaiveContracts(caseID);
            FillDdlMilestones(caseID);
        }
    }



    private void FillDdlWaiveContracts(long caseID)
    {
        ContractCollection contracts = Contract.Utils.GetContractsByCaseID(caseID);

        long filterContractID = Convert.ToInt64(Request.QueryString[FilterQueries.filterContract]);

        ddlContracts.Items.Add(new ListItem(Resources.Global.FilterAll, "0"));
        foreach (var item in contracts)
        {
            ddlContracts.Items.Add(new ListItem(item.Title, item.ID.ToString()));
        }

        ddlContracts.SelectedValue = filterContractID.ToString();
    }


    private void FillDdlMilestones(long caseID)
    {
        MilestoneCollection contracts = Milestone.Utils.GetMilestonesByCaseID(caseID);

        long filterMilestoneID = Convert.ToInt64(Request.QueryString[FilterQueries.filterMilestone]);

        ddlMilestones.Items.Add(new ListItem(Resources.Global.FilterAll, "0"));
        foreach (var item in contracts)
        {
            ddlMilestones.Items.Add(new ListItem(item.Title, item.ID.ToString()));
        }

        ddlMilestones.SelectedValue = filterMilestoneID.ToString();
    }



    protected void btnAddFilter_Click(object sender, EventArgs e)
    {



        long contractID = Convert.ToInt64(ddlContracts.SelectedValue);
        long milestoneID = Convert.ToInt64(ddlMilestones.SelectedValue);

        StringBuilder sb = new StringBuilder();

        if (contractID > 0)
        {
            sb.Append("&" + FilterQueries.filterContract + "=" + contractID.ToString());
        }
        if (milestoneID > 0)
        {
            sb.Append("&" + FilterQueries.filterMilestone + "=" + milestoneID.ToString());
        }





        Response.Redirect(Urls.WaivesUrl + "?caseid=" + caseID + "&pagetype=" + pageType + sb.ToString());

    }


}