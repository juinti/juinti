using Juinti.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Materials
/// </summary>
namespace Juinti.Case
{
    public class Material
    {
        public Int64 ID { get; set; }
        public string Title { get; set; }
        public int UnitID { get; set; }
        public int UnitType { get; set; }
        public Int64 CaseID { get; set; }
        public int TypeID { get; set; }  // 1 = Package, 2 = Stock, 3 = (pre)Stock
        public decimal MinStockOrder { get; set; }
        public Int64 CompanyID { get; set; }
        public MaterialPrice Price { get; set; }
        public string Number { get; set; }


        public Material()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Title = String.Empty;
            this.UnitID = 0;
            this.CaseID = 0;
            this.TypeID = 0;
            this.CompanyID = 0;
            this.MinStockOrder = 0;
            this.Number = String.Empty;
        }

        public Material(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetMaterialByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadMaterialByReader(row, this);
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
                Params.Add(new SqlParameter("Title", this.Title));
                Params.Add(new SqlParameter("UnitID", this.UnitID));
                Params.Add(new SqlParameter("CaseID", this.CaseID));
                Params.Add(new SqlParameter("TypeID", this.TypeID));
                Params.Add(new SqlParameter("CompanyID", this.CompanyID));
                Params.Add(new SqlParameter("MinStockOrder", this.MinStockOrder));
                Params.Add(new SqlParameter("Number", this.Number));

                this.ID = Execute.Scalar(StoredProcedures.SaveMaterial, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static class Utils
        {
            public static void LoadMaterialByReader(DataRow row, Material o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Title = row["Title"].ToString();
                o.UnitID = Convert.ToInt32(row["UnitID"]);
                o.UnitType = Convert.ToInt32(row["Type"]);
                o.CaseID = Convert.ToInt64(row["CaseID"]);
                o.TypeID = Convert.ToInt32(row["TypeID"]);
                o.CompanyID = Convert.ToInt64(row["CompanyID"]);
                o.MinStockOrder = Convert.ToDecimal(row["MinStockOrder"]);
                o.Price = new MaterialPrice(o.ID, o.CaseID, "DKK");
                o.Number = row["Number"].ToString();

            }

            public static MaterialCollection GetMaterialsByPartID(Int64 PartID)
            {
                MaterialCollection c = new MaterialCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetMaterialsByPartID, new System.Data.SqlClient.SqlParameter("PartID", PartID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Material o = new Material();
                        LoadMaterialByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

            public static MaterialCollection GetMaterialsByPackageID(Int64 PackageID)
            {
                MaterialCollection c = new MaterialCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetMaterialsByPackageID, new System.Data.SqlClient.SqlParameter("PackageID", PackageID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Material o = new Material();
                        LoadMaterialByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

            public static MaterialCollection GetMaterialsByPackageID_CompanyID_TypeID(Int64 PackageID, Int64 CompanyID, int TypeID)
            {
                MaterialCollection c = new MaterialCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("PackageID", PackageID));
                Params.Add(new SqlParameter("CompanyID", CompanyID));
                Params.Add(new SqlParameter("TypeID", TypeID));

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetMaterialsByPackageID_CompanyID_TypeID, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Material o = new Material();
                        LoadMaterialByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }



            public static MaterialCollection GetMaterialsByWaiveID(Int64 WaiveID)
            {
                MaterialCollection c = new MaterialCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetMaterialsByWaiveID, new System.Data.SqlClient.SqlParameter("WaiveID", WaiveID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Material o = new Material();
                        LoadMaterialByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

            

            public static MaterialCollection GetMaterialsByCaseID(Int64 CaseID)
            {
                MaterialCollection c = new MaterialCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetMaterialsByCaseID, new System.Data.SqlClient.SqlParameter("CaseID", CaseID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Material o = new Material();
                        LoadMaterialByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }


            public static MaterialCollection GetDDLMaterialsByCompanyID(int CompanyID, Int64 CaseID)
            {
                MaterialCollection c = new MaterialCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("CaseID", CaseID));
                Params.Add(new SqlParameter("CompanyID", CompanyID));

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetDDLMaterialsByCompanyID, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Material o = new Material();
                        LoadMaterialByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }


            


            public static bool DeleteMaterial(Int64 ID)
            {

                try
                {
                    Execute.NonQuery(StoredProcedures.DeleteMaterial, new System.Data.SqlClient.SqlParameter("ID", ID));
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
            public static string SaveMaterial
            {
                get
                {
                    return "case_Materials_SaveMaterial";
                }
            }
            public static string GetMaterialByID
            {
                get
                {
                    return "case_Materials_GetMaterialByID";
                }
            }

            public static string GetMaterialsByPackageID
            {
                get
                {
                    return "case_Materials_GetMaterialsByPackageID";
                }
            }

            public static string GetMaterialsByPartID
            {
                get
                {
                    return "case_Materials_GetMaterialsByPartID";
                }
            }

            public static string GetMaterialsByWaiveID
            {
                get
                {
                    return "case_Materials_GetMaterialsByWaiveID";
                }
            }
            

            public static string GetMaterialsByCaseID
            {
                get
                {
                    return "case_Materials_GetMaterialsByCaseID";
                }
            }
            public static string DeleteMaterial
            {
                get
                {
                    return "case_Materials_DeleteMaterial";
                }
            }

            public static string GetMaterialsByPackageID_CompanyID_TypeID
            {
                get
                {
                    return "case_Materials_GetMaterialsByPackageID_CompanyID_TypeID";
                }
            }
            public static string GetDDLMaterialsByCompanyID
            {
                get
                {
                    return "case_Materials_GetDDLMaterialsByCompanyID";
                }
            }

            


        }
    }
    public class MaterialCollection : List<Material> { }

}