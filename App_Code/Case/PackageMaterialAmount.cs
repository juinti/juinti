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
    public class PackageMaterialAmount
    {
        public Int64 ID { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Amount { get; set; }
        public Int64 PackageID { get; set; }
        public Int64 PartID { get; set; }
        public Int64 MaterialID { get; set; }


        public PackageMaterialAmount()
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
        }
        public PackageMaterialAmount(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetPackageMaterialAmountByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadPackageMaterialAmountByReader(row, this);
                }
            }
        }

        public PackageMaterialAmount(Int64 PackageID, Int64 PartID, Int64 MaterialID, decimal Length, decimal Width)
        {
            Initialize();

            SqlCommand cmd = new SqlCommand();
            SqlParameterCollection Params = cmd.Parameters;
            Params.Add(new SqlParameter("PackageID", PackageID));
            Params.Add(new SqlParameter("PartID", PartID));
            Params.Add(new SqlParameter("MaterialID", MaterialID));
            Params.Add(new SqlParameter("Length", Length));
            Params.Add(new SqlParameter("Width", Width));

            DataTable dt = Execute.FillDataTable(StoredProcedures.GetPackageMaterialAmountByPackageID_PartID_MaterialID_Length_Width, Params);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadPackageMaterialAmountByReader(row, this);
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
                this.ID = Execute.Scalar(StoredProcedures.SavePackageMaterialAmount, Params);

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
                Params.Add(new SqlParameter("PackageID", this.PackageID));
                Params.Add(new SqlParameter("PartID", this.PartID));
                Params.Add(new SqlParameter("MaterialID", this.MaterialID));
                this.ID = Execute.Scalar(StoredProcedures.AddPackageMaterialAmount, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public static class Utils
        {
            public static void LoadPackageMaterialAmountByReader(DataRow row, PackageMaterialAmount o)
            {

                o.ID = Convert.ToInt64(row["ID"]);
                o.Length = Convert.ToDecimal(row["Length"]);
                o.Width = Convert.ToDecimal(row["Width"]);
                o.Amount = Convert.ToDecimal(row["Amount"]);
                o.PackageID = Convert.ToInt64(row["PackageID"]);
                o.PartID = Convert.ToInt64(row["PartID"]);
                o.MaterialID = Convert.ToInt64(row["MaterialID"]);
            }   
            
            public static PackageMaterialAmountCollection GetPackageMaterialAmountByPackageID_PartID_MaterialID(Int64 PackageID, Int64 PartID, Int64 MaterialID)
            {
                PackageMaterialAmountCollection c = new PackageMaterialAmountCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("PackageID", PackageID));
                Params.Add(new SqlParameter("PartID", PartID));
                Params.Add(new SqlParameter("MaterialID", MaterialID));

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetPackageMaterialAmountByPackageID_PartID_MaterialID, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        PackageMaterialAmount o = new PackageMaterialAmount();
                        LoadPackageMaterialAmountByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

        }
        public static class StoredProcedures
        {
            public static string SavePackageMaterialAmount
            {
                get
                {
                    return "case_Package_Parts_MaterialAmounts_SaveAmount";
                }
            }
            public static string AddPackageMaterialAmount
            {
                get
                {
                    return "case_Package_Parts_MaterialAmounts_AddAmount";
                }
            }


            public static string GetPackageMaterialAmountByID
            {
                get
                {
                    return "case_PackageMaterialAmounts_GetPackageMaterialAmountByID";
                }
            }

           
            public static string GetPackageMaterialAmountByPackageID_PartID_MaterialID
            {
                get
                {
                    return "case_Package_Parts_MaterialAmounts_GetPackageMaterialAmountByPackageID_PartID_MaterialID";
                }
            }

            public static string GetPackageMaterialAmountByPackageID_PartID_MaterialID_Length_Width
            {
                get
                {
                    return "case_Package_Parts_MaterialAmounts_GetPackageMaterialAmountByPackageID_PartID_MaterialID_Length_Width";
                }
            }


        }
    }
    public class PackageMaterialAmountCollection : List<PackageMaterialAmount> { }

    
    public class PackageMaterialAmountDTO
    {
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Amount { get; set; }
        public Int64 PackageID { get; set; }
        public Int64 MaterialID { get; set; }

        public class Utils
        {

            public static void LoadPackageMaterialAmountDTOByReader(DataRow row, PackageMaterialAmountDTO o)
            {
                o.Length = Convert.ToDecimal(row["Length"]);
                o.Width = Convert.ToDecimal(row["Width"]);
                o.Amount = Convert.ToDecimal(row["Amount"]);
                o.PackageID = Convert.ToInt64(row["PackageID"]);
                o.MaterialID = Convert.ToInt64(row["MaterialID"]);
            }

            public static PackageMaterialAmountDTOCollection GetPackageMaterialAmountByPackageIDMaterialID(Int64 PackageID, Int64 MaterialID)
            {
                PackageMaterialAmountDTOCollection c = new PackageMaterialAmountDTOCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("PackageID", PackageID));
                Params.Add(new SqlParameter("MaterialID", MaterialID));

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetPackageMaterialAmountByPackageID_MaterialID, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        PackageMaterialAmountDTO o = new PackageMaterialAmountDTO();
                        LoadPackageMaterialAmountDTOByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }
        }
        public static class StoredProcedures
        {
            public static string GetPackageMaterialAmountByPackageID_MaterialID
            {
                get
                {
                    return "case_Package_Parts_MaterialAmounts_GetPackageMaterialAmountByPackageID_MaterialID";
                }
            }
        }

    }

    public class PackageMaterialAmountDTOCollection : List<PackageMaterialAmountDTO> { }
}