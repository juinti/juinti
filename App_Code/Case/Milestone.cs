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
namespace Juinti.Case
{
    public class Milestone
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public long CaseID { get; set; }
        public bool Active { get; set; }

        public Milestone()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Title = String.Empty;
            this.DateStart = this.DateStart;
            this.DateEnd = this.DateEnd;
            this.CaseID = 0;
            this.Active = true;
        }
        public Milestone(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetMilestoneByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadMilestoneByReader(row, this);
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
                Params.Add(new SqlParameter("DateStart", this.DateStart));
                Params.Add(new SqlParameter("DateEnd", this.DateEnd));
                Params.Add(new SqlParameter("CaseID", this.CaseID));
                Params.Add(new SqlParameter("Active", this.Active));

                this.ID = Execute.Scalar(StoredProcedures.SaveMilestone, Params);


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static class Utils
        {
            public static void LoadMilestoneByReader(DataRow row, Milestone o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Title = row["Title"].ToString();
                o.DateStart = Convert.ToDateTime(row["DateStart"]);
                o.DateEnd = Convert.ToDateTime(row["DateEnd"]);
                o.CaseID = Convert.ToInt64(row["CaseID"]);
                o.Active = Convert.ToBoolean(row["Active"]);

            }


            public static MilestoneCollection GetMilestonesByCaseID(Int64 CaseID)
            {
                MilestoneCollection c = new MilestoneCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetMilestonesByCaseID, new System.Data.SqlClient.SqlParameter("CaseID", CaseID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Milestone o = new Milestone();
                        LoadMilestoneByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;

            }
            public static bool DeleteMilestone(Int64 ID)
            {
                try
                {
                    Execute.NonQuery(StoredProcedures.DeleteMilestone, new SqlParameter("ID", ID));
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
            public static string SaveMilestone
            {
                get
                {
                    return "case_Milestones_SaveMilestone";
                }
            }
            public static string GetMilestoneByID
            {
                get
                {
                    return "case_Milestones_GetMilestoneByID";
                }
            }
            public static string GetMilestonesByCaseID
            {
                get
                {
                    return "case_Milestones_GetMilestonesByCaseID";
                }
            }

            public static string DeleteMilestone
            {
                get
                {
                    return "case_Milestones_DeleteMilestone";
                }
            }


        }
    }
    public class MilestoneCollection : List<Milestone> { }
}