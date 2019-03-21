using Juinti.Case;
using Juinti.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PackagePage : System.Web.UI.Page
{
    Int64 packageID;
    Int64 activityID;
    Int64 caseID;

    protected void Page_Load(object sender, EventArgs e)
    {
        packageID = Convert.ToInt64(Request.QueryString["packageid"]);
        activityID = Convert.ToInt64(Request.QueryString["activityid"]);
        caseID = Convert.ToInt64(Request.QueryString["caseID"]);

        Package package = new Package(packageID);

        litHeader.Text = package.Title;



        FillDetailsTable(package);

        CreateListParts();

        lnkAddPart.Visible = !package.isUsedInWaive();
        lnkCopyPackage.Visible = package.isUsedInWaive() && !package.hasBeenRevised();
        panEdit.Visible = !package.hasBeenRevised();

        if (!Page.IsPostBack)
        {
            FillDdlParts();
            FillEditTexts(package);
        }
    }

    private void FillDetailsTable(Package package)
    {

        litDetailsPriceValue.Text = package.GetTotalPrice().ToString("N2");

        if (package.OriginalPackageID > 0)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter writer = new HtmlTextWriter(sw))
                {
                    Package orgPackage = new Package(package.OriginalPackageID);
                    writer.AddAttribute("class", "revision");
                    writer.AddAttribute("href", Urls.PackageUrl + "?caseid=" + caseID + "&packageid=" + orgPackage.ID + "&activityid=" + orgPackage.ActivityID + "&pagetype=package");
                    writer.RenderBeginTag(HtmlTextWriterTag.A);
                    writer.Write(orgPackage.Title + " (" + Convert.ToDateTime(package.RevisionDate).ToShortDateString() + ")");
                    writer.RenderEndTag(); // Span
                }
                litRevision.Text = sw.ToString();
            }
        }
        else
        {
            panRevison.Visible = false;
        }

    }

    private void FillEditTexts(Package package)
    {
        txtPackageTitle.Text = package.Title;
    }

    private void FillDdlParts()
    {
        ActivityCollection activities = Activity.Utils.GetActitvitiesByCaseID(caseID);

        foreach (var activity in activities)
        {

            StructureCollection structures = Structure.Utils.GetStructuresByActivityID(activity.ID);

            foreach (Structure structure in structures)
            {
                PartCollection parts = Part.Utils.GetPartsByStructureID(structure.ID);

                foreach (var part in parts)
                {
                    Contract contract = new Contract(activity.ContractID);
                    ListItem listItem = new ListItem(part.Title, part.ID.ToString());
                    listItem.Attributes["data-optiongroup"] = contract.Title + " - " + activity.Title + " - " +  structure.Title;
                    ddlParts.Items.Add(listItem);
                }
            }

            
        }      
    }




    private void CreateListParts()
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {
                Package package = new Package(packageID);
                PartCollection parts = Part.Utils.GetPartsByPackageID(packageID);

                foreach (var part in parts)
                {
                    MaterialCollection materials = Material.Utils.GetMaterialsByPartID(part.ID);


                    writer.AddAttribute("class", "listview");
                    writer.AddAttribute("cellspacing", "0");
                    writer.AddAttribute("cellpadding", "0");
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);
                    writer.AddAttribute("class", "header");
                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);                    
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.Write("Produktionsdel: " + part.Title);
                    writer.RenderEndTag(); // Th
                    writer.AddAttribute("class", "delete");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    if (!package.isUsedInWaive())
                    {
                        writer.AddAttribute("class", "fa fa fa-times remove");
                        writer.AddAttribute("onclick", "return removePartfromPackage(" + part.ID + ", '" + part.Title + "');");
                        writer.RenderBeginTag(HtmlTextWriterTag.I);
                        writer.RenderEndTag(); // I  
                    }
                    writer.RenderEndTag(); // Th
                    writer.RenderEndTag(); // Tr

                    writer.RenderEndTag(); // Table


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
                    writer.Write(Resources.MaterialTexts.Material);
                    writer.RenderEndTag(); // Span

                    writer.AddAttribute("class", "amount");
                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                    writer.Write(Resources.PackageTexts.Amount);
                    writer.RenderEndTag(); // Span
                    writer.RenderEndTag(); // Th
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.RenderEndTag(); // Th


                    writer.RenderEndTag(); //Tr
                    foreach (var material in materials)
                    {
                        PackageMaterialAmountCollection materialAmounts = PackageMaterialAmount.Utils.GetPackageMaterialAmountByPackageID_PartID_MaterialID(packageID, part.ID, material.ID);

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
                            decimal length = 0;
                            decimal square = 0;

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
                                    length = (item.Length * item.Amount) / 1000;
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
                                    square = (item.Width * item.Length * item.Amount) / 1000000;
                                }

                                writer.AddAttribute("class", "amount");
                                writer.RenderBeginTag(HtmlTextWriterTag.Span);

                                if (isLength)
                                {
                                    writer.Write("(" + length.ToString("N2") + " Mtr) " + item.Amount.ToString("N2") + " stk.");
                                }
                                else if (isSquare)
                                {
                                    writer.Write("(" + square.ToString("N2") + " M2) " + item.Amount.ToString("N2") + " stk.");
                                }

                                if (!package.isUsedInWaive())
                                {
                                    writer.AddAttribute("class", "fa fa fa-pencil");
                                    writer.AddAttribute("onclick", "openModalEditMaterialAmount(" + part.ID + "," + material.ID + ",'" + item.Amount + "','" + item.Length + "', '" + item.Width + "')");
                                    writer.RenderBeginTag(HtmlTextWriterTag.I);
                                    writer.RenderEndTag(); // I 
                                }
                                writer.RenderEndTag(); // Span

                                writer.RenderEndTag(); // Li

                            }

                            writer.RenderEndTag(); // Ul
                            writer.RenderEndTag(); // Td

                        }
                        else
                        {

                            writer.AddAttribute("class", "amount");
                            writer.RenderBeginTag(HtmlTextWriterTag.Span);
                            writer.Write(materialAmounts.Count() > 0 ? materialAmounts[0].Amount.ToString("N0") : "0");
                            if (!package.isUsedInWaive())
                            {
                                writer.AddAttribute("class", "fa fa fa-pencil");
                                if (materialAmounts.Count() > 0)
                                {
                                    writer.AddAttribute("onclick", "openModalEditMaterialAmount(" + part.ID + "," + material.ID + ",'" + materialAmounts[0].Amount.ToString() + "',0,0)");
                                }
                                else
                                {
                                    writer.AddAttribute("onclick", "openModalAddMaterialAmount(" + part.ID + "," + material.ID + ", 0 ," + material.UnitType + ")");
                                }
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
                            if (!package.isUsedInWaive())
                            {
                                writer.AddAttribute("class", "fa fa fa-plus");
                                writer.AddAttribute("onclick", "openModalAddMaterialAmount(" + part.ID + "," + material.ID + ", 0 ," + material.UnitType + ")");
                                writer.RenderBeginTag(HtmlTextWriterTag.I);
                                writer.RenderEndTag(); // I 
                            }
                            writer.RenderEndTag(); // Td
                        }
                        writer.RenderEndTag(); //Tr
                    }
                    writer.RenderEndTag(); // Table
                }
            }
            litListParts.Text = sw.ToString();
        }
    }


    protected void btnAddPart_Click(object sender, EventArgs e)
    {
        Package package = new Package(packageID);
        package.AddPart(Convert.ToInt32(ddlParts.SelectedValue));
        Response.Redirect(Request.RawUrl);
    }

    protected void btnAddMaterialAmount_Click(object sender, EventArgs e)
    {
        PackageMaterialAmount materialAmount = new PackageMaterialAmount();
        materialAmount.Amount = Convert.ToDecimal(txtAmount.Text);

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

        materialAmount.PackageID = packageID;
        materialAmount.PartID = Convert.ToInt64(hidPartID.Value);
        materialAmount.MaterialID = Convert.ToInt64(hidMaterialID.Value);
        materialAmount.Add();
        Response.Redirect(Request.RawUrl);
    }


    protected void btnRemovePart_Click(object sender, EventArgs e)
    {
        Package package = new Package(packageID);
        package.RemovePart(Convert.ToInt64(hidPartID.Value));
        Response.Redirect(Request.RawUrl);
    }

    protected void btnEditMatrialAmount_Click(object sender, EventArgs e)
    {
        long partID = Convert.ToInt64(hidPartID.Value);
        long materialID = Convert.ToInt64(hidMaterialID.Value);
        decimal length = Convert.ToDecimal(hidLength.Value);
        decimal width = Convert.ToDecimal(hidWidth.Value);
        PackageMaterialAmount materialAmount = new PackageMaterialAmount(packageID, partID, materialID, length, width);
        materialAmount.Amount = Convert.ToDecimal(txtEditAmount.Text);
        materialAmount.Save();
        Response.Redirect(Request.RawUrl);
    }

    protected void lnkCopyPackage_Click(object sender, EventArgs e)
    {
        Package oldPackage = new Package(packageID);

        Int64 newPackageID = oldPackage.CopyPackage();
        if (newPackageID > 0)
        {
            Response.Redirect(Urls.PackageUrl + "?caseid=" + caseID + "&packageid=" + newPackageID + "&activityid=" + activityID + "&pagetype=package");
        }


    }

    protected void btnSavePackage_Click(object sender, EventArgs e)
    {
        Package package = new Package(packageID);
        package.Title = txtPackageTitle.Text;
        package.ActivityID = 0;
        package.Save();
        Response.Redirect(Urls.PackageUrl + "?caseid=" + caseID + "&packageid=" + package.ID + "&activityid=" + package.ActivityID + "&pagetype=package");
    }
}
