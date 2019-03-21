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
namespace Juinti.Stock
{
    public class MaterialStockDTO
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public int UnitTypeID { get; set; }
        public int TypeID { get; set; } // Package=1, Stock=2
        public decimal TotalAmount { get; set; }
        public int CompanyID { get; set; }

        //public decimal Totalprice { get; set; }
        //public int? Year { get; set; }
        //public int? Month { get; set; }


        public static class Utils
        {

            public static void LoadMaterialDTOByReader(DataRow row, MaterialStockDTO o)
            {
                o.ID = Convert.ToInt64(row["MaterialID"]);
                o.Title = row["Title"].ToString();
                o.UnitTypeID = Convert.ToInt32(row["UnitTypeID"]);
                o.TypeID = Convert.ToInt32(row["TypeID"]);
                o.TotalAmount = Convert.ToDecimal(row["TotalMaterialAmount"]);
                o.CompanyID = Convert.ToInt32(row["CompanyID"]);
                //o.Totalprice = Convert.ToDecimal(row["TotalPrice"]);
                //o.Year = Convert.ToInt32(row["Year"]);
                //o.Month = Convert.ToInt32(row["Month"]);
            }


            public static MaterialStockDTOCollection GetAllWaiveMaterials(long CaseID, DateTime? DateFrom, DateTime? DateTo, long? ContractID, long? ActivityID, long? PartID, long? MaterialID, bool? IsOrdered)
            {
                MaterialStockDTOCollection c = new MaterialStockDTOCollection();

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

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetTotalStockWaiveMaterials, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        MaterialStockDTO o = new MaterialStockDTO();
                        LoadMaterialDTOByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }
        }
    }
    public static class StoredProcedures
    {
        public static string GetTotalStockWaiveMaterials
        {
            get
            {
                return "case_Stock_GetTotalStockWaiveMaterials";
            }
        }
        public static string GetStockWaiveMaterialsGroupedByYearMonth
        {
            get
            {
                return "case_Stock_GetStockWaiveMaterialsGroupedByYearMonth";
            }
        }
    }

    public class MaterialStockDTOCollection : List<MaterialStockDTO> { }
}