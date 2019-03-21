using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Juinti.Case;
using Juinti.Variables;

public partial class PartsPage : System.Web.UI.Page
{
    Int64 caseID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        litHeader.Text = Resources.PartTexts.Parts;
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);
        if (!Page.IsPostBack)
        {
            FillDdlPartActivity();
        }

        CreateListActitvities();

    }

    private void FillDdlPartActivity()
    {
        ActivityCollection activities = Activity.Utils.GetActitvitiesByCaseID(caseID);

        foreach (var activity in activities)
        {           
            StructureCollection structures = Structure.Utils.GetStructuresByActivityID(activity.ID);
            foreach (var structure in structures)
            {
                Contract contract = new Contract(activity.ContractID);
                ListItem listItem = new ListItem();
                listItem.Text = structure.Title;
                listItem.Value = structure.ID.ToString();
                listItem.Attributes["data-optiongroup"] = contract.Title + " - " + activity.Title;
                ddlPartStructure.Items.Add(listItem);
            }

           
        }
    }

    private void CreateListActitvities()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {

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
                writer.Write(Resources.PartTexts.ListHeaderTitle);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "activity");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.PartTexts.ListHeaderStructure);
                writer.RenderEndTag(); // Th


                writer.AddAttribute("class", "delete");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.Global.ListHeaderButtons);
                writer.RenderEndTag(); // Th
                writer.RenderEndTag(); //Tr
                PartCollection parts = Part.Utils.GetPartsByCaseID(caseID);


                writer.AddAttribute("class", "listview inner");
                writer.AddAttribute("cellspacing", "0");
                writer.AddAttribute("cellpadding", "0");
                foreach (var part in parts)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                    writer.AddAttribute("class", "id dimmed");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(part.ID);
                    writer.RenderEndTag(); // Td

                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("href", Urls.PartUrl + "?caseid=" + caseID + "&partid=" + part.ID + "&pagetype=part");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(part.Title);
                    writer.RenderEndTag(); // Span
                    writer.RenderEndTag(); // A
                    writer.RenderEndTag(); // Td

                    Structure structure = new Structure(part.StructureID);
                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(structure.Title);
                    writer.RenderEndTag(); // Td

                    writer.AddAttribute("class", "delete");

                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (!part.isUsedInPackage())
                    {
                        writer.AddAttribute("class", "fa fa fa-times remove");
                        writer.AddAttribute("onclick", "return deletePart(" + part.ID + "," + part.ActivityID + ", '" + part.Title + "');");
                        writer.RenderBeginTag(HtmlTextWriterTag.I);
                        writer.RenderEndTag(); // I                           
                    }

                    writer.RenderEndTag(); // Td

                    writer.RenderEndTag(); // Tr
                }
                writer.RenderEndTag(); // Table
            }

            litlistActivities.Text = sw.ToString();
        }
    }


    protected void btnCreatePart_Click(object sender, EventArgs e)
    {
        Part part = new Part();
        part.StructureID = Convert.ToInt64(ddlPartStructure.SelectedValue);
        part.CaseID = caseID;
        part.Title = txtPartTitle.Text;
        part.Save();
        Response.Redirect(Request.RawUrl);
    }

    protected void btnDeletePart_Click(object sender, EventArgs e)
    {
        Part.Utils.DeletePart(Convert.ToInt64(hidPartID.Value), Convert.ToInt64(hidActivityID.Value));
        Response.Redirect(Request.RawUrl);
    }
}