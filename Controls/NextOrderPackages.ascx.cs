using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Juinti.Lists;
using Juinti.Variables;

public partial class Controls_NextOrderPackages : System.Web.UI.UserControl
{
    Int64 caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);

        RenderList(caseID);
    }

    private void RenderList(long caseID)
    {
        NextOrderPackageCollection nextOrderPackages = NextOrderPackage.Utils.GetTopNextOrderPackages(caseID, 10);

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);

                foreach (NextOrderPackage package in nextOrderPackages)
                {
                    if (package.TheConfirmDate < DateTime.Now)
                    {
                        writer.AddAttribute("class", "red");
                    }
                    else if (package.TheConfirmDate >= DateTime.Now && package.TheConfirmDate < DateTime.Now.AddDays(5))
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
                    writer.Write(package.TheConfirmDate.ToShortDateString());
                    writer.RenderEndTag(); // Span 

                    writer.AddAttribute("href", Urls.WaiveEmailUrl + "?caseid=" + caseID + "&waiveid=" + package.WaiveID + "&packageid=" + package.PackageID + "&pagetype=production");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(package.PackageTitle);
                    writer.RenderEndTag(); // A 
                    writer.RenderEndTag(); // Li
                }

                writer.RenderEndTag(); // Ul

                litList.Text = sw.ToString();
            }

        }
    }
}