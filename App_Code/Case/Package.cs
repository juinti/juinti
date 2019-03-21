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
    public class Package
    {
        public Int64 ID { get; set; }
        public string Title { get; set; }
        public Int64 ContractID { get; set; }
        public Int32 WaiveAmount { get; set; }
        public Int32 WaiveProductionWeeks { get; set; }
        public Int64 CaseID { get; set; }
        public Int64 ActivityID { get; set; }
        public Int64 OriginalPackageID { get; set; }
        public DateTime? RevisionDate { get; set; }

        public Package()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Title = String.Empty;
            this.ContractID = 0;
            this.WaiveAmount = 0;
            this.WaiveProductionWeeks = 0;
            this.ActivityID = 0;
            this.RevisionDate = null;
        }
        public Package(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetPackageByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadPackageByReader(row, this);
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
                Params.Add(new SqlParameter("ContractID", this.ContractID));
                Params.Add(new SqlParameter("CaseID", this.CaseID));
                Params.Add(new SqlParameter("ActivityID", this.ActivityID));
                Params.Add(new SqlParameter("OriginalPackageID", this.OriginalPackageID));
                if (this.RevisionDate != null)
                {
                    Params.Add(new SqlParameter("RevisionDate", this.RevisionDate));
                }
                else
                {
                    Params.Add(new SqlParameter("RevisionDate", DBNull.Value));
                }

                this.ID = Execute.Scalar(StoredProcedures.SavePackage, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AddPart(Int64 PartID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("PackageID", this.ID));
                Params.Add(new SqlParameter("PartID", PartID));
                this.ID = Execute.Scalar(StoredProcedures.AddPart, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemovePart(Int64 PartID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("PackageID", this.ID));
                Params.Add(new SqlParameter("PartID", PartID));
                this.ID = Execute.Scalar(StoredProcedures.RemovePart, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool isUsedInWaive()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("PackageID", this.ID));

                Int64 returnedWaiveID = Execute.Scalar(StoredProcedures.CheckIfPackageIsInUse, Params);
                if (returnedWaiveID > 0)
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

        public bool hasBeenRevised()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("PackageID", this.ID));

                Int64 returnedNewPackageID = Execute.Scalar(StoredProcedures.CheckIfPackageHasBeenRevised, Params);
                if (returnedNewPackageID > 0)
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

        public Int64 CopyPackage()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("OldPackageID", this.ID));

                return Execute.Scalar(StoredProcedures.CopyPackage, Params);

            }
            catch (Exception e)
            {
                return 0;
            }
        }



        public decimal GetTotalPrice()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("PackageID", this.ID));

                return Execute.ScalarDecimal(StoredProcedures.GetPackagePrice, Params);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public decimal GetUsedTotalPrice(Int64 WaiveID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("PackageID", this.ID));
                Params.Add(new SqlParameter("WaiveID", WaiveID));


                return Execute.ScalarDecimal(StoredProcedures.GetUsedPackagePrice, Params);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public static class Utils
        {
            public static void LoadPackageByReader(DataRow row, Package o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Title = row["Title"].ToString();
                o.ContractID = Convert.ToInt64(row["ContractID"]);
                o.WaiveAmount = Convert.ToInt32(row["Amount"]);
                o.WaiveProductionWeeks = Convert.ToInt32(row["ProductionWeeks"]);
                o.CaseID = Convert.ToInt64(row["CaseID"]);
                o.ActivityID = Convert.ToInt64(row["ActivityID"]);
                o.OriginalPackageID = Convert.ToInt64(row["OriginalPackageID"]);
                if (row["RevisionDate"] != DBNull.Value)
                {
                    o.RevisionDate = Convert.ToDateTime(row["RevisionDate"]);
                }

            }


            public static PackageCollection GetPackagesByCaseID(Int64 CaseID)
            {
                PackageCollection c = new PackageCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetPackagesByCaseID, new System.Data.SqlClient.SqlParameter("CaseID", CaseID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Package o = new Package();
                        LoadPackageByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;

            }
            public static PackageCollection GetPackagesByWaiveID(Int64 WaiveID)
            {
                PackageCollection c = new PackageCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetPackagesByWaiveID, new System.Data.SqlClient.SqlParameter("WaiveID", WaiveID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Package o = new Package();
                        LoadPackageByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;

            }



            public static bool DeletePackage(Int64 ID)
            {

                try
                {
                    Execute.NonQuery(StoredProcedures.DeletePackage, new System.Data.SqlClient.SqlParameter("PackageID", ID));
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
            public static string SavePackage
            {
                get
                {
                    return "case_Packages_SavePackage";
                }
            }
            public static string GetPackageByID
            {
                get
                {
                    return "case_Packages_GetPackageByID";
                }
            }
            public static string GetPackagesByContractID
            {
                get
                {
                    return "case_Packages_GetPackagesByContractID";
                }
            }
            public static string AddPart
            {
                get
                {
                    return "case_Packages_AddPart";
                }
            }
            public static string RemovePart
            {
                get
                {
                    return "case_Packages_RemovePart";
                }
            }
            public static string GetPackagesByWaiveID
            {
                get
                {
                    return "case_Packages_GetPackagesByWaiveID";
                }
            }
            public static string GetPackagesByCaseID
            {
                get
                {
                    return "case_Packages_GetPackagesByCaseID";
                }
            }
            public static string GetPackagePrice
            {
                get
                {
                    return "case_Packages_GetPackagePrice";
                }
            }
            public static string DeletePackage
            {
                get
                {
                    return "case_Packages_DeletePackage";
                }
            }

            public static string CheckIfPackageIsInUse
            {
                get
                {
                    return "case_Packages_CheckIfPackageIsInUse";
                }
            }

            public static string CopyPackage
            {
                get
                {
                    return "case_Packages_CopyPackage";
                }
            }

            public static string GetUsedPackagePrice
            {
                get
                {
                    return "case_Packages_GetUsedPackagePrice";
                }
            }

            public static string CheckIfPackageHasBeenRevised
            {
                get
                {
                    return "case_Packages_CheckIfPackageHasBeenRevised";
                }
            }            
        }
    }
    public class PackageCollection : List<Package> { }
}