using Juinti.Case;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PartPage : System.Web.UI.Page
{
    long partID;
    long caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        partID = Convert.ToInt64(Request.QueryString["partid"]);
        caseID = Convert.ToInt64(Request.QueryString["caseID"]);

        Part part = new Part(partID);
        litHeader.Text = part.Title;

        //panButtons.Visible = !part.isUsedInPackage();                 

        if (!Page.IsPostBack)
        {
            FillDdlMaterials();   
            FillDdlActivities(part);
            txtPartTitle.Text = part.Title;
        }

        CreateMaterialsList();
    }

    private void FillDdlActivities(Part part)
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
        ddlPartStructure.SelectedValue = part.StructureID.ToString();
    }
      

    private void FillDdlMaterials()
    {
        MaterialCollection materials = Material.Utils.GetMaterialsByCaseID(caseID);
        ddlMaterials.Items.Clear();
        foreach (var item in materials)
        {
            ddlMaterials.Items.Add(new ListItem(item.Title, item.ID.ToString()));
        }
    }

    private void CreateMaterialsList()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                MaterialCollection materials = Material.Utils.GetMaterialsByPartID(partID);

                writer.AddAttribute("class", "listview");
                writer.AddAttribute("cellspacing", "0");
                writer.AddAttribute("cellpadding", "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                writer.AddAttribute("class", "title");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.MaterialTexts.ListHeaderTitle);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "unit");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.MaterialTexts.ListHeaderUnit);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "delete");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.Global.ListHeaderButtons);
                writer.RenderEndTag(); // Th

                writer.RenderEndTag(); // Tr
                foreach (var material in materials)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(material.Title);
                    writer.RenderEndTag(); // Td
                    writer.AddAttribute("class", "unit");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    MaterialUnit unit = new MaterialUnit(material.UnitID);
                    writer.Write(unit.Title);
                    writer.RenderEndTag(); // Td
                    writer.AddAttribute("class", "delete");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("class", "fa fa fa-times remove");
                    writer.AddAttribute("onclick", "return removeMaterial(" + material.ID + ", '" + material.Title + "');");
                    writer.RenderBeginTag(HtmlTextWriterTag.I);
                    writer.RenderEndTag(); // I
                    writer.RenderEndTag(); // Td
                    writer.RenderEndTag(); //Tr
                }

                writer.RenderEndTag(); // Table

                litMaterialList.Text = sw.ToString();
            }
        }
    }

    protected void btnAddMaterial_Click(object sender, EventArgs e)
    {
        Part part = new Part(partID);
        part.AddMaterial(Convert.ToInt64(ddlMaterials.SelectedValue));

        Response.Redirect(Request.RawUrl);
    }

    protected void btnRemoveMaterial_Click(object sender, EventArgs e)
    {
        Part part = new Part(partID);
        part.RemoveMaterial(Convert.ToInt64(hidMaterialID.Value));

        Response.Redirect(Request.RawUrl);
    }

    protected void btnSavePart_Click(object sender, EventArgs e)
    {
        Part part = new Part(partID);
        part.Title = txtPartTitle.Text;
        part.StructureID = Convert.ToInt64(ddlPartStructure.SelectedValue);
        part.Save();

        Response.Redirect(Request.RawUrl);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}