using Juinti.Case;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Case_CaseStructure : System.Web.UI.Page
{
    long structureID;
    long caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        structureID = Convert.ToInt64(Request.QueryString["structureid"]);
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);

        Structure structure = new Structure(structureID);

        litHeader.Text = structure.Title;

        if (!Page.IsPostBack)
        {
            FillModalValues(structure);
        }

    }

    private void FillModalValues(Structure structure)
    {
        txtStructureTitle.Text = structure.Title;
    }

    protected void btnSaveStructure_Click(object sender, EventArgs e)
    {
        Structure structure = new Structure(structureID);
        structure.Title = txtStructureTitle.Text;
        structure.Save();
        Response.Redirect(Request.RawUrl);
    }
}