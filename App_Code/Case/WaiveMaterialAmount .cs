using Juinti.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Activity
/// </summary>
/// 

namespace Juinti.Case
{
    public class WaiveMaterialAmount
    {
        public Int64 ID { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Amount { get; set; }
        public Int64 WaiveID { get; set; }
        public Int64 MaterialID { get; set; }
        public WaivePackageStockMaterial packageStockMaterial { get; set; }


        public WaiveMaterialAmount()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Length = 0;
            this.Width = 0;
            this.Amount = 0;
            this.WaiveID = 0;
            this.MaterialID = 0;
        }
        public WaiveMaterialAmount(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetWaiveMaterialAmountByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadWaiveMaterialAmountByReader(row, this);
                }
            }
        }

        public WaiveMaterialAmount(Int64 WaiveID, Int64 MaterialID, decimal Length, decimal Width)
        {
            Initialize();

            SqlCommand cmd = new SqlCommand();
            SqlParameterCollection Params = cmd.Parameters;
            Params.Add(new SqlParameter("WaiveID", WaiveID));
            Params.Add(new SqlParameter("MaterialID", MaterialID));
            Params.Add(new SqlParameter("Length", Length));
            Params.Add(new SqlParameter("Width", Width));

            DataTable dt = Execute.FillDataTable(StoredProcedures.GetWaiveMaterialAmountByWaiveID_MaterialID_Length_Width, Params);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadWaiveMaterialAmountByReader(row, this);
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
                Params.Add(new SqlParameter("Length", this.Length));
                Params.Add(new SqlParameter("Width", this.Width));
                Params.Add(new SqlParameter("Amount", this.Amount));
                Params.Add(new SqlParameter("WaiveID", this.WaiveID));
                Params.Add(new SqlParameter("MaterialID", this.MaterialID));
                this.ID = Execute.Scalar(StoredProcedures.SaveWaiveMaterialAmount, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool Add()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("ID", this.ID));
                Params.Add(new SqlParameter("Length", this.Length));
                Params.Add(new SqlParameter("Width", this.Width));
                Params.Add(new SqlParameter("Amount", this.Amount));
                Params.Add(new SqlParameter("WaiveID", this.WaiveID));
                Params.Add(new SqlParameter("MaterialID", this.MaterialID));
                this.ID = Execute.Scalar(StoredProcedures.AddWaiveMaterialAmount, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static class Utils
        {
            public static void LoadWaiveMaterialAmountByReader(DataRow row, WaiveMaterialAmount o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Length = Convert.ToDecimal(row["Length"]);
                o.Width = Convert.ToDecimal(row["Width"]);
                o.Amount = Convert.ToDecimal(row["Amount"]);
                o.WaiveID = Convert.ToInt64(row["WaiveID"]);
                o.MaterialID = Convert.ToInt64(row["MaterialID"]);
            }

            public static WaiveMaterialAmountCollection GetWaiveMaterialAmountByWaiveID_MaterialID(Int64 WaiveID, Int64 MaterialID)
            {
                WaiveMaterialAmountCollection c = new WaiveMaterialAmountCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("WaiveID", WaiveID));
                Params.Add(new SqlParameter("MaterialID", MaterialID));

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetWaiveMaterialAmountByWaiveID_MaterialID, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        WaiveMaterialAmount o = new WaiveMaterialAmount();
                        LoadWaiveMaterialAmountByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }
        }
        public static class StoredProcedures
        {
            public static string SaveWaiveMaterialAmount
            {
                get
                {
                    return "case_Waive_MaterialAmounts_SaveAmount";
                }
            }
            public static string AddWaiveMaterialAmount
            {
                get
                {
                    return "case_Waive_MaterialAmounts_AddAmount";
                }
            }


            public static string GetWaiveMaterialAmountByID
            {
                get
                {
                    return "case_WaiveMaterialAmounts_GetWaiveMaterialAmountByID";
                }
            }
            public static string GetWaiveMaterialAmountByWaiveID_MaterialID
            {
                get
                {
                    return "case_Waive_MaterialAmounts_GetWaiveMaterialAmountByWaiveID_MaterialID";
                }
            }
            public static string GetWaiveMaterialAmountByWaiveID_MaterialID_Length_Width
            {
                get
                {
                    return "case_Waive_MaterialAmounts_GetWaiveMaterialAmountByWaiveID_MaterialID_Length_Width";
                }
            }

            
        }
    }
    public class WaiveMaterialAmountCollection : List<WaiveMaterialAmount> { }
}