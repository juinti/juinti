using Juinti.Case;
using Juinti.Contacts;
using Juinti.Lists;
using Juinti.Variables;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WaiveEmailPage : System.Web.UI.Page
{
    Int64 caseID = 0;
    Int64 waiveID = 0;
    Int64 packageID = 0;
    string pageType = "";
    bool isProductionPage = false;
    bool isInProduction = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        caseID = Convert.ToInt64(Request.QueryString["caseid"]);
        waiveID = Convert.ToInt64(Request.QueryString["waiveid"]);
        packageID = Convert.ToInt64(Request.QueryString["packageid"]);
        pageType = Request.QueryString["pagetype"];


        Waive waive = new Waive(waiveID);

        panButtons.Visible = !waive.IsOrdered;
        panCancel.Visible = waive.IsOrdered;

        linkPackages.NavigateUrl = Urls.WaiveUrl + "?caseid=" + caseID + "&waiveid=" + waiveID + "&pagetype=waive";
        linkDetails.NavigateUrl = Urls.WaiveEmailUrl + "?caseid=" + caseID + "&waiveid=" + waiveID + "&pagetype=waive";

        isProductionPage = (!String.IsNullOrEmpty(pageType) && pageType.Equals("production") && packageID > 0);

        isInProduction = NextOrderPackage.Utils.IsInProduction(waiveID, packageID);
        btnSend.Visible = !isProductionPage && !waive.IsOrdered;
        btnProduction.Visible = isProductionPage && !isInProduction;
        btnRemoveProduction.Visible = isProductionPage && isInProduction;

        btnResetChanges.Visible = Waive.Utils.IsWaiveChanged(waive.ID);


        Milestone milestone = new Milestone(waive.MilestoneID);

        litHeader.Text = waive.Title + " - " + waive.Location + " - " + milestone.Title;

        if (!Page.IsPostBack)
        {
            FillDdlMaterials();
            FillDdlMilestones(waive);
            FillValues(waive);
            CreateEmailTemplate(waive, false);
        }
        litEmail.Text = CreateEmailTemplate(waive, false).ToString();
    }


    private StringWriter CreateEmailTemplate(Waive waive, Boolean isEmail)
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter writer = new HtmlTextWriter(sw))
            {

                #region Address

                Case newCase = new Case(caseID);


                writer.AddAttribute("cellspacing", "0");
                writer.AddAttribute("cellpadding", "0");
                writer.RenderBeginTag(HtmlTextWriterTag.Table);


                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute("style", "text-align:left; vertical-align:top; padding:5px;");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.WaiveTexts.CaseNumber);
                writer.RenderEndTag(); // Th
                writer.AddAttribute("style", "text-align:left; vertical-align:top; padding:5px;");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(newCase.Number);
                writer.RenderEndTag(); // Td
                writer.RenderEndTag(); // Tr

                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute("style", "text-align:left; vertical-align:top;  padding:5px;");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.WaiveTexts.CaseName);
                writer.RenderEndTag(); // Th
                writer.AddAttribute("style", "text-align:left; vertical-align:top; padding:5px;");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(newCase.Name);
                writer.RenderEndTag(); // Td
                writer.RenderEndTag(); // Tr

                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute("style", "text-align:left; vertical-align:top;padding:5px; ");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write(Resources.WaiveTexts.CaseDeliveryAddress);
                writer.RenderEndTag(); // Th
                writer.AddAttribute("style", "text-align:left; vertical-align:top; padding:5px;");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(newCase.Street);
                writer.Write(" - ");
                writer.Write(newCase.ZipCode + " " + newCase.City);
                writer.RenderEndTag(); // Td
                writer.RenderEndTag(); // Tr


                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                writer.AddAttribute("style", "text-align:left; vertical-align:top; padding:5px;");
                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                writer.Write("Leveringsdato: ");
                writer.RenderEndTag(); // Th
                writer.AddAttribute("style", "text-align:left; vertical-align:top; padding:5px;");
                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                writer.Write(waive.EstOrderDate.ToShortDateString());
                writer.RenderEndTag(); // Td

                writer.RenderEndTag(); // Tr


                writer.RenderEndTag(); // Table

                #endregion

                #region Packages 

                List<Package> packages = null;

                if (isProductionPage)
                {
                    packages = Package.Utils.GetPackagesByWaiveID(waiveID).Where(x => x.ID == packageID).ToList();
                }
                else
                {
                    packages = Package.Utils.GetPackagesByWaiveID(waiveID);
                }

                if (packages.Count() > 0)
                {

                    Milestone milestone = new Milestone(waive.MilestoneID);
                    CompanyCollection companies = Company.Utils.GetCompanies();
                    foreach (var company in companies)
                    {
                        bool first = true;
                        decimal invoicePrice = 0;

                        foreach (var package in packages)
                        {
                            decimal totalPackagePrice = 0;
                            MaterialCollection PackageMaterials = Material.Utils.GetMaterialsByPackageID_CompanyID_TypeID(package.ID, company.ID, 1);
                            if (PackageMaterials.Count() > 0)
                            {

                                if (first)
                                {
                                    writer.AddAttribute("style", "border:1px solid; padding:10px; margin-top:20px;");
                                    writer.RenderBeginTag(HtmlTextWriterTag.Div);
                                    writer.AddAttribute("style", "margin-bottom:0;margin-top:20px; float:none;");
                                    writer.RenderBeginTag(HtmlTextWriterTag.H2);
                                    writer.Write(company.Name);
                                    writer.RenderEndTag(); // H2   
                                    first = false;

                                }

                                writer.AddAttribute("style", "margin-bottom:10px ;margin-top:40px;");
                                writer.RenderBeginTag(HtmlTextWriterTag.H3);
                                writer.Write(package.WaiveAmount + " " + Resources.WaiveTexts.PackageAmountSuffix + " " + "<span style='font-weight:900; font-size:14px; background:yellow;'>Mrk.: " + package.Title + " - " + waive.Location + " - "+ milestone.Title+ " </span>");
                                writer.RenderEndTag();
                                writer.AddAttribute("cellspacing", "0");
                                writer.AddAttribute("cellpadding", "0");
                                writer.AddAttribute("style", "width:100%;");
                                writer.RenderBeginTag(HtmlTextWriterTag.Table);
                                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                                writer.AddAttribute("style", "font-weight:bold; text-align:left; padding:5px; border-bottom:1px solid #000;");
                                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                                writer.Write("Varenr.");
                                writer.RenderEndTag(); // Th
                                writer.AddAttribute("style", "font-weight:bold; text-align:left; border-bottom:1px solid #000; padding:5px; ");
                                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                                writer.Write("Beskrivelse");
                                writer.RenderEndTag(); // Th

                                writer.AddAttribute("style", "text-align:right; border-bottom:1px solid #000; padding:15px;");
                                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                                writer.Write("Mængde/Enhed");
                                writer.RenderEndTag(); // Th
                                writer.AddAttribute("style", "text-align:right; border-bottom:1px solid #000; padding:15px;");
                                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                                writer.RenderEndTag();
                                if (waive.IsOrdered)
                                {
                                    writer.AddAttribute("style", "text-align:right; border-bottom:1px solid #000; padding:15px;");
                                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                                    writer.Write("Pris");
                                    writer.RenderEndTag(); // Th

                                    writer.AddAttribute("style", "text-align:right;  border-bottom:1px solid #000; padding:15px;");
                                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                                    writer.Write("Pris Total");
                                    writer.RenderEndTag(); // Th
                                }
                                writer.RenderEndTag(); // Tr

                                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                                writer.AddAttribute("style", "text-align:left;");
                                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                                writer.Write("");
                                writer.RenderEndTag(); // Th 

                                writer.AddAttribute("style", "text-align:right;");
                                writer.RenderBeginTag(HtmlTextWriterTag.Th);
                                writer.Write("");
                                writer.RenderEndTag(); // Th         

                                if (waive.IsOrdered)
                                {
                                    writer.AddAttribute("style", "text-align:right;");
                                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                                    writer.Write("");
                                    writer.RenderEndTag(); // Th 
                                    writer.AddAttribute("style", "text-align:right;");
                                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                                    writer.Write("");
                                    writer.RenderEndTag(); // Th 
                                }
                                writer.RenderEndTag(); // tr
                                foreach (var material in PackageMaterials)
                                {

                                    PackageMaterialAmountDTOCollection materialAmounts = PackageMaterialAmountDTO.Utils.GetPackageMaterialAmountByPackageIDMaterialID(package.ID, material.ID);
                                    decimal totalPrice = 0;
                                    bool isLength = material.UnitType == 1 ? true : false;
                                    bool isSquare = material.UnitType == 2 ? true : false;


                                    if (isLength || isSquare)
                                    {
                                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                                        writer.AddAttribute("style", "font-weight:bold; padding-left:5px;");
                                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                        writer.Write(material.Number);
                                        writer.RenderEndTag(); // Td
                                        writer.AddAttribute("style", "padding-left:5px;");
                                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                        writer.Write("<span style='font-weight:bold;'>" + material.Title + "</span> ");

                                        if (waive.IsOrdered)
                                        {

                                        }


                                        foreach (var item in materialAmounts)
                                        {
                                            decimal length = 0;
                                            decimal square = 0;

                                            WaiveEditMaterialAmount waiveEditMaterialAmount = new WaiveEditMaterialAmount(waiveID, package.ID, material.ID, item.Length, item.Width);

                                            if (waiveEditMaterialAmount.ID > 0)
                                            {
                                                item.Amount = waiveEditMaterialAmount.Amount;
                                            }

                                            if (isLength)
                                            {
                                                writer.Write(" - " + item.Length.ToString("0") + "mm");

                                                length = (item.Length * item.Amount) / 1000;

                                                totalPrice = length * material.Price.Price;

                                                if (!Page.IsPostBack)
                                                {
                                                    ListItem listItem = new ListItem(material.Title + " X " + item.Length.ToString("0") + "mm", package.ID + "-" + material.ID.ToString());
                                                    listItem.Attributes["data-optiongroup"] = package.Title;
                                                    ddlMaterial.Items.Add(listItem);
                                                }

                                            }
                                            else if (isSquare)
                                            {

                                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                                writer.Write(" - " + item.Length.ToString("0") + "mm");
                                                writer.RenderEndTag();
                                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                                writer.Write(" x ");
                                                writer.RenderEndTag();
                                                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                                writer.Write(item.Width.ToString("0") + "mm");
                                                writer.RenderEndTag();

                                                square = (item.Width * item.Length * item.Amount) / 1000000;
                                                totalPrice = square * material.Price.Price;

                                                if (!Page.IsPostBack)
                                                {
                                                    ListItem listItem = new ListItem(material.Title + " " + item.Length.ToString("0") + "mm X " + item.Width.ToString("0") + "mm", package.ID + "-" + material.ID.ToString());
                                                    listItem.Attributes["data-optiongroup"] = package.Title;
                                                    ddlMaterial.Items.Add(listItem);
                                                }
                                            }
                                            writer.RenderEndTag(); // Td   


                                           

                                            writer.AddAttribute("style", "text-align:right; padding:15px;");
                                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                            if (isLength)
                                            {
                                                writer.Write("(" + length.ToString("N2") + " Mtr) " + item.Amount.ToString("N2") + " stk.");
                                            }
                                            else if (isSquare)
                                            {
                                                writer.Write("(" + square.ToString("N2") + " M2) " + item.Amount.ToString("N2") + " stk.");
                                            }

                                            writer.RenderEndTag(); // Td  
                                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                            if (!isEmail)
                                            {
                                                writer.AddAttribute("class", "fa fa fa-pencil");
                                                writer.AddAttribute("onclick", "openModalEditWaiveMaterialAmount("+package.ID +", " + material.ID + ", '" + item.Amount + "','" + item.Length + "', '" + item.Width + "')");
                                                writer.RenderBeginTag(HtmlTextWriterTag.I);
                                                writer.RenderEndTag(); // I 
                                            }
                                            writer.RenderEndTag();

                                            if (waive.IsOrdered)
                                            {
                                                writer.AddAttribute("style", "text-align:right; padding:15px;");
                                                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                                writer.Write(material.Price.Price.ToString("N2"));
                                                writer.RenderEndTag(); // Td  

                                                writer.AddAttribute("style", "text-align:right; padding:15px;");
                                                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                                writer.Write(totalPrice.ToString("N2"));
                                                writer.RenderEndTag(); // Td
                                            }
                                            writer.RenderEndTag(); // Tr

                                        }
                                    }
                                    else
                                    {

                                        totalPrice = (materialAmounts.Count() > 0 ? materialAmounts[0].Amount : 0) * material.Price.Price;
                                        WaiveEditMaterialAmount waiveEditMaterialAmount = new WaiveEditMaterialAmount(waiveID, package.ID, material.ID, 0, 0);

                                        if (waiveEditMaterialAmount.ID > 0)
                                        {
                                            materialAmounts[0].Amount = waiveEditMaterialAmount.Amount;
                                        }

                                        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                                        writer.AddAttribute("style", "font-weight:bold; padding-left:5px;");
                                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                        writer.Write(material.Number);
                                        writer.RenderEndTag(); // Td
                                        writer.AddAttribute("style", "font-weight:bold; padding-left:5px;");
                                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                        writer.Write(material.Title);
                                        writer.RenderEndTag(); // Td       

                                        writer.AddAttribute("style", "text-align:right; padding:15px;");
                                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                        writer.Write((materialAmounts.Count() > 0 ? materialAmounts[0].Amount.ToString("N2") : "0,00") + " stk.");
                                        writer.RenderEndTag(); // Td  
                                        writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                        if (!isEmail)
                                        {
                                            writer.AddAttribute("class", "fa fa fa-pencil");
                                            writer.AddAttribute("onclick", "openModalEditWaiveMaterialAmount(" + package.ID + ", " + material.ID + ",'" + materialAmounts[0].Amount + "','0', '0')");
                                            writer.RenderBeginTag(HtmlTextWriterTag.I);
                                            writer.RenderEndTag(); // I 
                                        }
                                        writer.RenderEndTag();
                                        if (waive.IsOrdered)
                                        {
                                            writer.AddAttribute("style", "text-align:right; padding:15px;");
                                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                            writer.Write(material.Price.Price.ToString("N2"));
                                            writer.RenderEndTag(); // Td  

                                            writer.AddAttribute("style", "text-align:right; padding:15px;");
                                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                            writer.Write(totalPrice.ToString("N2"));
                                            writer.RenderEndTag(); // Td  
                                        }
                                        writer.RenderEndTag(); // Tr

                                    }
                                    totalPackagePrice += totalPrice;
                                }


                                writer.RenderEndTag(); // Table


                            }

                            invoicePrice += totalPackagePrice;
                        }
                        if (invoicePrice > 0 && !isEmail)
                        {

                            writer.AddAttribute("style", "text-align:right; padding:15px; border-top:1px solid; margin-top:20px; font-weight:bold;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Div);
                            writer.Write(company.Name + " i alt: " + invoicePrice.ToString("N2"));
                            writer.RenderEndTag();
                            writer.RenderEndTag(); // Div
                        }

                    }

                }

                #endregion
                //if (!isProductionPage)
                //{
                //    #region Stock
                //    WaivePackageStockMaterialCollection packageStockaterials = WaivePackageStockMaterial.Utils.GetStockWaiveMaterialsByWaiveID(waiveID);
                //    if (packageStockaterials.Count() > 0)
                //    {

                //        writer.AddAttribute("cellspacing", "0");
                //        writer.AddAttribute("cellpadding", "0");
                //        writer.AddAttribute("style", "width:100%; border:1px solid #000; margin-top:10px;");
                //        writer.RenderBeginTag(HtmlTextWriterTag.Table);
                //        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                //        writer.AddAttribute("style", "font-weight:bold; text-align:left; width:100%; background:lightgray; font-size:18px; padding:5px;");
                //        writer.RenderBeginTag(HtmlTextWriterTag.Th);
                //        writer.Write(Resources.WaiveTexts.Stock);
                //        writer.RenderEndTag(); // Th

                //        writer.AddAttribute("style", "text-align:right;background:lightgray; padding:5px;");
                //        writer.RenderBeginTag(HtmlTextWriterTag.Th);
                //        writer.Write(Resources.PackageTexts.Amount);
                //        writer.RenderEndTag(); // Th
                //        writer.RenderEndTag(); // Tr                             

                //        writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                //        writer.AddAttribute("style", "width:100%;");
                //        writer.AddAttribute("style", "text-align:left;");
                //        writer.RenderBeginTag(HtmlTextWriterTag.Th);
                //        writer.Write("");
                //        writer.RenderEndTag(); // Th 

                //        writer.AddAttribute("style", "text-align:right;");
                //        writer.RenderBeginTag(HtmlTextWriterTag.Th);
                //        writer.Write("");
                //        writer.RenderEndTag(); // Th               
                //        writer.RenderEndTag(); // tr

                //        Int64 prevID = 0;
                //        Int64 currID = 0;
                //        foreach (var material in packageStockaterials)
                //        {
                //            currID = material.MaterialID;


                //            bool isLength = material.UnitTypeID == 1 ? true : false;
                //            bool isSquare = material.UnitTypeID == 2 ? true : false;


                //            if (isLength || isSquare)
                //            {
                //                if (prevID != currID)
                //                {
                //                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                //                    writer.AddAttribute("style", "font-weight:bold; padding-left:5px; border-top:1px solid;");
                //                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                //                    writer.Write(material.Title);
                //                    writer.RenderEndTag(); // Td
                //                    writer.AddAttribute("style", "padding-left:5px; border-top:1px solid;  padding:5px;");
                //                    writer.RenderBeginTag(HtmlTextWriterTag.Td);
                //                    writer.Write("");
                //                    writer.RenderEndTag(); // Td
                //                    writer.RenderEndTag(); //Tr        
                //                }

                //                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                //                writer.AddAttribute("style", "padding-left:20px; border-top:1px dashed;");
                //                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                //                if (isLength)
                //                {
                //                    writer.Write(" - " + material.Length.ToString("0") + "mm");
                //                }
                //                else if (isSquare)
                //                {
                //                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                //                    writer.Write(" - " + material.Length.ToString("0") + "mm");
                //                    writer.RenderEndTag();
                //                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                //                    writer.Write(" x ");
                //                    writer.RenderEndTag();
                //                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                //                    writer.Write(material.Width.ToString("0") + "mm");
                //                    writer.RenderEndTag();
                //                }
                //                writer.RenderEndTag(); // Td   
                //                writer.AddAttribute("style", "text-align:right; padding-right:5px; border-top:1px dashed;");
                //                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                //                writer.Write(material.TotalAmount.ToString("N2"));
                //                writer.RenderEndTag(); // Td
                //                writer.RenderEndTag(); // Tr 

                //            }
                //            else
                //            {
                //                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                //                writer.AddAttribute("style", "font-weight:bold;  padding-left:5px; border-top:1px solid;");
                //                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                //                writer.Write(material.Title);
                //                writer.RenderEndTag(); // Td       
                //                writer.AddAttribute("style", "text-align:right; padding-right:5px; border-top:1px solid;");
                //                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                //                writer.Write(material.TotalAmount.ToString("N2"));
                //                writer.RenderEndTag(); // Td  
                //                writer.RenderEndTag(); // Tr
                //            }

                //            prevID = material.MaterialID;
                //        }
                //        writer.RenderEndTag(); // Table
                //    }
                //    #endregion
                //}

                #region EkstraMaterials
                MaterialCollection ExtraMaterials = Material.Utils.GetMaterialsByWaiveID(waiveID);

                if (ExtraMaterials.Count() > 0)
                {

                    writer.AddAttribute("cellspacing", "0");
                    writer.AddAttribute("cellpadding", "0");
                    writer.AddAttribute("style", "width:100%; border:1px solid #000; margin-top:10px; ");
                    writer.RenderBeginTag(HtmlTextWriterTag.Table);

                    writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                    writer.AddAttribute("style", "font-weight:bold; text-align:left; width:100%; background:lightgray; font-size:18px; padding:5px;");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.Write(Resources.WaiveTexts.ExtraMaterials);
                    writer.RenderEndTag(); // Th 

                    writer.AddAttribute("style", "text-align:right; background:lightgray; padding:5px;");
                    writer.RenderBeginTag(HtmlTextWriterTag.Th);
                    writer.Write(Resources.PackageTexts.Amount);
                    writer.RenderEndTag(); // Th               

                    foreach (var material in ExtraMaterials)
                    {
                        WaiveMaterialAmountCollection materialAmounts = WaiveMaterialAmount.Utils.GetWaiveMaterialAmountByWaiveID_MaterialID(waiveID, material.ID);

                        bool isLength = material.UnitType == 1 ? true : false;
                        bool isSquare = material.UnitType == 2 ? true : false;




                        if (isLength || isSquare)
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                            writer.AddAttribute("style", "font-weight:bold; padding-left:5px; border-top:1px solid;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write(material.Title);
                            writer.RenderEndTag(); // Td
                            writer.AddAttribute("style", "padding-left:5px; border-top:1px solid;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write("");
                            writer.RenderEndTag(); // Td
                            writer.RenderEndTag(); //Tr        

                            foreach (var item in materialAmounts)
                            {
                                writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                                writer.AddAttribute("style", "padding-left:20px; border-top:1px dashed;");
                                writer.RenderBeginTag(HtmlTextWriterTag.Td);

                                decimal length = 0;
                                decimal square = 0;

                                if (isLength)
                                {

                                    writer.Write(" - " + item.Length.ToString("0") + "mm");
                                    length = (item.Length * item.Amount) / 1000;
                                }
                                else if (isSquare)
                                {

                                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                    writer.Write(" - " + item.Length.ToString("0") + "mm");
                                    writer.RenderEndTag();
                                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                    writer.Write(" x ");
                                    writer.RenderEndTag();
                                    writer.RenderBeginTag(HtmlTextWriterTag.Span);
                                    writer.Write(item.Width.ToString("0") + "mm");
                                    writer.RenderEndTag();

                                    square = (item.Width * item.Length * item.Amount) / 1000000;

                                }
                                writer.RenderEndTag(); // Td   
                                writer.AddAttribute("style", "text-align:right; padding-right:5px; border-top:1px dashed;");
                                writer.RenderBeginTag(HtmlTextWriterTag.Td);
                                if (isLength)
                                {
                                    writer.Write("(" + length.ToString("N2") + " Mtr) " + item.Amount.ToString("N2") + " stk.");
                                }
                                else if (isSquare)
                                {
                                    writer.Write("(" + square.ToString("N2") + " M2) " + item.Amount.ToString("N2") + " stk.");
                                }
                                writer.RenderEndTag(); // Td
                                writer.RenderEndTag(); // Tr 
                            }
                        }
                        else
                        {
                            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
                            writer.AddAttribute("style", "font-weight:bold; padding-left:5px; border-top:1px solid;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write(material.Title);
                            writer.RenderEndTag(); // Td       

                            writer.AddAttribute("style", "text-align:right; padding-right:5px; border-top:1px solid;");
                            writer.RenderBeginTag(HtmlTextWriterTag.Td);
                            writer.Write((materialAmounts.Count() > 0 ? materialAmounts[0].Amount.ToString("N2") : "0,00") + " stk.");
                            writer.RenderEndTag(); // Td  

                            writer.RenderEndTag(); // Tr
                        }
                        writer.RenderEndTag(); // Table  
                    }

                }
                #endregion

                return sw;
            }
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
        txtEstDeliveryDate.Text = !waive.EstOrderDate.Date.Equals(DateTime.MaxValue.Date) ? waive.EstOrderDate.ToShortDateString() : "";
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

    protected void btnSend_Click(object sender, EventArgs e)
    {
        Waive waive = new Waive(waiveID);

        Case newCase = new Case(caseID);

        waive.IsOrdered = true;
        waive.OrderDate = DateTime.Now;

        waive.Save();
        Response.Redirect(Request.RawUrl);
        //SmtpClient smtpClient = new SmtpClient();
        //MailMessage mailMessage = new MailMessage();
        //mailMessage.Body = CreateEmailTemplate(waive).ToString();
        //mailMessage.IsBodyHtml = true;
        //mailMessage.To.Add(new MailAddress("anders.mullerup@gmail.com"));
        //mailMessage.Subject = "Afkald: " + newCase.Name + " - " + newCase.Number;


        //smtpClient.Send(mailMessage);
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

    protected void btnResetChanges_Click(object sender, EventArgs e)
    {

        if(WaiveEditMaterialAmount.Utils.DeleteWaiveEditMaterialAmounts(waiveID))
        Response.Redirect(Request.RawUrl);
    }

    

    protected void btnProduction_Click(object sender, EventArgs e)
    {
        NextOrderPackage.Utils.Save(waiveID, packageID);
        Response.Redirect(Request.RawUrl);
    }

    protected void btnRemoveProduction_Click(object sender, EventArgs e)
    {
        NextOrderPackage.Utils.Delete(waiveID, packageID);
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

    protected void btnSendEmail_Click(object sender, EventArgs e)
    {
        Waive waive = new Waive(waiveID);
        Case newCase = new Case(caseID);


        MembershipUser user = Membership.GetUser();

        SmtpClient smtpClient = new SmtpClient();
        MailMessage mailMessage = new MailMessage();
        mailMessage.Body = CreateEmailTemplate(waive, true).ToString();
        mailMessage.IsBodyHtml = true;
        mailMessage.To.Add(new MailAddress("cb@hustomrerne.dk"));
        //mailMessage.To.Add(new MailAddress(user.Email));
        mailMessage.Subject = "Afkald: " + waive.Title + " (" + waive.ID + ") - " + newCase.Name + " - " + newCase.Number;
        smtpClient.Send(mailMessage);
        Response.Redirect(Request.RawUrl);
    }


    protected void btnEditMatrialAmount_Click(object sender, EventArgs e)
    {
        long packageID = Convert.ToInt64(hidPacakageID.Value);
        long materialID = Convert.ToInt64(hidMaterialID.Value);
        decimal length = Convert.ToDecimal(hidLength.Value);
        decimal width = Convert.ToDecimal(hidWidth.Value);
        WaiveEditMaterialAmount waiveEditMaterialAmount = new WaiveEditMaterialAmount();

        waiveEditMaterialAmount.WaiveID = waiveID;
        waiveEditMaterialAmount.PartID = packageID;
        waiveEditMaterialAmount.MaterialID = materialID;
        waiveEditMaterialAmount.Length = length;
        waiveEditMaterialAmount.Width = width;
        waiveEditMaterialAmount.Amount = Convert.ToDecimal(txtEditAmount.Text);
        waiveEditMaterialAmount.Save();
        Response.Redirect(Request.RawUrl);
    }
}