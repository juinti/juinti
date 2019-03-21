using Juinti.Case;
using Juinti.Report;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Case_Report : System.Web.UI.Page
{
    long caseID;
    static Random random = new Random();
    protected void Page_Load(object sender, EventArgs e)
    {
        caseID = Convert.ToInt64(Request.QueryString["caseID"]);
        if (!Page.IsPostBack)
        {
            FillDDls();
        }
    }

    private void FillDDls()
    {
        ddlMaterial.Items.Clear();
        MaterialCollection materials = Material.Utils.GetMaterialsByCaseID(caseID);
        ddlMaterial.Items.Add(new ListItem(Resources.DeliveryPlanTexts.Choose, "-1"));
        foreach (var item in materials)
        {
            ddlMaterial.Items.Add(new ListItem(item.Title, item.ID.ToString()));
        }

        PartCollection parts = Part.Utils.GetPartsByCaseID(caseID);
        ddlPart.Items.Add(new ListItem(Resources.DeliveryPlanTexts.Choose, "-1"));
        foreach (var item in parts)
        {
            ddlPart.Items.Add(new ListItem(item.Title, item.ID.ToString()));
        }

        ActivityCollection activities = Activity.Utils.GetActitvitiesByCaseID(caseID);
        ddlActivity.Items.Add(new ListItem(Resources.DeliveryPlanTexts.Choose, "-1"));
        foreach (var item in activities)
        {
            Contract contract = new Contract(item.ContractID);
            ListItem listItem = new ListItem(item.Title, item.ID.ToString());
            listItem.Attributes["data-optiongroup"] = contract.Title;
            ddlActivity.Items.Add(listItem);
        }
    }

    private void RenderChartScript(MaterialReportDTOCollection materials)
    {
        DateTime startDate = Convert.ToDateTime(materials[0].Year + " " + materials[0].Month);
        DateTime endDate = Convert.ToDateTime(materials[materials.Count - 1].Year + " " + materials[materials.Count - 1].Month);

        IEnumerable<long> ids = materials.Select(m => m.ID).Distinct();
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Script);
                writer.WriteLine("$(document).ready(function() {");
                writer.WriteLine("var ctx = $('#reportMaterialChart');");
                writer.WriteLine("");
                writer.WriteLine("var myChart = new Chart(ctx, {");
                writer.WriteLine("type: 'line',");
                writer.WriteLine("data: {");
                writer.Write("labels: [");

                foreach (var date in EachMonth(startDate, endDate))
                {
                    writer.Write(" '" + date.Month + "/" + date.Year + "',");
                }
                writer.WriteLine("],");
                writer.WriteLine("datasets: [");

                long id = 0;
                bool first = true;

                foreach (var materialDTO in materials.OrderBy(m => m.ID))
                {
                    if (id != materialDTO.ID)
                    {
                        if (!first)
                        {
                            writer.WriteLine("},");
                        }

                        writer.WriteLine("{label: '" + materialDTO.Title + "',");
                        writer.WriteLine("borderColor: 'rgba(30,136,229, 0.8)',");
                        writer.WriteLine("backgroundColor: 'rgba(30,136,229, 0.1)',");
                        RenderDataArray(writer, startDate, endDate, ids, materialDTO.ID, materials);
               

                    }
                    first = false;
                    id = materialDTO.ID;
                }
                writer.WriteLine("}");
                writer.WriteLine("]},");
                writer.WriteLine("options: {");
                writer.WriteLine("scales: {");
                writer.WriteLine("xAxes: [{");

                writer.WriteLine("stacked: true");
                writer.WriteLine("}],");
                writer.WriteLine("yAxes: [{");
                writer.WriteLine("stacked: true");
                writer.WriteLine("}]");
                writer.WriteLine("},");
                writer.WriteLine("legend:");
                writer.WriteLine("{");
                writer.WriteLine("display: false");
                writer.WriteLine("}");
                writer.WriteLine("}");
                writer.WriteLine("});");
                writer.WriteLine("});");
                writer.RenderEndTag(); // Script
            }

            litChartScript.Text = sw.ToString();
        }
        startDate.AddMonths(1);

    }


    public HtmlTextWriter RenderDataArray(HtmlTextWriter writer, DateTime start, DateTime end, IEnumerable<long> ids, long ID, MaterialReportDTOCollection materials)
    {
        bool first = true;
        writer.Write("data:[");
        foreach (DateTime date in EachMonth(start, end))
        {

            if (materials.Where(m => m.ID == ID && m.Year == date.Year && m.Month == date.Month).Count() > 0)
            {
                foreach (var materialDTO in materials.Where(m => m.ID == ID && m.Year == date.Year && m.Month == date.Month))
                {
                    writer.Write((!first ? ", " : "") + materialDTO.Totalprice.ToString("N2").Replace(".", "").Replace(",", "."));
                    first = false;
                }
            }
            else
            {
                writer.Write((!first ? ", " : "") + "0.00");
                first = false;
            }

        }

        writer.WriteLine("],");
        return writer;
    }

    public IEnumerable<DateTime> EachMonth(DateTime from, DateTime thru)
    {
        for (var month = from.Date; month.Date <= thru.Date; month = month.AddMonths(1))
            yield return month;
    }


    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        DateTime dateFrom = !String.IsNullOrEmpty(txtDateFrom.Text) ? Convert.ToDateTime(txtDateFrom.Text) : Convert.ToDateTime("1790-01-01 00:00:00");
        DateTime dateTo = !String.IsNullOrEmpty(txtDateTo.Text) ? Convert.ToDateTime(txtDateTo.Text) : DateTime.MaxValue.AddYears(-1);
        Int64 materialID = Convert.ToInt64(ddlMaterial.SelectedValue);
        Int64 activityID = Convert.ToInt64(ddlActivity.SelectedValue);
        Int64 partID = Convert.ToInt64(ddlPart.SelectedValue);
        string title = "";
        try
        {


            TotalPriceReportDTOCollection prices = new TotalPriceReportDTOCollection();
            MaterialReportDTOCollection materials = new MaterialReportDTOCollection();

            if (materialID > 0)
            {
                prices = TotalPriceReportDTO.Utils.GetTotalPricesGroupedByMonthByMaterialID(caseID, dateFrom, dateTo, materialID);
                materials = MaterialReportDTO.Utils.GetAllWaiveMaterialsByDate(caseID, dateFrom, dateTo, null, null, null, materialID, null);
                title = new Material(materialID).Title;
            }
            else if (partID > 0)
            {
                prices = TotalPriceReportDTO.Utils.GetTotalPricesGroupedByMonthByPartID(caseID, dateFrom, dateTo, partID);
                title = new Part(partID).Title;
            }
            else if (activityID > 0)
            {
                prices = TotalPriceReportDTO.Utils.GetTotalPricesGroupedByMonthByActivityID(caseID, dateFrom, dateTo, activityID);
                title = new Activity(activityID).Title;
            }
            else
            {
                prices = TotalPriceReportDTO.Utils.GetTotalPricesGroupedByMonth(caseID, dateFrom, dateTo);
                title = "Total";
            }

            renderPrices(prices, title);

           

            RenderChartScript(materials);
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }

    private void renderPrices(TotalPriceReportDTOCollection prices, string Title)
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                decimal TotalTotalPrice = 0;
               
               litTitle.Text = Title;
  
                writer.AddAttribute("class", "reporttable");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);

                foreach (var item in prices)
                {
                    TotalTotalPrice += item.Totalprice;
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(Convert.ToDateTime(item.Year + "-" + item.Month).ToString("yyyy MMM"));
                    writer.RenderEndTag(); // Td
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(item.Totalprice.ToString("N2"));
                    writer.RenderEndTag(); // Td
                    writer.RenderEndTag(); // Tr
                }
                writer.AddAttribute("class", "total");
                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write("Total");
                writer.RenderEndTag(); // Td
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(TotalTotalPrice.ToString("N2"));
                writer.RenderEndTag(); // Td
                writer.RenderEndTag(); // Tr             
                writer.RenderEndTag(); // Table
            }
            litTotalPrices.Text = sw.ToString();
        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}


