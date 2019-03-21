using Juinti.Case;
using Juinti.Stock;
using Juinti.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Case_Stock : System.Web.UI.Page
{
    Int64 caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        caseID = Convert.ToInt64(Request.QueryString["caseID"]);

        CreateStockList();
    }



    private void CreateStockList()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                MaterialStockDTOCollection materials = MaterialStockDTO.Utils.GetAllWaiveMaterials(caseID, null, null, null, null, null, null, null);

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

                writer.AddAttribute("class", "price");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.StockTexts.ListHeaderAmount);
                writer.RenderEndTag(); // Th

                writer.RenderEndTag(); //Tr

                foreach (MaterialStockDTO material in materials)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);


                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("href", Urls.MaterialUrl + "?caseid=" + caseID + "&materialid=" + material.ID + "&pagetype=material");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(material.Title);
                    writer.RenderEndTag(); // A
                    writer.RenderEndTag(); // Td

                    MaterialUnit materialUnit = new MaterialUnit(material.UnitTypeID);

                    writer.AddAttribute("class", "unit");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(materialUnit.Title);
                    writer.RenderEndTag(); // Td

                    writer.AddAttribute("class", "amount");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.Write(material.TotalAmount.ToString("N2"));
                    writer.RenderEndTag(); // Td

                    writer.RenderEndTag(); //Tr
                }
                writer.RenderEndTag(); // Table

                litStockList.Text = sw.ToString();
            }


        }
    }

    protected void btnOrder_Click(object sender, EventArgs e)
    {

    }
}