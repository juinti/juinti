using Juinti.Case;
using Juinti.Contacts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Case_DeliveryPlan : System.Web.UI.Page
{
    Int64 caseID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);
        if (!Page.IsPostBack)
        {
            FillDdlCompany();
        }
    }

    private void FillDdlCompany()
    {
        CompanyCollection companies = Company.Utils.GetCompanies();
        ddlCompany.Items.Add(new ListItem(Resources.DeliveryPlanTexts.ChooseCompany, "-1"));
        foreach (var item in companies)
        {
            ddlCompany.Items.Add(new ListItem(item.Name, item.ID.ToString()));
        }
    }

    private void RenderList(Int64 CaseID, Int64 CompanyID, DateTime DateFrom, DateTime DateTo, Int64 MaterialID)
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                decimal TotalAmountOverTime = 0;
                decimal TotalMetersOverTime = 0;
                decimal TotalSquareMetersOverTime = 0;
                bool singleIsLength = false;
                bool singleIsSquare = false;
                WaivePackageStockMaterialCollection packageStockaterials = WaivePackageStockMaterial.Utils.GetStockWaiveMaterialsByWaiveID(caseID, CompanyID, DateFrom, DateTo, MaterialID);
                if (packageStockaterials.Count() > 0)
                {
                    Int64 prevID = 0;
                    Int64 currID = 0;
                    DateTime currDate = DateTime.MinValue;


                    foreach (var material in packageStockaterials)
                    {
                        if (currDate.Date != material.EstOrderDate.Date)
                        {
                            if (currDate != DateTime.MinValue || prevID != currID)
                            {
                                writer.RenderEndTag(); // Table
                            }

                            writer.AddAttribute("style", "margin-bottom:10px ;margin-top:40px;");
                            writer.RenderBeginTag(HtmlTextWriterTag.H3);
                            writer.Write("<span style='font-weight:900; font-size:14px; background:yellow;'>" + material.EstOrderDate.Date.ToShortDateString() + "</span>");
                            writer.RenderEndTag(); // H3

                            writer.AddAttribute("cellspacing", "0");
                            writer.AddAttribute("cellpadding", "0");
                            writer.AddAttribute("style", "width:100%;");
                            writer.AddAttribute("data-tag", material.EstOrderDate.Date.ToShortDateString());
                            writer.RenderBeginTag(HtmlTextWriterTag.Table);
                            writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                            writer.AddAttribute("style", "font-weight:bold; text-align:left; padding:5px; border-bottom:1px solid #000;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Th);
                            writer.Write("Beskrivelse");
                            writer.RenderEndTag(); // Th


                            writer.AddAttribute("style", "text-align:right; padding:5px; width:30%;  border-bottom:1px solid #000;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Th);
                            writer.Write(Resources.PackageTexts.Amount);
                            writer.RenderEndTag(); // Th
                            writer.RenderEndTag(); // Tr                             

                            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                            writer.AddAttribute("style", "width:100%;");
                            writer.AddAttribute("style", "text-align:left;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Th);
                            writer.Write("");
                            writer.RenderEndTag(); // Th 

                            writer.AddAttribute("style", "text-align:right;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Th);
                            writer.Write("");
                            writer.RenderEndTag(); // Th               
                            writer.RenderEndTag(); // tr

                        }
                        currID = material.MaterialID;


                        bool isLength = material.UnitTypeID == 1 ? true : false;
                        bool isSquare = material.UnitTypeID == 2 ? true : false;

                        decimal length = 0;
                        decimal square = 0;


                        if (MaterialID > 0)
                        {
                            TotalAmountOverTime += material.TotalAmount;
                        }

                        if (isLength || isSquare)
                        {

                            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                            writer.AddAttribute("style", "padding-left:5px;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write("<span style='font-weight:bold;'>" + material.Title + "</span> ");

                            if (isLength)
                            {
                                writer.Write(" - " + material.Length.ToString("0") + "mm");
                                length = (material.Length * material.TotalAmount) / 1000;
                                TotalMetersOverTime += (material.Length * material.TotalAmount) / 1000;
                                singleIsLength = true;

                            }
                            else if (isSquare)
                            {
                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                writer.Write(" - " + material.Length.ToString("0") + "mm");
                                writer.RenderEndTag();
                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                writer.Write(" x ");
                                writer.RenderEndTag();
                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                writer.Write(material.Width.ToString("0") + "mm");
                                writer.RenderEndTag();
                                square = (material.Width * material.Length * material.TotalAmount) / 1000000;
                                TotalSquareMetersOverTime += (material.Width * material.Length * material.TotalAmount) / 1000000;
                                singleIsSquare = true;
                            }
                            writer.RenderEndTag(); // Td

                            writer.AddAttribute("style", "text-align:right; padding:5px; width:30%;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            if (isLength)
                            {
                                writer.Write("(" + length.ToString("N2") + " Mtr) " + material.TotalAmount.ToString("N2") + " stk.");
                            }
                            else if (isSquare)
                            {
                                writer.Write("(" + square.ToString("N2") + " M2) " + material.TotalAmount.ToString("N2") + " stk.");
                            }

                            writer.RenderEndTag(); // Td
                            writer.RenderEndTag(); // Tr 


                        }
                        else
                        {

                            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                            writer.AddAttribute("style", "padding-left:5px;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write("<span style='font-weight:bold;'>" + material.Title + "</span> ");
                            writer.RenderEndTag(); // Td       
                            writer.AddAttribute("style", "text-align:right; padding-right:5px;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write(material.TotalAmount.ToString("N2") + " stk.");
                            writer.RenderEndTag(); // Td  
                            writer.RenderEndTag(); // Tr

                        }

                        prevID = material.MaterialID;

                        currDate = material.EstOrderDate;



                    }
                    if (packageStockaterials.Count() > 0)
                    {
                        writer.RenderEndTag(); // Table
                    }
                }
               


                if (MaterialID > 0)
                {
                    writer.AddAttribute("style", "text-align:right; padding-top:20px; text-align:right; font-weight:bold;");
                    writer.RenderBeginTag(HtmlTextWriterTag.P);
                    if (singleIsLength)
                    {
                        writer.Write("(" + TotalMetersOverTime.ToString("N2") + " Mtr) " + TotalAmountOverTime.ToString("N2") + " stk.");
                    }
                    else if (singleIsSquare)
                    {
                        writer.Write("(" + TotalSquareMetersOverTime.ToString("N2") + " M2) " + TotalAmountOverTime.ToString("N2") + " stk.");
                    }
                    else
                    {
                        writer.Write(TotalAmountOverTime.ToString("N2") + " stk.");

                    }
                    writer.RenderEndTag();
                }

            }

            litList.Text = sw.ToString();
        }
    }


    protected void btnGetList_Click(object sender, EventArgs e)
    {
        Int64 companyID = Convert.ToInt64(ddlCompany.SelectedValue);
        DateTime dateFrom = !String.IsNullOrEmpty(txtDateFrom.Text) ? Convert.ToDateTime(txtDateFrom.Text) : Convert.ToDateTime("1790-01-01 00:00:00");
        DateTime dateTo = !String.IsNullOrEmpty(txtDateTo.Text) ? Convert.ToDateTime(txtDateTo.Text) : DateTime.MaxValue.AddYears(-1);
        Int64 materialID = Convert.ToInt64(ddlMaterial.SelectedValue);
        RenderList(caseID, companyID, dateFrom, dateTo, materialID);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlMaterial.Items.Clear();
        MaterialCollection materials = Material.Utils.GetDDLMaterialsByCompanyID(Convert.ToInt32(ddlCompany.SelectedValue), caseID);
        ddlMaterial.Items.Add(new ListItem(Resources.DeliveryPlanTexts.Choose, "-1"));
        foreach (var item in materials)
        {
            ddlMaterial.Items.Add(new ListItem(item.Title, item.ID.ToString()));
        }
    }
}