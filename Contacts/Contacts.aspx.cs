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

public partial class Contacts_Contacts : System.Web.UI.Page
{
    Int64 caseID;
  

    protected void Page_Load(object sender, EventArgs e)
    {
        litHeader.Text = Resources.ContactsTexts.Contacts;
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);
        if (!Page.IsPostBack)
        {
            FillDdlCompanies();
        }
        CreateListContacts();
    }

    private void FillDdlCompanies()
    {
        CompanyCollection companies = Company.Utils.GetCompanies();
        ddlCompanies.Items.Add(new ListItem(Resources.ContactsTexts.NoCompanyDdlValue, "0"));
        foreach (var item in companies)
        {
            ddlCompanies.Items.Add(new ListItem(item.Name, item.ID.ToString()));
        }
        
    }

    private void CreateListContacts()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                CompanyCollection companies = Company.Utils.GetCompanies();


                foreach (var company in companies)
                {

                    writer.AddAttribute("class", "listview");
                    writer.AddAttribute("cellspacing", "0");
                    writer.AddAttribute("cellpadding", "0");
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);

                    writer.AddAttribute("colspan", "3");
                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.AddAttribute("href", Urls.ContactCompanyUrl + "?caseid=" + caseID + "&companyid=" + company.ID + "&pagetype=contacts");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(company.Name);
                    writer.RenderEndTag(); // Span
                    writer.RenderEndTag(); // A
                    writer.RenderEndTag(); // Th       
               

                    writer.AddAttribute("class", "delete");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.AddAttribute("class", "fa fa fa-times remove");
                    writer.AddAttribute("onclick", "return deleteCompany(" + company.ID + ",'" + company.Name + "');");
                    writer.RenderBeginTag(HtmlTextWriterTag.I);
                    writer.RenderEndTag(); // I
                    writer.RenderEndTag(); // Th
                    writer.RenderEndTag(); //Tr
                    PersonCollection persons = Person.Utils.GetPersonsByCompanyID(company.ID);


                    writer.AddAttribute("class", "listview inner contactpersons");
                    writer.AddAttribute("cellspacing", "0");
                    writer.AddAttribute("cellpadding", "0");
                    foreach (var person in persons)
                    {
                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);

                        writer.AddAttribute("href", Urls.ContactPersonUrl + "?caseid=" + caseID + "&personid=" + person.ID + "&pagetype=contacts");
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.AddAttribute("class", "title");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(person.Name);
                        writer.RenderEndTag(); // Span
                        writer.RenderEndTag(); // A
                        writer.RenderEndTag(); // Td

                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.AddAttribute("href", "mailto:" + person.Email);
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.AddAttribute("class", "email");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(person.Email);
                        writer.RenderEndTag(); // Span
                        writer.RenderEndTag(); // A
                        writer.RenderEndTag(); // Td

                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.AddAttribute("href", "tel:" + person.Phone);
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.AddAttribute("class", "phone");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(person.Phone);
                        writer.RenderEndTag(); // Span
                        writer.RenderEndTag(); // A
                        writer.RenderEndTag(); // Td

                        writer.AddAttribute("class", "delete");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.AddAttribute("class", "fa fa fa-times remove");
                        writer.AddAttribute("onclick", "return deleteContact(" + person.ID + ",'" + person.Name + "');");
                        writer.RenderBeginTag(HtmlTextWriterTag.I);
                        writer.RenderEndTag(); // I
                        writer.RenderEndTag(); // Td
                        writer.RenderEndTag(); // Tr
                    }
                    writer.RenderEndTag(); // Table
                }

            }
            litListContacts.Text = sw.ToString();
        }
    }

    protected void btnCreatePerson_Click(object sender, EventArgs e)
    {
        string MembershipUserid = Membership.GetUser().ProviderUserKey.ToString();

        bool newCompany = false;
        Int64 companyID = 0;

        if (!String.IsNullOrEmpty(txtCompanyName.Text))
        {
            newCompany = true;
            Company company = new Company();
            company.Name = txtCompanyName.Text;
            company.MembershipUserid = MembershipUserid;
            company.Save();
            companyID = company.ID;
        }
        Person person = new Person();

        person.Name = txtName.Text;
        person.Email = txtEmail.Text;
        person.Phone = txtPhone.Text;
        person.CompanyID = newCompany ? companyID : Convert.ToInt64(ddlCompanies.SelectedValue);
        person.MembershipUserid = MembershipUserid;
        person.Save();

        Response.Redirect(Request.RawUrl);
    }


    protected void btnDeleteContact_Click(object sender, EventArgs e)
    {
        Person.Utils.DeletePerson(Convert.ToInt64(hidContactID.Value));
        Response.Redirect(Request.RawUrl);

    }
    protected void btnDeleteCompany_Click(object sender, EventArgs e)
    {
        Company.Utils.DeleteCompany(Convert.ToInt64(hidCompanyID.Value));
        Response.Redirect(Request.RawUrl);
    }
}