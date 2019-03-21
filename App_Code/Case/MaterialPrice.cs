using Juinti.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialPrice
/// </summary>
/// 

namespace Juinti.Case
{
    public class MaterialPrice
    {
        public Int64 ID { get; set; }
        public string CurrencyISO { get; set; }
        public decimal Price { get; set; }
        public Int64 MaterialID  { get; set; }
        public Int64 CaseID { get; set; }

        public MaterialPrice()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.CurrencyISO = String.Empty;
            this.Price = 0;
        }
        public MaterialPrice(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetMaterialPriceByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadMaterialPriceByReader(row, this);
                }
            }
        }

        public MaterialPrice(Int64 MaterialID, Int64 CaseID, string CurrencyISO)
        {
            Initialize();
            SqlCommand cmd = new SqlCommand();
            SqlParameterCollection Params = cmd.Parameters;
            Params.Add(new SqlParameter("MaterialID", MaterialID));
            Params.Add(new SqlParameter("CaseID", CaseID));
            Params.Add(new SqlParameter("CurrencyISO", CurrencyISO));

            DataTable dt = Execute.FillDataTable(StoredProcedures.GetMaterialPricesByMaterialIDAndCaseIDAndCurrencyISO, Params);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadMaterialPriceByReader(row, this);
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
                Params.Add(new SqlParameter("CurrencyISO", this.CurrencyISO));
                Params.Add(new SqlParameter("Price", this.Price));
                Params.Add(new SqlParameter("MaterialID", this.MaterialID));
                Params.Add(new SqlParameter("CaseID", this.CaseID));

                this.ID = Execute.Scalar(StoredProcedures.SaveMaterialPrice, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static class Utils
        {
            public static void LoadMaterialPriceByReader(DataRow row, MaterialPrice o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.CurrencyISO = row["CurrencyISO"].ToString();
                o.Price = Convert.ToDecimal(row["Price"]);
                o.MaterialID = Convert.ToInt64(row["MaterialID"]);
                o.CaseID = Convert.ToInt64(row["CaseID"]);
            }          
        }
        public static class StoredProcedures
        {
            public static string SaveMaterialPrice
            {
                get
                {
                    return "case_MaterialPrices_SaveMaterialPrice";
                }
            }
            public static string GetMaterialPriceByID
            {
                get
                {
                    return "case_MaterialPrices_GetMaterialPriceByID";
                }
            }
            public static string GetMaterialPricesByMaterialIDAndCaseIDAndCurrencyISO
            {
                get
                {
                    return "case_MaterialPrices_GetMaterialPricesByMaterialIDAndCaseIDAndCurrencyISO";
                }
            }
        }
    }
    public class MaterialPriceCollection : List<MaterialPrice> { }
}