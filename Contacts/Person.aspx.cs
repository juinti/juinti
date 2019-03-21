using Juinti.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contacts_Person : System.Web.UI.Page
{
    Int64 personid;
    Int64 caseID;

    protected void Page_Load(object sender, EventArgs e)
    {

        personid = Convert.ToInt64(Request.QueryString["personid"]);
        Person person = new Person(personid);

        litHeader.Text = person.Name;
        if (!Page.IsPostBack)
        {
            FillDdlCompany();
            FillValues(person);
         
        }
        
    }

    private void FillDdlCompany()
    {
        CompanyCollection companies = Company.Utils.GetCompanies();
        foreach (var item in companies)
        {
            ddlCompany.Items.Add(new ListItem(item.Name, item.ID.ToString()));

        }
    }

    private void FillValues(Person person)
    {
        txtName.Text = person.Name;
        txtEmail.Text = person.Email;
        txtPhone.Text = person.Phone;
        ddlCompany.SelectedValue = person.CompanyID.ToString();
    }

    protected void btnSaveContact_Click(object sender, EventArgs e)
    {
        personid = Convert.ToInt64(Request.QueryString["personid"]);
        Person person = new Person(personid);

        person.Name = txtName.Text;
        person.Email = txtEmail.Text;
        person.Phone = txtPhone.Text;
        person.CompanyID = Convert.ToInt64(ddlCompany.SelectedValue);
        person.Save();

        Response.Redirect(Request.RawUrl);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
}