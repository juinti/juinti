using Juinti.Case;
using Juinti.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WaivesPage : System.Web.UI.Page
{
    Int64 caseID = 0;
    Int64 contractID = 0;
    int isOrdered = 1;

    protected void Page_Load(object sender, EventArgs e)
    {
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);
        contractID = Convert.ToInt64(Request.QueryString["contractid"]);
        isOrdered = Convert.ToInt32(Request.QueryString["isordered"]);



        linkNotSend.NavigateUrl = "~/case/waives.aspx?caseid=5&pagetype=waive&isordered=1#tabindex-0";
        linkSend.NavigateUrl = "~/case/waives.aspx?caseid=5&pagetype=waive&isordered=2#tabindex-1";
        linkAll.NavigateUrl = "~/case/waives.aspx?caseid=5&pagetype=waive&isordered=3#tabindex-2";
        

        CreateWaivesList();
        if (!Page.IsPostBack)
        {
            FillDdlWaiveActivities(caseID);
            FillDdlMilestones(caseID);
        }
    }

    private void FillDdlMilestones(long caseID)
    {
        MilestoneCollection contracts = Milestone.Utils.GetMilestonesByCaseID(caseID);

        long filterMilestoneID = Convert.ToInt64(Request.QueryString[FilterQueries.filterMilestone]);

        foreach (var item in contracts)
        {         
            ddlMilestone.Items.Add(new ListItem(item.Title, item.ID.ToString()));
            ddlMilestoneExtra.Items.Add(new ListItem(item.Title, item.ID.ToString()));
        }
      
    }

    private void CreateWaivesList()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                WaiveCollection waives;

                long filterContractID = Convert.ToInt64(Request.QueryString[FilterQueries.filterContract]);
                long filterMilestoneID = Convert.ToInt64(Request.QueryString[FilterQueries.filterMilestone]);
                string searchText = Request.QueryString[FilterQueries.filterText];


                Milestone milestone = new Milestone(filterMilestoneID);
                waives = Waive.Utils.GetWaives(caseID, searchText, filterContractID > 0 ? filterContractID : 0, filterMilestoneID > 0 ? milestone.ID : 0, null, null);



                writer.AddAttribute("class", "listview");
                writer.AddAttribute("cellspacing", "0");
                writer.AddAttribute("cellpadding", "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.AddAttribute("class", "id");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.PackageTexts.ListHeaderID);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "title");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.WaiveTexts.ListHeaderTitle);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "location");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.WaiveTexts.ListHeaderLocation);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "milestone");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.WaiveTexts.ListHeaderMilestone);
                writer.RenderEndTag(); // Th
                writer.AddAttribute("class", "date");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.WaiveTexts.ListHeaderEstOrderDate);
                writer.RenderEndTag(); // Th

                //writer.AddAttribute("class", "price");
                //writer.RenderBeginTag(HtmlTextWriterTag.Th);
                //writer.Write(Resources.WaiveTexts.ListHeaderPrice);
                //writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "delete");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.Global.ListHeaderButtons);
                writer.RenderEndTag(); // Th
                writer.RenderEndTag(); //Tr



                writer.AddAttribute("class", "listview inner");
                writer.AddAttribute("cellspacing", "0");
                writer.AddAttribute("cellpadding", "0");


                decimal totalPrice = 0;

                foreach (var waive in waives)
                {
                    //totalPrice += waive.GetTotalPrice();

                    if (isOrdered <= 1 && waive.IsOrdered || isOrdered == 2 && !waive.IsOrdered)
                        continue;
                  
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                    writer.AddAttribute("class", "id dimmed");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(waive.ID);
                    writer.RenderEndTag(); // Td
                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("href", Urls.WaiveUrl + "?caseid=" + caseID + "&contractid=" + contractID + "&waiveid=" + waive.ID + "&pagetype=waive" + "&extra=" + waive.IsExtra.ToString().ToLower());
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(waive.Title);
                    writer.RenderEndTag(); // A
                    writer.RenderEndTag(); // Td

                    writer.AddAttribute("class", "location");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(waive.Location);
                    writer.RenderEndTag(); // Td

                    Milestone waiveMilestone = new Milestone(waive.MilestoneID);
                    writer.AddAttribute("class", "milestone");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(waiveMilestone.Title);
                    writer.RenderEndTag(); // Td

                    writer.AddAttribute("class", "date");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (waive.EstOrderDate.Year != 9999)
                    {
                        writer.Write(waive.EstOrderDate.ToShortDateString() + " (" + Decimal.Floor(Convert.ToDecimal(waive.EstOrderDate.Subtract(waiveMilestone.DateStart).TotalDays/7)).ToString() + ")");
                    }
                    writer.RenderEndTag(); // Td

                    //writer.AddAttribute("class", "price");
                    //writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    //writer.Write(waive.GetTotalPrice().ToString("N2"));
                    //writer.RenderEndTag(); // Td

                    writer.AddAttribute("class", "delete");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (!waive.IsOrdered)
                    {
                        writer.AddAttribute("class", "fa fa fa-times remove");
                        writer.AddAttribute("onclick", "return deleteWaive(" + waive.ID + ", '" + waive.Title + "');");
                        writer.RenderBeginTag(HtmlTextWriterTag.I);
                        writer.RenderEndTag(); // I  
                    }                  
                    writer.RenderEndTag(); // Td

                    writer.RenderEndTag(); // Tr
                }
                writer.RenderEndTag(); // Table
                litWaivesList.Text = sw.ToString();
            }
        }
    }

    private void FillDdlWaiveActivities(long caseID)
    {
      
            ActivityCollection activities = Activity.Utils.GetActitvitiesByCaseID(caseID);

            foreach (var item in activities)
            {
                Contract contract = new Contract(item.ContractID);
                ListItem listItem = new ListItem(item.Title, item.ID.ToString());
                listItem.Attributes["data-optiongroup"] = contract.Title;

                ddlEkstraActivity.Items.Add(listItem);
            }
        

    }

    protected void btnCreateWaive_Click(object sender, EventArgs e)
    {
        Waive waive = new Waive();
        waive.CaseID = caseID;
        waive.ContractID = 0;
        waive.Title = txtTitle.Text;
        waive.IsExtra = false;
        waive.MilestoneID = Convert.ToInt64(ddlMilestone.SelectedValue);

        Milestone milestone = new Milestone(Convert.ToInt64(ddlMilestone.SelectedValue));


        if (!String.IsNullOrEmpty(txtWeeksFromMilestoneStart.Text) && Convert.ToInt32(txtWeeksFromMilestoneStart.Text) > 0)
        {
            waive.EstOrderDate = milestone.DateStart.AddDays(Convert.ToInt32(txtWeeksFromMilestoneStart.Text) * 7);
        }
     
        waive.Save();
        Response.Redirect(Urls.WaiveUrl + "?caseid=" + caseID + "&waiveid=" + waive.ID+ "&extra=false");

    }
    
    protected void btnAddEkstra_Click(object sender, EventArgs e)
    {
        Waive waive = new Waive();
        waive.CaseID = caseID;
        waive.ContractID = 0;
        waive.ActivityID = Convert.ToInt64(ddlEkstraActivity.SelectedValue);
        waive.Title = txtEkstraTitle.Text;
        waive.IsExtra = true;
        waive.MilestoneID = Convert.ToInt64(ddlMilestone.SelectedValue);

        Milestone milestone = new Milestone(Convert.ToInt64(ddlMilestone.SelectedValue));

        waive.Save();
        Response.Redirect(Urls.WaiveUrl + "?caseid=" + caseID + "&waiveid=" + waive.ID +"&extra=true");
    }

    protected void btnDeleteWaive_Click(object sender, EventArgs e)
    {
        Waive.Utils.DeleteWaive(Convert.ToInt64(hidWaiveID.Value));
        Response.Redirect(Request.RawUrl);
    }
}