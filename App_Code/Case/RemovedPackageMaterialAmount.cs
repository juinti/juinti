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
    public class RemovedPackageMaterialAmount
    {
        public Int64 ID { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Amount { get; set; }
        public Int64 PackageID { get; set; }
        public Int64 PartID { get; set; }
        public Int64 MaterialID { get; set; }
        public Int64 WaiveID { get; set; }

        public RemovedPackageMaterialAmount()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Length = 0;
            this.Width = 0;
            this.Amount = 0;
            this.PackageID = 0;
            this.PartID = 0;
            this.MaterialID = 0;
            this.WaiveID = 0;
        }
        public RemovedPackageMaterialAmount(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetRemovedPackageMaterialAmountByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadRemovedPackageMaterialAmountByReader(row, this);
                }
            }
        }

        public RemovedPackageMaterialAmount(Int64 PackageID, Int64 PartID, Int64 MaterialID, decimal Length, decimal Width, Int64 WaiveID)
        {
            Initialize();

            SqlCommand cmd = new SqlCommand();
            SqlParameterCollection Params = cmd.Parameters;
            Params.Add(new SqlParameter("PackageID", PackageID));
            Params.Add(new SqlParameter("PartID", PartID));
            Params.Add(new SqlParameter("MaterialID", MaterialID));
            Params.Add(new SqlParameter("Length", Length));
            Params.Add(new SqlParameter("Width", Width));
            Params.Add(new SqlParameter("WaiveID", WaiveID));

            DataTable dt = Execute.FillDataTable(StoredProcedures.GetRemovedPackageMaterialAmountByPackageID_PartID_MaterialID_Length_Width_WaiveID, Params);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadRemovedPackageMaterialAmountByReader(row, this);
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
                Params.Add(new SqlParameter("PackageID", this.PackageID));
                Params.Add(new SqlParameter("PartID", this.PartID));
                Params.Add(new SqlParameter("MaterialID", this.MaterialID));
                Params.Add(new SqlParameter("WaiveID", this.WaiveID));
                this.ID = Execute.Scalar(StoredProcedures.SaveRemovedPackageMaterialAmount, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
   
        public static class Utils
        {
            public static void LoadRemovedPackageMaterialAmountByReader(DataRow row, RemovedPackageMaterialAmount o)
            {

                o.ID = Convert.ToInt64(row["ID"]);
                o.Length = Convert.ToDecimal(row["Length"]);
                o.Width = Convert.ToDecimal(row["Width"]);
                o.Amount = Convert.ToDecimal(row["Amount"]);
                o.PackageID = Convert.ToInt64(row["PackageID"]);
                o.PartID = Convert.ToInt64(row["PartID"]);
                o.MaterialID = Convert.ToInt64(row["MaterialID"]);
                o.WaiveID = Convert.ToInt64(row["WaiveID"]);
            }   
           

        }
        public static class StoredProcedures
        {
            public static string SaveRemovedPackageMaterialAmount
            {
                get
                {
                    return "case_Package_Parts_MaterialAmounts_SaveRemovedAmount";
                }
            }
            public static string AddRemovedPackageMaterialAmount
            {
                get
                {
                    return "case_Package_Parts_MaterialAmounts_AddRemovedAmount";
                }
            }


            public static string GetRemovedPackageMaterialAmountByID
            {
                get
                {
                    return "case_PackageMaterialAmounts_GetRemovedPackageMaterialAmountByID";
                }
            }
      

            public static string GetRemovedPackageMaterialAmountByPackageID_PartID_MaterialID_Length_Width_WaiveID
            {
                get
                {
                    return "case_Package_Parts_MaterialAmounts_GetRemovedPackageMaterialAmountByPackageID_PartID_MaterialID_Length_Width_WaiveID";
                }
            }


        }
    }
    public class RemovedPackageMaterialAmountCollection : List<RemovedPackageMaterialAmount> { }

    
    //public class PackageMaterialAmountDTO
    //{
    //    public decimal Length { get; set; }
    //    public decimal Width { get; set; }
    //    public decimal Amount { get; set; }
    //    public Int64 PackageID { get; set; }
    //    public Int64 MaterialID { get; set; }

    //    public class Utils
    //    {

    //        public static void LoadPackageMaterialAmountDTOByReader(DataRow row, PackageMaterialAmountDTO o)
    //        {
    //            o.Length = Convert.ToDecimal(row["Length"]);
    //            o.Width = Convert.ToDecimal(row["Width"]);
    //            o.Amount = Convert.ToDecimal(row["Amount"]);
    //            o.PackageID = Convert.ToInt64(row["PackageID"]);
    //            o.MaterialID = Convert.ToInt64(row["MaterialID"]);
    //        }

    //        public static PackageMaterialAmountDTOCollection GetPackageMaterialAmountByPackageIDMaterialID(Int64 PackageID, Int64 MaterialID)
    //        {
    //            PackageMaterialAmountDTOCollection c = new PackageMaterialAmountDTOCollection();

    //            SqlCommand cmd = new SqlCommand();
    //            SqlParameterCollection Params = cmd.Parameters;

    //            Params.Add(new SqlParameter("PackageID", PackageID));
    //            Params.Add(new SqlParameter("MaterialID", MaterialID));

    //            DataTable dt = Execute.FillDataTable(StoredProcedures.GetPackageMaterialAmountByPackageID_MaterialID, Params);

    //            if (dt.Rows.Count > 0)
    //            {
    //                foreach (DataRow row in dt.Rows)
    //                {
    //                    PackageMaterialAmountDTO o = new PackageMaterialAmountDTO();
    //                    LoadPackageMaterialAmountDTOByReader(row, o);
    //                    c.Add(o);
    //                }
    //            }

    //            return c;
    //        }
    //    }
    //    public static class StoredProcedures
    //    {
    //        public static string GetPackageMaterialAmountByPackageID_MaterialID
    //        {
    //            get
    //            {
    //                return "case_Package_Parts_MaterialAmounts_GetPackageMaterialAmountByPackageID_MaterialID";
    //            }
    //        }
    //    }

    //}

    //public class PackageMaterialAmountDTOCollection : List<PackageMaterialAmountDTO> { }
}