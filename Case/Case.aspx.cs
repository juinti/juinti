using Juinti.User.Customer;
using Juinti.Case;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Juinti.Variables;

public partial class _Case : System.Web.UI.Page
{
    Int64 caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);
        Case newCase = new Case(caseID);

        linkDeliveryPlan.NavigateUrl = "~/case/deliveryplan.aspx" + "?caseid=" + caseID + "&pagetype=overview";

        if (!Page.IsPostBack)
        {
            FillValues(newCase);
            FillddlContract();
        }
        RenderListActivities();
        RenderListMilestones();

    }

    private void FillddlContract()
    {
        ContractCollection contracts = Contract.Utils.GetContractsByCaseID(caseID);
        foreach (var item in contracts)
        {
            ddlActivityContract.Items.Add(new ListItem(item.Title, item.ID.ToString()));
        }
    }

    private void RenderListActivities()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                ActivityCollection activities = Activity.Utils.GetActitvitiesByCaseID(caseID);

                writer.AddAttribute("class", "listview");
                writer.AddAttribute("cellspacing", "0");
                writer.AddAttribute("cellpadding", "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);              


                writer.AddAttribute("class", "title");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.MilestoneTexts.ListHeaderTitle);
                writer.RenderEndTag(); // Th


                writer.AddAttribute("class", "contract");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.ActivityTexts.ListHeaderContract);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "delete");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.Global.ListHeaderButtons);
                writer.RenderEndTag(); // Th
                writer.RenderEndTag(); //Tr
                foreach (var activity in activities)
                {

                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                 

                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("href", Urls.ActivityUrl + "?caseid=" + caseID + "&activityid=" + activity.ID + "&pagetype=overview");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(activity.Title);
                    writer.RenderEndTag(); // A


                    Contract contract = new Contract(activity.ContractID);
                    writer.AddAttribute("class", "contract");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("href", Urls.ContractUrl + "?caseid=" + caseID + "&contractid=" + contract.ID + "&pagetype=overview");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(contract.Title);
                    writer.RenderEndTag(); // A
                    writer.RenderEndTag(); // Td      


                    //writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                    //foreach (var item in Part.Utils.GetPartsByActivityID(activity.ID))
                    //{
                    //    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    //    writer.Write(item.Title);
                    //    writer.RenderEndTag();
                       
                       
                    //}
                    //writer.RenderEndTag();
                    writer.RenderEndTag(); // Td      


                 

                    writer.AddAttribute("class", "delete");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (!activity.IsInUse())
                    {
                        writer.AddAttribute("class", "fa fa fa-times remove");
                        writer.AddAttribute("onclick", "return deleteActivity(" + activity.ID + ", '" + activity.Title + "');");
                        writer.RenderBeginTag(HtmlTextWriterTag.I);
                        writer.RenderEndTag(); // I
                    }
                    writer.RenderEndTag(); // Td

                    writer.RenderEndTag(); // Tr

                }

                writer.RenderEndTag(); // Table
            }
            litListActivities.Text = sw.ToString();
        }
    }



    private void RenderListMilestones()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                MilestoneCollection milestones = Milestone.Utils.GetMilestonesByCaseID(caseID);
                writer.AddAttribute("class", "listview");
                writer.AddAttribute("cellspacing", "0");
                writer.AddAttribute("cellpadding", "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.AddAttribute("class", "title");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.MilestoneTexts.ListHeaderTitle);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "date from");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.MilestoneTexts.ListHeaderDateFrom);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "date to");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.MilestoneTexts.ListHeaderDateTo);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "delete");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.Global.ListHeaderButtons);
                writer.RenderEndTag(); // Th
                writer.RenderEndTag(); //Tr
                foreach (var milestone in milestones)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("href", Urls.MilestoneUrl + "?caseid=" + caseID + "&milestoneid=" + milestone.ID + "&pagetype=milestone");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(milestone.Title);
                    writer.RenderEndTag(); // A
                    writer.RenderEndTag(); // Td
                    writer.AddAttribute("class", "date start");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(milestone.DateStart.ToShortDateString());
                    writer.RenderEndTag(); // Td
                    writer.AddAttribute("class", "date end");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(milestone.DateEnd.ToShortDateString());
                    writer.RenderEndTag(); // Td


                    writer.AddAttribute("class", "delete");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("class", "fa fa fa-times remove");
                    writer.AddAttribute("onclick", "return deleteMilestone(" + milestone.ID + ", '" + milestone.Title + "');");
                    writer.RenderBeginTag(HtmlTextWriterTag.I);
                    writer.RenderEndTag(); // I
                    writer.RenderEndTag(); // Td

                    writer.RenderEndTag(); // Tr

                }

                writer.RenderEndTag(); // Table
            }
            litListMilestones.Text = sw.ToString();
        }
    }

    private void FillValues(Case newCase)
    {
        txtCaseName.Text = newCase.Name;

        txtCaseNumber.Text = newCase.Number;
        litDetailsLabelCaseNumberValue.Text = newCase.Number;

        txtCaseStreet.Text = newCase.Street;
        litDetailsLabelAddressValue.Text = newCase.Street + ",  " + newCase.ZipCode + " " + newCase.City + " - " + newCase.Country;

        txtCaseZipCode.Text = newCase.ZipCode;
        txtCaseCity.Text = newCase.City;
        txtCaseCountry.Text = newCase.Country;


        if (newCase.StartDate.HasValue)
        {
            txtCaseDateStart.Text = newCase.StartDate.Value.ToShortDateString();
            litDetailsLabelStartDateValue.Text = newCase.StartDate.Value.ToShortDateString();
        }
        if (newCase.EndDate.HasValue)
        {
            txtCaseDateEnd.Text = newCase.EndDate.Value.ToShortDateString();
            litDetailsLabelEndDateValue.Text = newCase.EndDate.Value.ToShortDateString();
        }

    }

    protected void btnSaveCase_Click(object sender, EventArgs e)
    {
        Case newCase = new Case(caseID);

        newCase.Name = txtCaseName.Text;
        newCase.Number = txtCaseNumber.Text;
        newCase.Street = txtCaseStreet.Text;
        newCase.ZipCode = txtCaseZipCode.Text;
        newCase.City = txtCaseCity.Text;
        newCase.Country = txtCaseCountry.Text;
        newCase.StartDate = Convert.ToDateTime(txtCaseDateStart.Text);
        newCase.EndDate = Convert.ToDateTime(txtCaseDateEnd.Text);
        newCase.Save();

        Response.Redirect(Request.RawUrl);
    }

    protected void btnDeleteCase_Click(object sender, EventArgs e)
    {
        Case newCase = new Case(caseID);
        newCase.Delete();
        Response.Redirect("~/pages/start.aspx");
    }

    protected void btnCreateMilestone_Click(object sender, EventArgs e)
    {
        Milestone milestone = new Milestone();
        milestone.Title = txtMilestoneTitle.Text;
        milestone.DateStart = Convert.ToDateTime(txtMilestoneDateStart.Text);
        milestone.DateEnd = Convert.ToDateTime(txtMilestoneDateEnd.Text);
        milestone.CaseID = caseID;
        milestone.Save();
        Response.Redirect(Request.RawUrl);
    }

    protected void btnDeleteMilestone_Click(object sender, EventArgs e)
    {
        Milestone.Utils.DeleteMilestone(Convert.ToInt64(hidMilestoneID.Value));
        Response.Redirect(Request.RawUrl);
    }

    protected void btnCreateActivity_Click(object sender, EventArgs e)
    {
        bool newContract = false;
        Int64 contractID = 0;

        if (!String.IsNullOrEmpty(txtContractTitle.Text))
        {
            newContract = true;
            Contract contract = new Contract();
            contract.Title = txtContractTitle.Text;
            contract.CaseID = caseID;
            contract.Save();
            contractID = contract.ID;
        }

        Activity activity = new Activity();
        activity.Title = txtActivityTitle.Text;
        activity.ContractID = newContract ? contractID : Convert.ToInt64(ddlActivityContract.SelectedValue);
        activity.CaseID = caseID;
        activity.Save();
        Response.Redirect(Request.RawUrl);
    }

    protected void btnDeleteActivity_Click(object sender, EventArgs e)
    {
        Activity.Utils.DeleteActivity(Convert.ToInt64(hidActivityID.Value));
        Response.Redirect(Request.RawUrl);
    }
}