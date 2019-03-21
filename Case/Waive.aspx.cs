using Juinti.Case;
using Juinti.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WaivePage : System.Web.UI.Page
{
    Int64 caseID = 0;
    Int64 contractID = 0;
    Int64 waiveID = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);
        contractID = Convert.ToInt64(Request.QueryString["contractid"]);
        waiveID = Convert.ToInt64(Request.QueryString["waiveid"]);

      
        Waive waive = new Waive(waiveID);
        Milestone milestone = new Milestone(waive.MilestoneID);


        linkPackages.NavigateUrl = Urls.WaiveUrl + "?caseid=" + caseID + "&waiveid=" + waiveID + "&pagetype=waive";
        linkDetails.NavigateUrl = Urls.WaiveEmailUrl + "?caseid=" + caseID + "&waiveid=" + waiveID + "&pagetype=waive";


        panButtons.Visible = !waive.IsOrdered;
        panCancel.Visible = waive.IsOrdered;

        panAddMaterial.Visible = waive.IsExtra;
        panAddPackage.Visible = !waive.IsExtra;

        litHeader.Text = waive.Title + " - " + waive.Location + " - " + milestone.Title;

        if (!Page.IsPostBack)
        {
            FillDdlWaivePackage(waive.ContractID);
            FillDdlMaterials();
            FillDdlMilestones(waive);
            FillValues(waive);
        }

        if (waive.IsExtra)
        {
            CreateListMaterials();
        }
        else
        {
            CreatePackagesList();
        }      
    }

    private void FillDdlMilestones(Waive waive)
    {
        MilestoneCollection milestones = Milestone.Utils.GetMilestonesByCaseID(caseID);

        foreach (var item in milestones)
        {
            ddlMilestone.Items.Add(new ListItem(item.Title, item.ID.ToString()));
            
        }
        ddlMilestone.SelectedValue = waive.MilestoneID.ToString();
    }

  
    private void FillValues(Waive waive)
    {
        txtWaiveTitle.Text = waive.Title;
        txtMessage.Text = waive.Message;
        txtLocation.Text = waive.Location;
        txtEstDeliveryDate.Text =  !waive.EstOrderDate.Date.Equals(DateTime.MaxValue.Date) ? waive.EstOrderDate.ToShortDateString() : "";
    }

    private void FillDdlWaivePackage(long contractID)
    {
        PackageCollection packages = Package.Utils.GetPackagesByCaseID(caseID);
        foreach (var item in packages)
        {
            ddlWaivePackage.Items.Add(new ListItem(item.Title, item.ID.ToString()));
        }
    }

    private void FillDdlMaterials()
    {
        MaterialCollection materials = Material.Utils.GetMaterialsByCaseID(caseID);
        ddlMaterial.Items.Add(new ListItem(Resources.WaiveTexts.ChooseMaterial, "0"));
        foreach (var item in materials)
        {
            ListItem listItem = new ListItem(item.Title, item.ID.ToString());
            ddlMaterial.Items.Add(listItem);
        }
    }

    private void CreateListMaterials()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {

                MaterialCollection materials = Material.Utils.GetMaterialsByWaiveID(waiveID);
                if (materials.Count() > 0)
                {
                    writer.RenderBeginTag(HtmlTextWriterTag.H2);
                    writer.Write(Resources.MaterialTexts.Materials);
                    writer.RenderEndTag(); // H2
                    writer.AddAttribute("class", "listview");
                    writer.AddAttribute("cellspacing", "0");
                    writer.AddAttribute("cellpadding", "0");
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);

                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.AddAttribute("class", "title");
                    writer.AddAttribute("style", "width:100%;");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.AddAttribute("class", "title");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(Resources.MaterialTexts.ListHeaderTitle);
                    writer.RenderEndTag(); // Span


                    writer.AddAttribute("class", "price");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(Resources.PackageTexts.ListHeaderPrice);
                    writer.RenderEndTag(); // Span

                    writer.AddAttribute("class", "amount");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(Resources.PackageTexts.Amount);
                    writer.RenderEndTag(); // Span


                    writer.RenderEndTag(); // Th               

                    writer.AddAttribute("class", "delete");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.RenderEndTag(); // Th
                    writer.RenderEndTag(); // Tr
                    foreach (var material in materials)
                    {
                        WaiveMaterialAmountCollection materialAmounts = WaiveMaterialAmount.Utils.GetWaiveMaterialAmountByWaiveID_MaterialID(waiveID, material.ID);

                        bool isLength = material.UnitType == 1 ? true : false;
                        bool isSquare = material.UnitType == 2 ? true : false;

                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                        writer.AddAttribute("class", "listvalue");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.AddAttribute("href", Urls.MaterialUrl + "?caseid=" + caseID + "&materialid=" + material.ID + "&pagetype=material");
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.AddAttribute("class", "title");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(material.Title);
                        writer.RenderEndTag(); // Span
                        writer.RenderEndTag(); // A

                        if (isLength || isSquare)
                        {
                            decimal price = 0;
                            writer.RenderBeginTag(HtmlTextWriterTag.Ul);
                            foreach (var item in materialAmounts)
                            {

                                writer.RenderBeginTag(HtmlTextWriterTag.Li);
                                if (isLength)
                                {
                                    writer.AddAttribute("class", "length");
                                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                    writer.Write(item.Length.ToString("0") + "mm");
                                    writer.RenderEndTag();

                                    price = material.Price.Price * (item.Length / 1000) * item.Amount;

                                }
                                else if (isSquare)
                                {
                                    writer.AddAttribute("class", "length");
                                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                    writer.Write(item.Length.ToString("0") + "mm");
                                    writer.RenderEndTag();
                                    writer.AddAttribute("class", "x_mark");
                                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                    writer.Write("x");
                                    writer.RenderEndTag();
                                    writer.AddAttribute("class", "width");
                                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                    writer.Write(item.Width.ToString("0") + "mm");
                                    writer.RenderEndTag();

                                    price = material.Price.Price * (item.Length * item.Width / 1000000) * item.Amount;
                                }

                                writer.AddAttribute("class", "price");
                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                writer.Write(price.ToString("N2"));
                                writer.RenderEndTag(); // Span

                                writer.AddAttribute("class", "amount");
                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                writer.Write(item.Amount.ToString("N0"));
                                writer.AddAttribute("class", "fa fa fa-pencil");
                                writer.AddAttribute("onclick", "openModalEditWaiveMaterialAmount(" + material.ID + ",'" + item.Amount + "','" + item.Length + "', '" + item.Width + "')");
                                writer.RenderBeginTag(HtmlTextWriterTag.I);
                                writer.RenderEndTag(); // I 
                                writer.RenderEndTag(); // Span

                                writer.RenderEndTag(); // Li

                            }
                            writer.RenderEndTag(); // Ul
                            writer.RenderEndTag(); // Td               

                        }
                        else
                        {
                            decimal price = 0;
                            price = materialAmounts.Count() > 0 ? material.Price.Price * materialAmounts[0].Amount : 0;
                            writer.AddAttribute("class", "price");
                            writer.RenderBeginTag(HtmlTextWriterTag.Span);
                            writer.Write(price.ToString("N2"));
                            writer.RenderEndTag(); // Span

                            writer.AddAttribute("class", "amount");
                            writer.RenderBeginTag(HtmlTextWriterTag.Span);
                            writer.Write(materialAmounts.Count() > 0 ? materialAmounts[0].Amount.ToString("N0") : "0");
                            writer.AddAttribute("class", "fa fa fa-pencil");
                            if (materialAmounts.Count() > 0)
                            {
                                writer.AddAttribute("onclick", "openModalEditWaiveMaterialAmount(" + material.ID + ",'" + materialAmounts[0].Amount.ToString() + "',0,0)");
                            }
                            else
                            {
                                writer.AddAttribute("onclick", "openModalAddWaiveMaterialAmount(" + material.ID + ", 0 ," + material.UnitType + ")");
                            }

                            writer.RenderBeginTag(HtmlTextWriterTag.I);
                            writer.RenderEndTag(); // I 
                            writer.RenderEndTag(); // Span 
                            writer.RenderEndTag(); // Td                 
                        }

                        if (!isLength && !isSquare)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.RenderEndTag(); // Td
                        }
                        else
                        {
                            writer.AddAttribute("class", "delete");
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.AddAttribute("class", "fa fa fa-plus");
                            writer.AddAttribute("onclick", "openModalAddWaiveMaterialAmount(" + material.ID + ", 0 ," + material.UnitType + ")");
                            writer.RenderBeginTag(HtmlTextWriterTag.I);
                            writer.RenderEndTag(); // I 
                            writer.RenderEndTag(); // Td
                        }
                        writer.RenderEndTag(); //Tr
                    }
                    writer.RenderEndTag(); // Table  
                }

                litMaterialsList.Text = sw.ToString();
            }
        }
    }

    private void CreatePackagesList()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                PackageCollection packages = Package.Utils.GetPackagesByWaiveID(waiveID);

                Waive waive = new Waive(waiveID);
                if (packages.Count() > 0)
                {                   
                    writer.AddAttribute("class", "listview");
                    writer.AddAttribute("cellspacing", "0");
                    writer.AddAttribute("cellpadding", "0");
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.AddAttribute("class", "package");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.Write(Resources.PackageTexts.Package);
                    writer.RenderEndTag(); // Th


                    writer.AddAttribute("class", "amount");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.Write(Resources.PackageTexts.Amount);
                    writer.RenderEndTag(); // Th

                    writer.AddAttribute("class", "produtionweeks");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.Write(Resources.PackageTexts.ListHeaderProductionWeeks);
                    writer.RenderEndTag(); // Th                

                    writer.AddAttribute("class", "delete");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.Write(Resources.Global.ListHeaderButtons);
                    writer.RenderEndTag(); // Th
                    writer.RenderEndTag(); // Tr
                    foreach (var package in packages)
                    {
                        Activity activity = new Activity(package.ActivityID);

                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                        writer.AddAttribute("class", "package");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.AddAttribute("href", Urls.PackageUrl + "?caseid=" + caseID + "&packageid=" + package.ID + "&activityid=" + package.ActivityID + "&pagetype=package");
                        writer.RenderBeginTag(HtmlTextWriterTag.A);
                        writer.AddAttribute("class", "title");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(package.Title + " - " + activity.Title);
                        writer.RenderEndTag(); //Span
                        writer.RenderEndTag(); // A
                        writer.RenderEndTag(); // Td                      
                        writer.AddAttribute("class", "amount");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.AddAttribute("class", "amount");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(package.WaiveAmount);
                        if (!waive.IsOrdered)
                        {
                            writer.AddAttribute("class", "fa fa-pencil edit");
                            writer.AddAttribute("onclick", "return editPackageAmount(" + package.ID + ", '" + package.Title + "', " + package.WaiveAmount + ", "+ package.WaiveProductionWeeks + ");");
                            writer.RenderBeginTag(HtmlTextWriterTag.I);
                            writer.RenderEndTag(); // I
                        }
                        writer.RenderEndTag(); // Span
                        writer.RenderEndTag(); // Td

                        writer.AddAttribute("class", "amount");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        writer.AddAttribute("class", "amount");
                        writer.RenderBeginTag(HtmlTextWriterTag.Span);
                        writer.Write(package.WaiveProductionWeeks);
                        if (!waive.IsOrdered)
                        {
                            writer.AddAttribute("class", "fa fa-pencil edit");
                            writer.AddAttribute("onclick", "return editPackageProdutionWeeks(" + package.ID + ", '" + package.Title + "', " + package.WaiveAmount + ", " + package.WaiveProductionWeeks + ");");
                            writer.RenderBeginTag(HtmlTextWriterTag.I);
                            writer.RenderEndTag(); // I
                        }
                        writer.RenderEndTag(); // Span
                        writer.RenderEndTag(); // Td                      

                        writer.AddAttribute("class", "delete");
                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                        if (!waive.IsOrdered)
                        {
                            writer.AddAttribute("class", "fa fa fa-times remove");
                            writer.AddAttribute("onclick", "return removePackage(" + package.ID + ", '" + package.Title + "');");
                            writer.RenderBeginTag(HtmlTextWriterTag.I);
                            writer.RenderEndTag(); // I         
                        }
                        writer.RenderEndTag(); //Td

                        writer.RenderEndTag(); // Tr

                    }               

                    writer.RenderEndTag(); // Table
                }
                litPackagesList.Text = sw.ToString();
            }

        }
    }



    protected void btnAddPackage_Click(object sender, EventArgs e)
    {
        Waive waive = new Waive(waiveID);

        Int64 packageID = Convert.ToInt64(ddlWaivePackage.SelectedValue);
        Int32 amount = Convert.ToInt32(txtAmount.Text);
        Int32 productionWeeks = Convert.ToInt32(txtProductionsWeeks.Text);
        waive.SavePackage(packageID, amount, productionWeeks);
        Response.Redirect(Request.RawUrl);
    }

    protected void btnRemovePackage_Click(object sender, EventArgs e)
    {
        Int64 packageID = Convert.ToInt64(hidPackageID.Value);
        Waive waive = new Waive(waiveID);
        waive.RemovePackage(packageID);
        Response.Redirect(Request.RawUrl);
    }

    protected void btnEditAmount_Click(object sender, EventArgs e)
    {
        Int64 packageID = Convert.ToInt64(hidPackageID.Value);
        Int32 amount = Convert.ToInt32(txtEditAmount.Text);
        Int32 productionWeeks = Convert.ToInt32(txtEditWeeks.Text);
        Waive waive = new Waive(waiveID);
        waive.SavePackage(packageID, amount, productionWeeks);
        Response.Redirect(Request.RawUrl);
    }

    protected void btnSaveWaive_Click(object sender, EventArgs e)
    {
        Waive waive = new Waive(waiveID);
        waive.MilestoneID = Convert.ToInt64(ddlMilestone.SelectedValue);
        waive.Title = txtWaiveTitle.Text;
        waive.Location = txtLocation.Text;
        waive.Message = txtMessage.Text;
        if (!String.IsNullOrEmpty(txtEstDeliveryDate.Text))
        {
            waive.EstOrderDate = Convert.ToDateTime(txtEstDeliveryDate.Text);

        }
        waive.Save();
        Response.Redirect(Request.RawUrl);

    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btnAddMaterial_Click(object sender, EventArgs e)
    {

        Int64 materialID = Convert.ToInt64(hidMaterialID.Value);

        if (materialID > 0)
        {
            Waive waive = new Waive(waiveID);
            waive.AddMaterial(materialID);
            Response.Redirect(Request.RawUrl);
        }


    }

    protected void btnEditMatrialAmount_Click(object sender, EventArgs e)
    {
        long materialID = Convert.ToInt64(hidMaterialID.Value);
        decimal length = Convert.ToDecimal(hidLength.Value);
        decimal width = Convert.ToDecimal(hidWidth.Value);
        WaiveMaterialAmount materialAmount = new WaiveMaterialAmount(waiveID, materialID, length, width);
        materialAmount.Amount = Convert.ToDecimal(txtEditMaterialAmount.Text);
        materialAmount.Save();
        Response.Redirect(Request.RawUrl);
    }

    protected void btnAddMaterialAmount_Click(object sender, EventArgs e)
    {
        WaiveMaterialAmount materialAmount = new WaiveMaterialAmount();
        materialAmount.Amount = Convert.ToDecimal(txtMaterialAmount.Text);

        string lenght = txtLength.Text;
        string width = txtWidth.Text;

        if (!String.IsNullOrEmpty(lenght))
        {
            materialAmount.Length = Convert.ToDecimal(lenght);
        }

        if (!String.IsNullOrEmpty(width))
        {
            materialAmount.Width = Convert.ToDecimal(width);
        }

        materialAmount.WaiveID = waiveID;
        materialAmount.MaterialID = Convert.ToInt64(hidMaterialID.Value);
        materialAmount.Add();
        Response.Redirect(Request.RawUrl);
    }

 
    protected void btnEditWeeks_Click(object sender, EventArgs e)
    {
        Int64 packageID = Convert.ToInt64(hidPackageID.Value);
        Int32 productionWeeks = Convert.ToInt32(txtEditWeeks.Text);
        Int32 amount = Convert.ToInt32(txtEditAmount.Text);
        Waive waive = new Waive(waiveID);
        waive.SavePackage(packageID, amount, productionWeeks);
        Response.Redirect(Request.RawUrl);
    }

    protected void lnkCancel_Click(object sender, EventArgs e)
    {
        Waive waive = new Waive(waiveID);
        waive.IsOrdered = false;
        waive.OrderDate = null;
        if (waive.Save())
        {
            Waive.Utils.UpdateWaivePackages(waiveID);
        }
        Response.Redirect(Request.RawUrl);
    }
}