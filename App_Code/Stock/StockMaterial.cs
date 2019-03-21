using Juinti.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for StockOrderedMaterial
/// </summary>
namespace Juinti.Case
{
    public class StockOrderedMaterial
    {
        public Int64 ID { get; set; }
        public Int64 CaseID { get; set; }
        public Int64 MaterialID { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Amount { get; set; }
        public decimal PricePerUnit { get; set; }
        public DateTime DateOrdered { get; set; }

        public StockOrderedMaterial()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.ID = 0;
            this.CaseID = 0;
            this.MaterialID = 0;
            this.Length = 0;
            this.Width = 0;
            this.Amount = 0;
            this.PricePerUnit = 0;
            this.DateOrdered = DateTime.Now;
        }
        public StockOrderedMaterial(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetStockOrderedMaterialByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadStockOrderedMaterialByReader(row, this);
                }
            }
        }

        public bool Save()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("ID", this.ID));
                Params.Add(new SqlParameter("CaseID", this.CaseID));
                Params.Add(new SqlParameter("MaterialID", this.MaterialID));
                Params.Add(new SqlParameter("Length", this.Length));
                Params.Add(new SqlParameter("Width", this.Width));
                Params.Add(new SqlParameter("Amount", this.Amount));
                Params.Add(new SqlParameter("PricePerUnit", this.PricePerUnit));

                this.ID = Execute.Scalar(StoredProcedures.SaveOrdered, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static class Utils
        {
            public static void LoadStockOrderedMaterialByReader(DataRow row, StockOrderedMaterial o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.CaseID = Convert.ToInt64(row["CaseID"]);
                o.MaterialID = Convert.ToInt64(row["MaterialID"]);
                o.Length = Convert.ToDecimal(row["Length"]);
                o.Width = Convert.ToDecimal(row["Width"]);
                o.Amount = Convert.ToDecimal(row["Amount"]);
                o.PricePerUnit = Convert.ToDecimal(row["PricePerUnit"]);
                o.DateOrdered = Convert.ToDateTime(row["DateOrdered"]);
            }
        }
        public static class StoredProcedures
        {
            public static string SaveOrdered
            {
                get
                {
                    return "case_Stock_SaveOrdered";
                }
            }
            public static string GetStockOrderedMaterialByID
            {
                get
                {
                    return "case_Stock_GetStockOrderedMaterialByID";
                }
            }
        }
    }
    public class StockOrderedMaterialCollection : List<StockOrderedMaterial> { }

}