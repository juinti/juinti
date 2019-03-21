using Juinti.Case;
using Juinti.Contacts;
using Juinti.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MaterialPage : System.Web.UI.Page
{
    Int64 materialID;
    Int64 caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        materialID = Convert.ToInt64(Request.QueryString["materialid"]);
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);

        Material material = new Material(materialID);
        litHeader.Text = material.Title;

        linkReport.NavigateUrl = Urls.MaterialReportUrl + "?caseid="+caseID+ "&materialid=" +materialID + "&pagetype=material"; 

        if (!Page.IsPostBack)
        {
            FillDdlMaterialUnits(material);
            FillDdlMaterialType(material);
            FillDdlCompany(material);
            FillValues(material);
        }

    }

    private void FillDdlCompany(Material material)
    {
        CompanyCollection companies = Company.Utils.GetCompanies();
        foreach (var item in companies)
        {
            ddlCompany.Items.Add(new ListItem(item.Name, item.ID.ToString()));

        }
        ddlCompany.SelectedValue = material.CompanyID.ToString();
    }

    private void FillValues(Material material)
    {
        txtMaterialTitle.Text = material.Title;
        txtMaterialPrice.Text = material.Price.Price.ToString("N2");
        txtMinStockAmount.Text = material.MinStockOrder.ToString("N2");
        txtMaterialNumber.Text = material.Number;

        litPartnumberDetails.Text = material.Number;

        MaterialUnit unit = new MaterialUnit(material.UnitID);

        litUnitDetails.Text = unit.Title;

        litPriceUnitDetails.Text = material.Price.Price.ToString("N2");

        string typeTxt = "";

        switch (material.TypeID)
        {
            case 1:
                typeTxt = "Pakke";
                break;
            case 2:
                typeTxt = "Lager";
                break;
            case 3:
                typeTxt = "Mellemlager";
                break;
            default:
                break;
        }

        litTypeDetails.Text = typeTxt;

        litMinimumOrderDetails.Text = material.MinStockOrder.ToString("N2");
        litCompanyDetails.Text = new Company(material.CompanyID).Name;
    }

    private void FillDdlMaterialType(Material material)
    {
        ddlMaterialType.Items.Clear();
        ddlMaterialType.Items.Add(new ListItem(Resources.MaterialTexts.MaterialTypePackage, "1"));
        ddlMaterialType.Items.Add(new ListItem(Resources.MaterialTexts.MaterialTypeStock, "2"));
        ddlMaterialType.Items.Add(new ListItem(Resources.MaterialTexts.MaterialTypePreStock, "3"));
        ddlMaterialType.SelectedValue = material.TypeID.ToString();
    }

    private void FillDdlMaterialUnits(Material material)
    {
        MaterialUnitCollection materialUnits = MaterialUnit.Utils.GetMaterialUnitsByLanguageISO("da-DK");
        ddlMaterialUnit.Items.Clear();
        foreach (var item in materialUnits)
        {
            ddlMaterialUnit.Items.Add(new ListItem(item.Title, item.ID.ToString()));

        }
        ddlMaterialUnit.SelectedValue = material.UnitID.ToString();
    }

    protected void btnSaveMaterial_Click(object sender, EventArgs e)
    {
        Material material = new Material(materialID);
        material.UnitID = Convert.ToInt32(ddlMaterialUnit.SelectedValue);
        material.Title = txtMaterialTitle.Text;
        material.CaseID = caseID;
        material.TypeID = Convert.ToInt32(ddlMaterialType.SelectedValue);
        material.CompanyID = Convert.ToInt64(ddlCompany.SelectedValue);
        material.MinStockOrder = Convert.ToDecimal(txtMinStockAmount.Text);
        material.Number = txtMaterialNumber.Text;

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

        Response.Redirect(Request.RawUrl);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}