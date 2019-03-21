using Juinti.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Case_CaseContract : System.Web.UI.Page
{
    long contractID;
    long caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        contractID = Convert.ToInt64(Request.QueryString["contractid"]);
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);

        Contract contract = new Contract(contractID);

        litHeader.Text = contract.Title;

        if (!Page.IsPostBack)
        {
            FillModalValues(contract);
        }

    }

    private void FillModalValues(Contract contract)
    {
        txtContractTitle.Text = contract.Title;
    }

    protected void btnSaveContract_Click(object sender, EventArgs e)
    {
        Contract contract = new Contract(contractID);
        contract.Title = txtContractTitle.Text;
        contract.Save();
        Response.Redirect(Request.RawUrl);
    }
}