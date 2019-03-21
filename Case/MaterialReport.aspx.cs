using Juinti.Case;
using Juinti.Contacts;
using Juinti.Report;
using Juinti.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MaterialReportPage : System.Web.UI.Page
{
    Int64 materialID;
    Int64 caseID;
    static Random random = new Random();

    protected void Page_Load(object sender, EventArgs e)
    {
        materialID = Convert.ToInt64(Request.QueryString["materialid"]);
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);

        Material material = new Material(materialID);
        litHeader.Text = material.Title;

        linkDetails.NavigateUrl = Urls.MaterialUrl + "?caseid=" + caseID + "&materialid=" + materialID + "&pagetype=material";

        if (!Page.IsPostBack)
        {
         
        }


      
        MaterialReportDTOCollection materials = MaterialReportDTO.Utils.GetAllWaiveMaterialsByDate(caseID, null, null, null, null, null, materialID, null);

        RenderChartScript(materials);

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
                        //RenderColorArray(writer, startDate, endDate, ids, materialDTO.ID, materials, "'#1e88e5'");

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
                writer.WriteLine("stacked: true,");
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
                    writer.Write((!first ? ", " : "") + materialDTO.TotalAmount);
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


    public HtmlTextWriter RenderColorArray(HtmlTextWriter writer, DateTime start, DateTime end, IEnumerable<long> ids, long ID, MaterialReportDTOCollection materials, string Color)
    {
        bool first = true;
        writer.Write("backgroundColor:[");
        foreach (DateTime date in EachMonth(start, end))
        {
            writer.Write((!first ? ", " : "") + Color);
            first = false;
        }

        writer.WriteLine("]");
        return writer;
    }

    private string ColorGenerator()
    {
        return "'hsl(" + 30 + ", " + random.Next(40, 101) + "%, " + random.Next(20, 80) + "%)'";
    }

    public IEnumerable<DateTime> EachMonth(DateTime from, DateTime thru)
    {
        for (var month = from.Date; month.Date <= thru.Date; month = month.AddMonths(1))
            yield return month;
    }

}