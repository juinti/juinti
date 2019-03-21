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
    public class Part
    {
        public Int64 ID { get; set; }
        public string Title { get; set; }
        public Int64 ActivityID { get; set; } // TODO: SAFE DELETE (NOT IN USE)
        public Int64 CaseID { get; set; }
        public decimal PieceworkUnitPrice { get; set; }
        public Int32 PieceworkMaterialUnitID { get; set; }
        public Int64 StructureID { get; set; }


        public Part()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Title = String.Empty;
            this.ActivityID = 0;
            this.CaseID = 0;
            this.PieceworkUnitPrice = 0;
            this.PieceworkMaterialUnitID = 0;
            this.StructureID = 0;
        }
        public Part(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetPartByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadPartByReader(row, this);
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
                Params.Add(new SqlParameter("ActivityID", this.ActivityID));
                Params.Add(new SqlParameter("CaseID", this.CaseID));
                Params.Add(new SqlParameter("PieceworkUnitPrice", this.PieceworkUnitPrice));
                Params.Add(new SqlParameter("PieceworkMaterialUnitID", this.PieceworkMaterialUnitID));
                Params.Add(new SqlParameter("StructureID", this.StructureID));

                this.ID = Execute.Scalar(StoredProcedures.SavePart, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AddMaterial(Int64 MaterialID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("PartID", this.ID));
                Params.Add(new SqlParameter("MaterialID", MaterialID));
                
                Execute.NonQuery(StoredProcedures.AddMaterial, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool RemoveMaterial(Int64 MaterialID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("PartID", this.ID));
                Params.Add(new SqlParameter("MaterialID", MaterialID));
                Execute.NonQuery(StoredProcedures.RemoveMaterial, Params);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool isUsedInPackage()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("PartID", this.ID));

                Int64 returnedPackageID = Execute.Scalar(StoredProcedures.CheckIfPartIsInUse, Params);
                if (returnedPackageID > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }




        public static class Utils
        {
            public static void LoadPartByReader(DataRow row, Part o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Title = row["Title"].ToString();
                o.ActivityID = Convert.ToInt64(row["ActivityID"]);
                o.CaseID = Convert.ToInt64(row["CaseID"]);
                o.PieceworkUnitPrice = Convert.ToDecimal(row["PieceworkUnitPrice"]);
                o.PieceworkMaterialUnitID = Convert.ToInt32(row["PieceworkMaterialUnitID"]);
                o.StructureID = Convert.ToInt64(row["StructureID"]);
            }


            public static PartCollection GetPartsByActivityID(Int64 ActivityID)
            {
                PartCollection c = new PartCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetPartsByActivityID, new System.Data.SqlClient.SqlParameter("ActivityID", ActivityID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Part o = new Part();
                        LoadPartByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

            public static PartCollection GetPartsByStructureID(Int64 StructureID)
            {
                PartCollection c = new PartCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetPartsByStructureID, new System.Data.SqlClient.SqlParameter("StructureID", StructureID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Part o = new Part();
                        LoadPartByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

            public static PartCollection GetPartsByPackageID(Int64 PackageID)
            {
                PartCollection c = new PartCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetPartsByPackageID, new System.Data.SqlClient.SqlParameter("PackageID", PackageID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Part o = new Part();
                        LoadPartByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }
            public static PartCollection GetPartsByCaseID(Int64 CaseID)
            {
                PartCollection c = new PartCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetPartsByCaseID, new System.Data.SqlClient.SqlParameter("CaseID", CaseID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Part o = new Part();
                        LoadPartByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

            public static bool DeletePart(Int64 PartID, Int64 ActivityID)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlParameterCollection Params = cmd.Parameters;
                    Params.Add(new SqlParameter("ActivityID", ActivityID));
                    Params.Add(new SqlParameter("PartID", PartID));

                    Execute.NonQuery(StoredProcedures.DeletePart, Params);
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
            public static string SavePart
            {
                get
                {
                    return "case_Parts_SavePart";
                }
            }
            public static string GetPartByID
            {
                get
                {
                    return "case_Parts_GetPartByID";
                }
            }
            public static string GetPartsByActivityID
            {
                get
                {
                    return "case_Parts_GetPartsByActivityID";
                }
            }
            public static string GetPartsByStructureID
            {
                get
                {
                    return "case_Parts_GetPartsByStructureID";
                }
            }


            public static string GetPartsByCaseID
            {
                get
                {
                    return "case_Parts_GetPartsByCaseID";
                }
            }
            public static string AddMaterial
            {
                get
                {
                    return "case_Parts_AddMaterial";
                }
            }

            public static string RemoveMaterial
            {
                get
                {
                    return "case_Parts_RemoveMaterial";
                }
            }

            public static string DeletePart
            {
                get
                {
                    return "case_Parts_DeletePart";
                }
            }

            public static string GetPartsByPackageID
            {
                get
                {
                    return "case_Parts_GetPartsByPackageID";
                }
            }

            public static string CheckIfPartIsInUse
            {
                get
                {
                    return "case_Parts_CheckIfPartIsInUse";
                }
            }
        }
    }
    public class PartCollection : List<Part> { }
}