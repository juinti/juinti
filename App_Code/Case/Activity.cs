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
    public class Activity
    {
        public Int64 ID { get; set; }
        public string Title { get; set; }
        public Int64 ContractID { get; set; }
        public Int64 CaseID { get; set; }


        public int MyProperty { get; set; }

        public Activity()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Title = String.Empty;
        }
        public Activity(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetActivityByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadActivityByReader(row, this);
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


                this.ID = Execute.Scalar(StoredProcedures.SaveActivity, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool IsInUse()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("ActivityID", this.ID));

                Int64 returnedPackageID = Execute.Scalar(StoredProcedures.CheckIfActivityIsInUse, Params);
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
            public static void LoadActivityByReader(DataRow row, Activity o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Title = row["Title"].ToString();
                o.ContractID = Convert.ToInt64(row["ContractID"]);
                o.CaseID = Convert.ToInt64(row["CaseID"]);
            }         

          
            public static ActivityCollection GetActitvitiesByCaseID(Int64 CaseID)
            {
                ActivityCollection c = new ActivityCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetActitvitiesByCaseID, new System.Data.SqlClient.SqlParameter("CaseID", CaseID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Activity o = new Activity();
                        LoadActivityByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

            public static bool DeleteActivity(long ID)
            {
                try
                {
                    Execute.NonQuery(StoredProcedures.DeleteActivity, new System.Data.SqlClient.SqlParameter("ID", ID));
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
            public static string SaveActivity
            {
                get
                {
                    return "case_Activities_SaveActivity";
                }
            }
            public static string GetActivityByID
            {
                get
                {
                    return "case_Activities_GetActivityByID";
                }
            }
            public static string GetActitvitiesByContractID
            {
                get
                {
                    return "case_Activities_GetActitvitiesByContractID";
                }
            }
            public static string GetActitvitiesByCaseID
            {
                get
                {
                    return "case_Activities_GetActitvitiesByCaseID";
                }
            }
            public static string DeleteActivity
            {
                get
                {
                    return "case_Activities_DeleteActivity";
                }
            }

            public static string CheckIfActivityIsInUse
            {
                get
                {
                    return "case_Activities_CheckIfActivityIsInUse";
                }
            }
            
        }       
    }
    public class ActivityCollection : List<Activity> { }
}