using Juinti.Case;
using Juinti.User.Customer;
using Juinti.Variables;
using System;
using System.IO;
using System.Web.Security;
using System.Web.UI;

public partial class MasterPage : System.Web.UI.MasterPage
{

    Customer customer = new Customer();
    Int64 caseID = 0;
    Int64 contractID = 0;
    string pageType = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        Page.Form.Attributes["onsubmit"] = "this.action+=top.location.hash;";

        customer = Customer.Load((Guid)Membership.GetUser().ProviderUserKey);
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);
        contractID = Convert.ToInt64(Request.QueryString["contractid"]);
        pageType = Request.QueryString["pagetype"];
     

        lnkContacts.PostBackUrl = Urls.ContactsUrl + "?caseid=" + caseID + "&pagetype=contacts";
        



        if (caseID != 0)
        {
            Case caseItem = new Case(caseID);
            litSelectedCaseName.Text = "";
            litSelctedCaseNumber.Text = caseItem.Number;
            litSelectedCaseName.Text = caseItem.Name;
            SetSelectedMenuText(pageType);
        }
        else
        {
            litSelectedCaseName.Text = Resources.CaseTexts.ChooseCase;
        }


        CreateCaseDropdown();
        CreateMainMenu();
    }

    private void SetSelectedMenuText(string pageType)
    {
        string menuText = "";

        switch (pageType)
        {
            case "overview":
                menuText = Resources.CaseTexts.Overview;
                break;         
            case "milestone":
                menuText = Resources.CaseTexts.Overview;
                break;
            case "activity":
                menuText = Resources.CaseTexts.Overview;
                break;
            case "contract":
                menuText = Resources.CaseTexts.Overview;
                break;
            case "material":
                menuText = Resources.MaterialTexts.Materials;
                break;
            case "part":
                menuText = Resources.PartTexts.Parts;
                break;
            case "package":
                menuText = Resources.PackageTexts.Packages;
                break;
            case "waive": menuText = Resources.WaiveTexts.Waives;
                break;
            case "report":
                menuText = Resources.ReportTexts.Report;
                break;
            default:
                break;
        }      
    }

    private void CreateCaseDropdown()
    {

        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                CaseCollection cases = Juinti.Case.Utils.GetCasesByCustomerID(customer.ID);

                writer.RenderBeginTag(HtmlTextWriterTag.Ul);

                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", "#");
                writer.AddAttribute("onclick", "createCase(); return false;");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddAttribute("class", "title");
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(Resources.Global.CreateCase);
                writer.RenderEndTag(); // Span
                writer.RenderEndTag(); // A
                writer.RenderEndTag(); // Li

                foreach (var item in cases)
                {
                    writer.AddAttribute("class", item.ID == caseID ? "menuitem selected" : "menuitem");

                    writer.RenderBeginTag(HtmlTextWriterTag.Li);
                    writer.AddAttribute("href", Urls.OverviewUrl + "?caseid=" + item.ID + "&pagetype=overview#tabindex-0");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(item.Name);
                    writer.RenderEndTag(); // Span
                    writer.AddAttribute("class", "number");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(item.Number);
                    writer.RenderEndTag(); // Span
                    writer.RenderEndTag(); // A
                    writer.RenderEndTag(); // Li
                }
            }
            litCaseDropdown.Text = sw.ToString();
        }
    }

    private void CreateMainMenu()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                ContractCollection contracts = Contract.Utils.GetContractsByCaseID(caseID);


                writer.AddAttribute("class", "main-menu");
                writer.RenderBeginTag(HtmlTextWriterTag.Ul);              

                if (pageType == "overview")
                {
                    writer.AddAttribute("class", "selected");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", Urls.OverviewUrl + "?caseid=" + caseID + "&pagetype=overview#tabindex-0");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddAttribute("class", "fa fa-home");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag(); // I
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(Resources.CaseTexts.Overview);
                writer.RenderEndTag(); // Span
                writer.RenderEndTag(); // A
                writer.RenderEndTag(); // Li      


                if (pageType == "material")
                {
                    writer.AddAttribute("class", "selected");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", Urls.MaterialsUrl + "?caseid=" + caseID + "&pagetype=material");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddAttribute("class", "fa fa-cubes");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag(); // I
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(Resources.MaterialTexts.Materials);
                writer.RenderEndTag(); // Span
                writer.RenderEndTag(); // A
                writer.RenderEndTag(); // Li      


                if (pageType == "part")
                {
                    writer.AddAttribute("class", "selected");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", Urls.PartsUrl + "?caseid=" + caseID + "&pagetype=part");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddAttribute("class", "fa fa-puzzle-piece");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag(); // I
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(Resources.PartTexts.Parts);
                writer.RenderEndTag(); // Span
                writer.RenderEndTag(); // A
                writer.RenderEndTag(); // Li


                if (pageType == "package")
                {
                    writer.AddAttribute("class", "selected");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", Urls.PackagesUrl + "?caseid=" + caseID + "&pagetype=package");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddAttribute("class", "fa fa-cube");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag(); // I
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(Resources.PackageTexts.Packages);
                writer.RenderEndTag(); // Span
                writer.RenderEndTag(); // A
                writer.RenderEndTag(); // Li


                if (pageType == "waive")
                {
                    writer.AddAttribute("class", "selected");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", Urls.WaivesUrl + "?caseid=" + caseID + "&pagetype=waive#tabindex-0");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddAttribute("class", "fa fa-truck");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag(); // I
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(Resources.WaiveTexts.Waives);
                writer.RenderEndTag(); // Span
                writer.RenderEndTag(); // A
                writer.RenderEndTag(); // Li


                if (pageType == "stock")
                {
                    writer.AddAttribute("class", "selected");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", Urls.StockUrl + "?caseid=" + caseID + "&pagetype=stock");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddAttribute("class", "fa fa-th");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag(); // I
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(Resources.StockTexts.Stock);
                writer.RenderEndTag(); // Span
                writer.RenderEndTag(); // A
                writer.RenderEndTag(); // Li


                if (pageType == "report")
                {
                    writer.AddAttribute("class", "selected");
                }
                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                writer.AddAttribute("href", Urls.ReportUrl + "?caseid=" + caseID + "&pagetype=report#tabindex-0");
                writer.RenderBeginTag(HtmlTextWriterTag.A);
                writer.AddAttribute("class", "fa fa-bar-chart");
                writer.RenderBeginTag(HtmlTextWriterTag.I);
                writer.RenderEndTag(); // I
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(Resources.ReportTexts.Report);
                writer.RenderEndTag(); // Span
                writer.RenderEndTag(); // A
                writer.RenderEndTag(); // Li


          


                writer.RenderEndTag(); // Ul
            }

            litCaseMenu.Text = sw.ToString();
        }
    }

    protected void bntSignOut_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        FormsAuthentication.RedirectToLoginPage();
    }

    protected void btnCreateCase_Click(object sender, EventArgs e)
    {
        Case newCase = new Case();

        newCase.CustomerID = customer.ID;
        newCase.Name = txtCaseName.Text;
        newCase.Number = txtCaseNumber.Text;
        newCase.Street = txtCaseStreet.Text;
        newCase.ZipCode = txtCaseZipCode.Text;
        newCase.City = txtCaseCity.Text;
        newCase.Country = txtCaseCountry.Text;
        newCase.Active = true;

        if (newCase.Save())
        {
            Response.Redirect(Urls.OverviewUrl + "?caseid=" + newCase.ID);
        }


    }
}
