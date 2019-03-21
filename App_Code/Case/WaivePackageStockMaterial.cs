using Juinti.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Juinti.Case
{
    public class WaivePackageStockMaterial
    {
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal TotalAmount { get; set; }
        public string Title { get; set; }
        public Int64 MaterialID { get; set; }
        public int UnitTypeID { get; set; }
        public DateTime EstOrderDate { get; set; }
        public int MyProperty { get; set; }

        public WaivePackageStockMaterial()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.Length = 0;
            this.Width = 0;
            this.TotalAmount = 0;
            this.Title = String.Empty;
            this.MaterialID = 0;
            this.UnitTypeID = 0;
            this.EstOrderDate = DateTime.MaxValue;
        }


        public static class Utils
        {
            public static void LoadWaivePackageStockMaterial(DataRow row, WaivePackageStockMaterial o, bool forDeliveryPlan)
            {
                o.Length = Convert.ToDecimal(row["Length"]);
                o.Width = Convert.ToDecimal(row["Width"]);
                o.TotalAmount = Convert.ToDecimal(row["TotalAmount"]);
                o.Title = row["Title"].ToString();
                o.MaterialID = Convert.ToInt64(row["MaterialID"]);
                o.UnitTypeID = Convert.ToInt32(row["UnitTypeID"]);

                if (forDeliveryPlan)
                {
                    o.EstOrderDate = Convert.ToDateTime(row["EstOrderDate"]);
                }
            }

            public static WaivePackageStockMaterialCollection GetStockWaiveMaterialsByWaiveID(Int64 WaiveID)
            {
                WaivePackageStockMaterialCollection c = new WaivePackageStockMaterialCollection();
              
                DataTable dt = Execute.FillDataTable(StoredProcedures.GetStockWaiveMaterialsByWaiveID, new SqlParameter("WaiveID", WaiveID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        WaivePackageStockMaterial o = new WaivePackageStockMaterial();
                        LoadWaivePackageStockMaterial(row, o, false);
                        c.Add(o);
                    }
                }

                return c;
            }

            public static WaivePackageStockMaterialCollection GetStockWaiveMaterialsByWaiveID(Int64 CaseID, Int64 CompanyID, DateTime DateFrom, DateTime DateTo, Int64 MaterialID)
            {
                WaivePackageStockMaterialCollection c = new WaivePackageStockMaterialCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("CaseID", CaseID));
                Params.Add(new SqlParameter("CompanyID", CompanyID));
                Params.Add(new SqlParameter("DateFrom", DateFrom));
                Params.Add(new SqlParameter("DateTo", DateTo));
                if (MaterialID > 0)
                {
                    Params.Add(new SqlParameter("MaterialID", MaterialID));
                }
                         

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetAllWaiveMaterialsByCompanyIDAndDate, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        WaivePackageStockMaterial o = new WaivePackageStockMaterial();
                        LoadWaivePackageStockMaterial(row, o, true);
                        c.Add(o);
                    }
                }

                return c;
            }

        }


        public static class StoredProcedures
        {
            public static string GetStockWaiveMaterialsByWaiveID
            {
                get
                {
                    return "case_Materials_GetStockWaiveMaterialsByWaiveID";
                }
            }
             
            public static string GetAllWaiveMaterialsByCompanyIDAndDate
            {
                get
                {
                    return "case_Materials_GetAllWaiveMaterialsByCompanyIDAndDate";
                }
            }
        }

    }

    public class WaivePackageStockMaterialCollection : List<WaivePackageStockMaterial> { }
}