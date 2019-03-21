using Juinti.Case;
using Juinti.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialReportDTO
/// </summary>
/// 
namespace Juinti.Report
{
    public class MaterialReportDTO
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public int UnitTypeID { get; set; }
        public int TypeID { get; set; } // Package=1, Stock=2
        public decimal TotalAmount { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Totalprice { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public bool IsOrdered { get; set; }


        public static class Utils
        {

            public static void LoadMaterialDTOByReader(DataRow row, MaterialReportDTO o)
            {
                o.ID = Convert.ToInt64(row["MaterialID"]);
                o.Title = row["Title"].ToString();
                o.Length = Convert.ToDecimal(row["Length"]);
                o.Width = Convert.ToDecimal(row["Width"]);
                o.UnitTypeID = Convert.ToInt32(row["UnitTypeID"]);
                o.TypeID = Convert.ToInt32(row["TypeID"]);
                o.TotalAmount = Convert.ToDecimal(row["TotalMaterialAmount"]);
                o.UnitPrice = Convert.ToDecimal(row["AverageUnitPrice"]);
                o.Totalprice = Convert.ToDecimal(row["TotalPrice"]);
                o.Year = Convert.ToInt32(row["Year"]);
                o.Month = Convert.ToInt32(row["Month"]);
                o.IsOrdered = Convert.ToBoolean(row["IsOrdered"]);
            }


            public static MaterialReportDTOCollection GetAllWaiveMaterialsByDate(long CaseID, DateTime? DateFrom, DateTime? DateTo, long? ContractID, long? ActivityID, long? PartID, long? MaterialID, bool? IsOrdered)
            {
                MaterialReportDTOCollection c = new MaterialReportDTOCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("CaseID", CaseID));
                if (DateFrom != null)
                {
                    Params.Add(new SqlParameter("DateFrom", DateFrom));
                }
                if (DateTo != null)
                {
                    Params.Add(new SqlParameter("DateTo", DateTo));
                }
                if (ContractID != null)
                {
                    Params.Add(new SqlParameter("ContractID", ContractID));
                }
                if (ActivityID != null)
                {
                    Params.Add(new SqlParameter("ActivityID", ActivityID));
                }
                if (PartID != null)
                {
                    Params.Add(new SqlParameter("PartID", PartID));
                }
                if (MaterialID != null)
                {
                    Params.Add(new SqlParameter("MaterialID", MaterialID));
                }
                if (IsOrdered != null)
                {
                    Params.Add(new SqlParameter("IsOrdered", IsOrdered));

                }


                DataTable dt = Execute.FillDataTable(StoredProcedures.GetAllWaiveMaterialsByDate, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        MaterialReportDTO o = new MaterialReportDTO();
                        LoadMaterialDTOByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }
        }
    }


    public class TotalPriceReportDTO
    {
        public decimal Totalprice { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        public static class Utils
        {

            public static void LoadTotalPriceDTOByReader(DataRow row, TotalPriceReportDTO o)
            {
                o.Totalprice = Convert.ToDecimal(row["TotalPrice"]);
                o.Year = Convert.ToInt32(row["Year"]);
                o.Month = Convert.ToInt32(row["Month"]);
            }


            public static TotalPriceReportDTOCollection GetTotalPricesByDate(long CaseID, DateTime? DateFrom, DateTime? DateTo, long? ContractID, long? ActivityID, long? PartID, long? MaterialID, bool? IsOrdered)
            {
                TotalPriceReportDTOCollection c = new TotalPriceReportDTOCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("CaseID", CaseID));
                if (DateFrom != null)
                {
                    Params.Add(new SqlParameter("DateFrom", DateFrom));
                }
                if (DateTo != null)
                {
                    Params.Add(new SqlParameter("DateTo", DateTo));
                }
                if (ContractID != null)
                {
                    Params.Add(new SqlParameter("ContractID", ContractID));
                }
                if (ActivityID != null)
                {
                    Params.Add(new SqlParameter("ActivityID", ActivityID));
                }
                if (PartID != null)
                {
                    Params.Add(new SqlParameter("PartID", PartID));
                }
                if (MaterialID != null)
                {
                    Params.Add(new SqlParameter("MaterialID", MaterialID));
                }
                if (IsOrdered != null)
                {
                    Params.Add(new SqlParameter("IsOrdered", IsOrdered));

                }


                DataTable dt = Execute.FillDataTable(StoredProcedures.GetTotalPricesByDate, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        TotalPriceReportDTO o = new TotalPriceReportDTO();
                        LoadTotalPriceDTOByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

            public static TotalPriceReportDTOCollection GetTotalPricesGroupedByMonth(long CaseID, DateTime? DateFrom, DateTime? DateTo)
            {
                TotalPriceReportDTOCollection c = new TotalPriceReportDTOCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("CaseID", CaseID));
                if (DateFrom != null)
                {
                    Params.Add(new SqlParameter("DateFrom", DateFrom));
                }
                if (DateTo != null)
                {
                    Params.Add(new SqlParameter("DateTo", DateTo));
                }

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetTotalPricesGroupedByMonth, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        TotalPriceReportDTO o = new TotalPriceReportDTO();
                        LoadTotalPriceDTOByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }
            public static TotalPriceReportDTOCollection GetTotalPricesGroupedByMonthByMaterialID(long CaseID, DateTime? DateFrom, DateTime? DateTo, Int64 MaterialID)
            {
                TotalPriceReportDTOCollection c = new TotalPriceReportDTOCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("CaseID", CaseID));
                Params.Add(new SqlParameter("MaterialID", MaterialID));
                if (DateFrom != null)
                {
                    Params.Add(new SqlParameter("DateFrom", DateFrom));
                }
                if (DateTo != null)
                {
                    Params.Add(new SqlParameter("DateTo", DateTo));
                }

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetTotalPricesGroupedByMonthByMaterialID, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        TotalPriceReportDTO o = new TotalPriceReportDTO();
                        LoadTotalPriceDTOByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }
            public static TotalPriceReportDTOCollection GetTotalPricesGroupedByMonthByPartID(long CaseID, DateTime? DateFrom, DateTime? DateTo, Int64 PartID)
            {
                TotalPriceReportDTOCollection c = new TotalPriceReportDTOCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("CaseID", CaseID));
                Params.Add(new SqlParameter("PartID", PartID));
                if (DateFrom != null)
                {
                    Params.Add(new SqlParameter("DateFrom", DateFrom));
                }
                if (DateTo != null)
                {
                    Params.Add(new SqlParameter("DateTo", DateTo));
                }

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetTotalPricesGroupedByMonthByPartID, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        TotalPriceReportDTO o = new TotalPriceReportDTO();
                        LoadTotalPriceDTOByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

            public static TotalPriceReportDTOCollection GetTotalPricesGroupedByMonthByActivityID(long CaseID, DateTime? DateFrom, DateTime? DateTo, Int64 ActivityID)
            {
                TotalPriceReportDTOCollection c = new TotalPriceReportDTOCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("CaseID", CaseID));
                Params.Add(new SqlParameter("ActivityID", ActivityID));
                if (DateFrom != null)
                {
                    Params.Add(new SqlParameter("DateFrom", DateFrom));
                }
                if (DateTo != null)
                {
                    Params.Add(new SqlParameter("DateTo", DateTo));
                }

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetTotalPricesGroupedByMonthByActivityID, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        TotalPriceReportDTO o = new TotalPriceReportDTO();
                        LoadTotalPriceDTOByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }
        }
    }


    public static class StoredProcedures
    {
        public static string GetAllWaiveMaterialsByDate
        {
            get
            {
                return "case_Materials_GetAllWaiveMaterialsByDate";
            }
        }
        public static string GetTotalPricesByDate
        {
            get
            {
                return "case_Materials_GetTotalPricesByDate";
            }
        }

        public static string GetTotalPricesGroupedByMonth
        {
            get
            {
                return "case_Report_GetTotalPricesGroupedByMonth";
            }
        }

        public static string GetTotalPricesGroupedByMonthByMaterialID
        {
            get
            {
                return "case_Report_GetTotalPricesGroupedByMonthByMaterialID";
            }
        }
        public static string GetTotalPricesGroupedByMonthByPartID
        {
            get
            {
                return "case_Report_GetTotalPricesGroupedByMonthByPartID";
            }
        }
        public static string GetTotalPricesGroupedByMonthByActivityID
        {
            get
            {
                return "case_Report_GetTotalPricesGroupedByMonthByActivityID";
            }
        }
    }

    public class MaterialReportDTOCollection : List<MaterialReportDTO> { }
    public class TotalPriceReportDTOCollection : List<TotalPriceReportDTO> { }
}