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
    public class WaiveEditMaterialAmount
    {
        public Int64 ID { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Amount { get; set; }
        public Int64 WaiveID { get; set; }
        public Int64 PartID { get; set; }
        public Int64 MaterialID { get; set; }
        public WaivePackageStockMaterial packageStockMaterial { get; set; }


        public WaiveEditMaterialAmount()
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
            this.PartID = 0;
            this.MaterialID = 0;
        }
        public WaiveEditMaterialAmount(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetWaiveEditMaterialAmountByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadEditWaiveMaterialAmountByReader(row, this);
                }
            }
        }

        public WaiveEditMaterialAmount(Int64 WaiveID, Int64 PackageID, Int64 MaterialID, decimal Length, decimal Width)
        {
            Initialize();

            SqlCommand cmd = new SqlCommand();
            SqlParameterCollection Params = cmd.Parameters;
            Params.Add(new SqlParameter("WaiveID", WaiveID));
            Params.Add(new SqlParameter("PartID", PackageID));
            Params.Add(new SqlParameter("MaterialID", MaterialID));
            Params.Add(new SqlParameter("Length", Length));
            Params.Add(new SqlParameter("Width", Width));

            DataTable dt = Execute.FillDataTable(StoredProcedures.GetWaiveEditMaterialAmountByWaiveID_MaterialID_Length_Width, Params);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadEditWaiveMaterialAmountByReader(row, this);
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
                Params.Add(new SqlParameter("PartID", PartID));
                Params.Add(new SqlParameter("MaterialID", this.MaterialID));
                this.ID = Execute.Scalar(StoredProcedures.SaveWaiveEditMaterialAmount, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static class Utils
        {
            public static void LoadEditWaiveMaterialAmountByReader(DataRow row, WaiveEditMaterialAmount o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Length = Convert.ToDecimal(row["Length"]);
                o.Width = Convert.ToDecimal(row["Width"]);
                o.Amount = Convert.ToDecimal(row["Amount"]);
                o.WaiveID = Convert.ToInt64(row["WaiveID"]);
                o.PartID = Convert.ToInt64(row["PartID"]);
                o.MaterialID = Convert.ToInt64(row["MaterialID"]);
            }



            public static bool DeleteWaiveEditMaterialAmounts(Int64 waiveID)
            {
                try
                {
                    Execute.NonQuery(StoredProcedures.DeleteWaiveEditMaterialAmounts, new System.Data.SqlClient.SqlParameter("WaiveID", waiveID));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static class StoredProcedures
        {
            public static string SaveWaiveEditMaterialAmount
            {
                get
                {
                    return "case_Waive_EditMaterialAmounts_SaveAmount";
                }
            }

            public static string GetWaiveEditMaterialAmountByID
            {
                get
                {
                    return "case_WaiveEditMaterialAmounts_GetWaiveMaterialAmountByID";
                }
            }

            public static string GetWaiveEditMaterialAmountByWaiveID_MaterialID_Length_Width
            {
                get
                {
                    return "case_Waive_EditMaterialAmounts_GetWaiveMaterialAmountByWaiveID_MaterialID_Length_Width";
                }
            }
            public static string DeleteWaiveEditMaterialAmounts
            {
                get
                {
                    return "case_Waive_EditMaterialAmounts_DeleteWaiveEditMaterialAmounts";
                }
            }


            

        }
        public class WaiveEditMaterialAmountCollection : List<WaiveEditMaterialAmount> { }
    }
}