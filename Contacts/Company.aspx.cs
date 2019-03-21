using Juinti.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contacts_Company : System.Web.UI.Page
{
    Int64 companyID;
    Int64 caseID;

    protected void Page_Load(object sender, EventArgs e)
    {

        companyID = Convert.ToInt64(Request.QueryString["companyid"]);
        Company company = new Company(companyID);

        litHeader.Text = company.Name;
        if (!Page.IsPostBack)
        {
            FillValues(company);
        }

    }

    private void FillValues(Company company)
    {
        txtName.Text = company.Name;
        txtEmail.Text = company.Email;
        txtPhone.Text = company.Phone;
        txtAddress.Text = company.Address;
        txtAddress2.Text = company.Address2;
        txtZipCode.Text = company.Zipcode;
        txtCity.Text = company.City;
    }

    protected void btnSaveContact_Click(object sender, EventArgs e)
    {     
        Company company = new Company(companyID);

        company.Name = txtName.Text;
        company.Email = txtEmail.Text;
        company.Phone = txtPhone.Text;
        company.Address = txtAddress.Text;
        company.Address2 = txtAddress2.Text;
        company.Zipcode = txtZipCode.Text;
        company.City = txtCity.Text;
        company.Save();

        Response.Redirect(Request.RawUrl);
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    
}