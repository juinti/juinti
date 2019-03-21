using Juinti.Case;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MilestonePage : System.Web.UI.Page
{
    long milestoneID;
    long caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        milestoneID = Convert.ToInt64(Request.QueryString["milestoneid"]);
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);

        Milestone milestone = new Milestone(milestoneID);
        litHeader.Text = milestone.Title;

        if (!Page.IsPostBack)
        {
            FillValues(milestone);
        }

    }

    private void FillValues(Milestone material)
    {
        txtMilestoneTitle.Text = material.Title;
        txtMilestoneDateStart.Text = material.DateStart.ToShortDateString();
        litDetailsLabelDateFromValue.Text = material.DateStart.ToShortDateString();
        txtMilestoneDateEnd.Text = material.DateEnd.ToShortDateString();
        litDetailsLabelDateToValue.Text = material.DateEnd.ToShortDateString(); 
    }


    protected void btnSaveMilestone_Click(object sender, EventArgs e)
    {
        Milestone milestone = new Milestone(milestoneID);
        milestone.Title = txtMilestoneTitle.Text;
        milestone.DateStart = Convert.ToDateTime(txtMilestoneDateStart.Text);
        milestone.DateEnd = Convert.ToDateTime(txtMilestoneDateEnd.Text);
        milestone.CaseID = caseID;
        milestone.Save();

        Response.Redirect(Request.RawUrl);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}