using Juinti.User.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_CreateCustomer_Click(object sender, EventArgs e)
    {
        Customer customer = new Customer();
        customer.Name = txtName.Text;
        customer.ContactPerson = txtContactPerson.Text;
        customer.Phonenumber = txtPhonenumber.Text;
        customer.SaveCustomer(txtEmail.Text, txtPassword.Text);
    }
}