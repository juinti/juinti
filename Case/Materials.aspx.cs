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
using Juinti.Contacts;

public partial class MaterialsPage : System.Web.UI.Page
{
    Int64 caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        caseID = Convert.ToInt64(Request.QueryString["caseID"]);

        litHeader.Text = Resources.MaterialTexts.Materials;

        CreateMaterialsList();

        if (!Page.IsPostBack)
        {
            FillDdlMaterialUnits();
            FillDdlMaterialType();
            FillDdlCompany();
        }
    }


    private void FillDdlCompany()
    {
        CompanyCollection companies = Company.Utils.GetCompanies();
        foreach (var item in companies)
        {
            ddlCompany.Items.Add(new ListItem(item.Name, item.ID.ToString()));

        }
    }

    private void FillDdlMaterialType()
    {
        ddlMaterialType.Items.Clear();
        ddlMaterialType.Items.Add(new ListItem(Resources.MaterialTexts.MaterialTypePackage, "1"));
        ddlMaterialType.Items.Add(new ListItem(Resources.MaterialTexts.MaterialTypeStock, "2"));
        ddlMaterialType.Items.Add(new ListItem(Resources.MaterialTexts.MaterialTypePreStock, "3"));
    }

    private void FillDdlMaterialUnits()
    {
        MaterialUnitCollection materialUnits = MaterialUnit.Utils.GetMaterialUnitsByLanguageISO("da-DK");
        ddlMaterialUnit.Items.Clear();
        foreach (var item in materialUnits)
        {
            ddlMaterialUnit.Items.Add(new ListItem(item.Title, item.ID.ToString()));
        }
    }

    private void CreateMaterialsList()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                MaterialCollection materials = Material.Utils.GetMaterialsByCaseID(caseID);

                writer.AddAttribute("class", "listview");
                writer.AddAttribute("cellspacing", "0");
                writer.AddAttribute("cellpadding", "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);


                writer.AddAttribute("class", "number");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write("Varenummer");
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "title");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.MaterialTexts.ListHeaderTitle);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "unit");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.MaterialTexts.ListHeaderUnit);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "Type");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.MaterialTexts.ListHeaderType);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "price");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.MaterialTexts.ListHeaderPricePerUnit);
                writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "delete");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.Global.ListHeaderButtons);
                writer.RenderEndTag(); // Th

                writer.RenderEndTag(); //Tr

                foreach (var material in materials)
                {

                    string title = material.Title;

                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                    writer.AddAttribute("class", "number");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(material.Number);
                    writer.RenderEndTag(); // Td


                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("href", Urls.MaterialUrl + "?caseid=" + caseID + "&materialid=" + material.ID + "&pagetype=material");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(title);
                    writer.RenderEndTag(); // A
                    writer.RenderEndTag(); // Td

                    MaterialUnit materialUnit = new MaterialUnit(material.UnitID);

                    writer.AddAttribute("class", "unit");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(materialUnit.Title);
                    writer.RenderEndTag(); // Td

                    writer.AddAttribute("class", "type");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (material.TypeID == 1)
                    {
                        writer.Write(Resources.MaterialTexts.MaterialTypePackage);
                    }
                    else if (material.TypeID == 2)
                    {
                        writer.Write(Resources.MaterialTexts.MaterialTypeStock);
                    }
                    else if (material.TypeID == 3)
                    {
                        writer.Write(Resources.MaterialTexts.MaterialTypePreStock);
                    }
                    writer.RenderEndTag(); // Td


                    writer.AddAttribute("class", "price");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(material.Price.Price.ToString("N2") + " " + Resources.Global.CurrencyDisplay);
                    writer.RenderEndTag(); // Td

                    writer.AddAttribute("class", "delete");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    //writer.AddAttribute("class", "fa fa-pencil edit");
                    //writer.AddAttribute("onclick", "return editMaterial(" + material.ID + ", '" + material.Title + "', " + material.UnitID + ", " + material.TypeID + ");");
                    //writer.RenderBeginTag(HtmlTextWriterTag.I);
                    //writer.RenderEndTag(); // I
                    writer.AddAttribute("class", "fa fa fa-times remove");
                    writer.AddAttribute("onclick", "return deleteMaterial(" + material.ID + ", '" + material.Title + "');");
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

    protected void btnCreateMaterial_Click(object sender, EventArgs e)
    {
        Material material = new Material();
        material.UnitID = Convert.ToInt32(ddlMaterialUnit.SelectedValue);
        material.Title = txtMaterialTitle.Text;
        material.CaseID = caseID;
        material.TypeID = Convert.ToInt32(ddlMaterialType.SelectedValue);
        material.CompanyID = Convert.ToInt64(ddlCompany.SelectedValue);
        if (material.Save())
        {
            MaterialPrice price = new MaterialPrice();
            price.Price = Convert.ToDecimal(txtMaterialPrice.Text);
            price.MaterialID = material.ID;
            price.CaseID = caseID;
            price.CurrencyISO = "DKK";
            price.Save();
            Response.Redirect(Request.RawUrl);
        }
    }

    protected void btnDeleteMaterial_Click(object sender, EventArgs e)
    {
        Material.Utils.DeleteMaterial(Convert.ToInt64(hidMaterialID.Value));
        Response.Redirect(Request.RawUrl);
    }
}