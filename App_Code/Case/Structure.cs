using Juinti.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Structure
/// </summary>
/// 

namespace Juinti.Case
{
    public class Structure
    {
        public Int64 ID { get; set; }
        public string Title { get; set; }
        public Int64 ActivityID { get; set; }
        public Int64 CaseID { get; set; }

        public Structure()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Title = String.Empty;
            this.ActivityID = 0;
            this.CaseID = 0;          
        }
        public Structure(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetStructureByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadStructureByReader(row, this);
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

                this.ID = Execute.Scalar(StoredProcedures.SaveStructure, Params);

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
                Params.Add(new SqlParameter("StructureID", this.ID));

                Int64 returnedPackageID = Execute.Scalar(StoredProcedures.CheckIfStructureIsInUse, Params);
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
            public static void LoadStructureByReader(DataRow row, Structure o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Title = row["Title"].ToString();
                o.ActivityID = Convert.ToInt64(row["ActivityID"]);
                o.CaseID = Convert.ToInt64(row["CaseID"]);
            }


            public static StructureCollection GetStructuresByActivityID(Int64 ActivityID)
            {
                StructureCollection c = new StructureCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetStructuresByActivityID, new System.Data.SqlClient.SqlParameter("ActivityID", ActivityID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Structure o = new Structure();
                        LoadStructureByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

           


            public static bool DeleteStructure(Int64 StructureID)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlParameterCollection Params = cmd.Parameters;
                    Params.Add(new SqlParameter("ID", StructureID));

                    Execute.NonQuery(StoredProcedures.DeleteStructure, Params);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }


        }
        private static class StoredProcedures
        {
            public static string SaveStructure
            {
                get
                {
                    return "case_Structures_SaveStructure";
                }
            }
            public static string GetStructureByID
            {
                get
                {
                    return "case_Structures_GetStructureByID";
                }
            }
            public static string GetStructuresByActivityID
            {
                get
                {
                    return "case_Structures_GetStructuresByActivityID";
                }
            }

         
            public static string DeleteStructure
            {
                get
                {
                    return "case_Structures_DeleteStructure";
                }
            }
                   

            public static string CheckIfStructureIsInUse
            {
                get
                {
                    return "case_Structures_CheckIfPartIsInUse";
                }
            }

            
        }
    }
    public class StructureCollection : List<Structure> { }
}