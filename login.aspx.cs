using Juinti.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Utils.LoginUser(txtUsername.Text, txtPassword.Text, this.Page, "~/case/case.aspx");
    }

    protected void btnForgot_Click(object sender, EventArgs e)
    {

    }
}