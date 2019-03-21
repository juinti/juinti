using Juinti.Case;
using Juinti.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Case_Activity : System.Web.UI.Page
{
    long activityID;
    long caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        activityID = Convert.ToInt64(Request.QueryString["activityid"]);
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);


        Activity activity = new Activity(activityID);
        Contract contract = new Contract(activity.ContractID);

        litDetailsLabelContractValue.Text = contract.Title;

        litHeader.Text = activity.Title;

        if (!Page.IsPostBack)
        {
            FillddlContracts();
            FillModalValues(activity, contract);
            RenderListStructures();
        }

    }

    private void FillddlContracts()
    {
        ContractCollection contracts = Contract.Utils.GetContractsByCaseID(caseID);
        foreach (var item in contracts)
        {
            ddlActivityContract.Items.Add(new ListItem(item.Title, item.ID.ToString()));
        }
    }

    private void FillModalValues(Activity activity, Contract contract)
    {
        txtActivityTitle.Text = activity.Title;
        ddlActivityContract.SelectedValue = activity.ContractID.ToString();
    }

    private void RenderListStructures()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                StructureCollection structures = Structure.Utils.GetStructuresByActivityID(activityID);
                writer.AddAttribute("class", "listview");
                writer.AddAttribute("cellspacing", "0");
                writer.AddAttribute("cellpadding", "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.AddAttribute("class", "title");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.MilestoneTexts.ListHeaderTitle);
                writer.RenderEndTag(); // Th


                writer.AddAttribute("class", "delete");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.Global.ListHeaderButtons);
                writer.RenderEndTag(); // Th
                writer.RenderEndTag(); //Tr
                foreach (var structure in structures)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("href", Urls.StructureUrl + "?caseid=" + caseID + "&structureid=" + structure.ID + "&pagetype=overview");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(structure.Title);
                    writer.RenderEndTag(); // A
                    writer.RenderEndTag(); // Td      
                    
                    writer.AddAttribute("class", "delete");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("class", "fa fa fa-times remove");
                    writer.AddAttribute("onclick", "return deleteStructure(" + structure.ID + ", '" + structure.Title + "');");
                    writer.RenderBeginTag(HtmlTextWriterTag.I);
                    writer.RenderEndTag(); // I

                    writer.RenderEndTag(); // Td

                    writer.RenderEndTag(); // Tr

                }

                writer.RenderEndTag(); // Table
            }
            litListStructures.Text = sw.ToString();
        }
    }

    protected void btnSaveActivity_Click(object sender, EventArgs e)
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

        Activity activity = new Activity(activityID);
        activity.Title = txtActivityTitle.Text;
        activity.ContractID = newContract ? contractID : Convert.ToInt64(ddlActivityContract.SelectedValue);
        activity.CaseID = caseID;
        activity.Save();
        Response.Redirect(Request.RawUrl);
    }

    protected void btnAddStructure_Click(object sender, EventArgs e)
    {
        Structure structure = new Structure();
        structure.CaseID = caseID;
        structure.ActivityID = activityID;
        structure.Title = txtStructureTitle.Text;
        structure.Save();
        Response.Redirect(Request.RawUrl);
    }

    protected void btnDeleteStructure_Click(object sender, EventArgs e)
    {
        Structure.Utils.DeleteStructure(Convert.ToInt64(hidStructureID.Value));
        Response.Redirect(Request.RawUrl);
    }
}