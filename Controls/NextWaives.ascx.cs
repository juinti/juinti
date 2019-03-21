using Juinti.Case;
using Juinti.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Controls_NextWaives : System.Web.UI.UserControl
{

    Int64 caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);


        RenderList(caseID);
    }

    private void RenderList(long caseID)
    {
        WaiveCollection waives = Waive.Utils.GetTopWaivesByCaseID(caseID, 5);

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {

                writer.RenderBeginTag(HtmlTextWriterTag.Ul);

                foreach (Waive waive in waives)
                {
                    if (waive.EstOrderDate < DateTime.Now)
                    {
                        writer.AddAttribute("class", "red");
                    }
                    else if (waive.EstOrderDate >= DateTime.Now && waive.EstOrderDate < DateTime.Now.AddDays(5))
                    {
                        writer.AddAttribute("class", "yellow");
                    }
                    else
                    {
                        writer.AddAttribute("class", "green");
                    }

                    writer.RenderBeginTag(HtmlTextWriterTag.Li);


                    writer.AddAttribute("class", "fa fa-circle");
                    writer.RenderBeginTag(HtmlTextWriterTag.I);
                    writer.RenderEndTag();

                    writer.AddAttribute("class", "date");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(waive.EstOrderDate.ToShortDateString());
                    writer.RenderEndTag(); // Span 

                    writer.AddAttribute("href", Urls.WaiveUrl + "?caseid=" + caseID  + "&waiveid=" + waive.ID + "&pagetype=waive");           
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(waive.Title);
                    writer.RenderEndTag(); // A 
                    writer.RenderEndTag(); // Li

                }

                writer.RenderEndTag(); // UL

            }

            litWaives.Text = sw.ToString();
        }
    }
}