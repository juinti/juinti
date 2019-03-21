using Juinti.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for NextOrderPackages
/// </summary>
/// 

namespace Juinti.Lists
{
    public class NextOrderPackage
    {

        public long WaiveID { get; set; }
        public string WaiveTitle { get; set; }
        public long PackageID { get; set; }
        public string PackageTitle { get; set; }
        public int Amount { get; set; }
        public int ProductionWeeks { get; set; }
        public DateTime TheConfirmDate { get; set; }

        public NextOrderPackage()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.WaiveID = 0;
            this.WaiveTitle = String.Empty;
            this.PackageID = 0;
            this.PackageTitle = String.Empty;
            this.Amount = 0;
            this.ProductionWeeks = 0;
            this.TheConfirmDate = DateTime.MaxValue;
        }


        public static class Utils
        {
            public static void LoadNextOrderPackageByReader(DataRow row, NextOrderPackage o)
            {
                o.WaiveID = Convert.ToInt64(row["WaiveID"]);
                o.WaiveTitle = row["WaiveTitle"].ToString();
                o.PackageID = Convert.ToInt64(row["PackageID"]);
                o.PackageTitle = row["PackageTitle"].ToString();
                o.Amount = Convert.ToInt32(row["Amount"]);
                o.ProductionWeeks = Convert.ToInt32(row["ProductionWeeks"]);
                o.TheConfirmDate = Convert.ToDateTime(row["TheConfirmDate"]);
            }

            public static NextOrderPackageCollection GetTopNextOrderPackages(long CaseID, int Size)
            {
                NextOrderPackageCollection c = new NextOrderPackageCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("CaseID", CaseID));
                Params.Add(new SqlParameter("Size", Size));

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetTopNextOrderPackages, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        NextOrderPackage o = new NextOrderPackage();
                        LoadNextOrderPackageByReader(row, o);
                        c.Add(o);
                    }
                }
                return c;
            }


            public static NextOrderPackageCollection GetTopNextOrderPackagesOrdered(long CaseID, int Size)
            {
                NextOrderPackageCollection c = new NextOrderPackageCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("CaseID", CaseID));
                Params.Add(new SqlParameter("Size", Size));

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetTopNextOrderPackagesOrdered, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        NextOrderPackage o = new NextOrderPackage();
                        LoadNextOrderPackageByReader(row, o);
                        c.Add(o);
                    }
                }
                return c;
            }


            


            public static bool Save(long WaiveID, long PackageID)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlParameterCollection Params = cmd.Parameters;
                    Params.Add(new SqlParameter("WaiveID", WaiveID));
                    Params.Add(new SqlParameter("PackageID", PackageID));

                    Execute.NonQuery(StoredProcedures.PackagesOrderedSave, Params);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public static bool IsInProduction(long WaiveID, long PackageID)
            {

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("WaiveID", WaiveID));
                Params.Add(new SqlParameter("PackageID", PackageID));

                long packageID = 0;
                packageID = Execute.Scalar(StoredProcedures.PackagesOrderedGetOrdered, Params);

                if (packageID > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            public static bool Delete(long WaiveID, long PackageID)
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlParameterCollection Params = cmd.Parameters;
                    Params.Add(new SqlParameter("WaiveID", WaiveID));
                    Params.Add(new SqlParameter("PackageID", PackageID));

                    Execute.NonQuery(StoredProcedures.PackagesOrderedDeleteOrdered, Params);
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
            public static string GetTopNextOrderPackages
            {
                get
                {
                    return "case_Waives_GetTopNextOrderPackages";
                }
            }
            public static string PackagesOrderedSave
            {
                get
                {
                    return "case_Waive_Packages_Ordered_Save";
                }
            }

            public static string PackagesOrderedGetOrdered
            {
                get
                {
                    return "case_Waive_Packages_Ordered_GetOrdered";
                }
            }
            public static string PackagesOrderedDeleteOrdered
            {
                get
                {
                    return "case_Waive_Packages_Ordered_DeleteOrdered";
                }
            }

            public static string GetTopNextOrderPackagesOrdered
            {
                get
                {
                    return "case_Waives_GetTopNextOrderPackagesOrdered";
                }
            }
            



        }

    }
    public class NextOrderPackageCollection : List<NextOrderPackage> { }
}