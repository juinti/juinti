using Juinti.Case;
using Juinti.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PackagesPage : System.Web.UI.Page
{
    Int64 caseID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        litHeader.Text = Resources.PackageTexts.Packages;
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);

        CreateListPackages();
    }

    private void CreateListPackages()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                PackageCollection packages = Package.Utils.GetPackagesByCaseID(caseID);

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
                writer.Write(Resources.PackageTexts.ListHeaderTitle);
                writer.RenderEndTag(); // Th


                //writer.AddAttribute("class", "activity");
                //writer.RenderBeginTag(HtmlTextWriterTag.Th);
                //writer.Write(Resources.PackageTexts.ListHeaderActivity);
                //writer.RenderEndTag(); // Th

                writer.AddAttribute("class", "price");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.PackageTexts.ListHeaderPrice);
                writer.RenderEndTag(); // Th
                writer.AddAttribute("class", "delete");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.Global.ListHeaderButtons);
                writer.RenderEndTag(); // Th
                writer.RenderEndTag(); // Tr
                foreach (var package in packages)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("class", "id dimmed");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(package.ID);
                    writer.RenderEndTag(); //Span
                    writer.RenderEndTag(); // Td


                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.AddAttribute("href", Urls.PackageUrl + "?caseid=" + caseID + "&packageid=" + package.ID + "&activityid=" + package.ActivityID + "&pagetype=package#tabindex-0");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(package.Title);
                    writer.RenderEndTag(); //Span
                    writer.RenderEndTag(); // A
                    writer.RenderEndTag(); // Td

                    //Activity activity = new Activity(package.ActivityID);

                    //writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    //writer.AddAttribute("class", "activity");
                    //writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    //writer.Write(activity.Title);
                    //writer.RenderEndTag(); //Span
                    //writer.RenderEndTag(); // Td

                    writer.AddAttribute("class", "price");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(package.GetTotalPrice().ToString("N2") + Resources.Global.CurrencyDisplay);
                    writer.RenderEndTag(); //Span
                    writer.RenderEndTag(); // Td


                    writer.AddAttribute("class", "delete");
                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                    if (!package.isUsedInWaive())
                    {
                        writer.AddAttribute("class", "fa fa fa-times remove");
                        writer.AddAttribute("onclick", "return deletePackage(" + package.ID + ", '" + package.Title + "');");
                        writer.RenderBeginTag(HtmlTextWriterTag.I);
                        writer.RenderEndTag(); // I
                    }
                    writer.RenderEndTag(); // Td

                    writer.RenderEndTag(); // Tr

                }
                writer.RenderEndTag(); // Table
            }
            litlistPackages.Text = sw.ToString();
        }
    }

    protected void btnCreatePackage_Click(object sender, EventArgs e)
    {

        Package package = new Package();
        package.Title = txtPackageTitle.Text;
        package.CaseID = caseID;
        package.ActivityID = 0;
        package.ContractID = 0;
        package.Save();
        Response.Redirect(Request.RawUrl);
    }

    protected void btnDeletePackage_Click(object sender, EventArgs e)
    {
        Package.Utils.DeletePackage(Convert.ToInt64(hidPackageID.Value));
        Response.Redirect(Request.RawUrl);
    }
}