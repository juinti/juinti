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
    public class Waive
    {
        public Int64 ID { get; set; }
        public string Title { get; set; }
        public DateTime EstOrderDate { get; set; }
        public string Location { get; set; }
        public string Message { get; set; }
        public string Requisition { get; set; }
        public bool IsOrdered { get; set; }
        public DateTime? OrderDate { get; set; }
        public Int64 CaseID { get; set; }
        public Int64 ContractID { get; set; }
        public Int64 MilestoneID { get; set; }
        public Int64 ActivityID { get; set; }
        public bool IsExtra { get; set; }

        public Waive()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Title = String.Empty;
            this.EstOrderDate = DateTime.MaxValue;
            this.Location = String.Empty;
            this.Message = String.Empty;
            this.Requisition = String.Empty;
            this.IsOrdered = false;
            this.OrderDate = null;
            this.CaseID = 0;
            this.ContractID = 0;
            this.MilestoneID = 0;
            this.ActivityID = 0;
            this.IsExtra = false;
        }
        public Waive(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetWaiveByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadWaiveByReader(row, this);
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
                Params.Add(new SqlParameter("EstOrderDate", this.EstOrderDate));
                Params.Add(new SqlParameter("Location", this.Location));
                Params.Add(new SqlParameter("Message", this.Message));
                Params.Add(new SqlParameter("Requisition", this.Requisition));
                Params.Add(new SqlParameter("IsOrdered", this.IsOrdered));
                if (this.OrderDate == null)
                {
                    Params.Add(new SqlParameter("OrderDate", DBNull.Value));
                }
                else
                {
                    Params.Add(new SqlParameter("OrderDate", this.OrderDate));
                }
                Params.Add(new SqlParameter("CaseID", this.CaseID));
                Params.Add(new SqlParameter("ContractID", this.ContractID));
                Params.Add(new SqlParameter("MilestoneID", this.MilestoneID));
                Params.Add(new SqlParameter("ActivityID", this.ActivityID));
                Params.Add(new SqlParameter("IsExtra", this.IsExtra));

                this.ID = Execute.Scalar(StoredProcedures.SaveWaive, Params);


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool SavePackage(Int64 PackageID, Int32 Amount, Int32 ProductionWeeks)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("WaiveID", this.ID));
                Params.Add(new SqlParameter("PackageID", PackageID));
                Params.Add(new SqlParameter("Amount", Amount));
                Params.Add(new SqlParameter("ProductionWeeks", ProductionWeeks));
                this.ID = Execute.Scalar(StoredProcedures.SavePackage, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool RemovePackage(Int64 PackageID)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("PackageID", PackageID));
                Params.Add(new SqlParameter("WaiveID", this.ID));

                Execute.NonQuery(StoredProcedures.RemovePackage, Params);

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
                Params.Add(new SqlParameter("WaiveID", this.ID));
                Params.Add(new SqlParameter("MaterialID", MaterialID));

                Execute.NonQuery(StoredProcedures.AddMaterial, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public decimal GetTotalPrice()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("WaiveID", this.ID));

                return Execute.ScalarDecimal(StoredProcedures.GetWaivePrice, Params);
            }
            catch (Exception e)
            {
                return 0;
            }
        }



        public static class Utils
        {
            public static void LoadWaiveByReader(DataRow row, Waive o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Title = row["Title"].ToString();
                o.EstOrderDate = Convert.ToDateTime(row["EstOrderDate"]);
                o.Location = row["Location"].ToString();
                o.Message = row["Message"].ToString();
                o.Requisition = row["Requisition"].ToString();
                o.IsOrdered = Convert.ToBoolean(row["IsOrdered"]);
                if (row["OrderDate"] != DBNull.Value)
                {
                    o.OrderDate = Convert.ToDateTime(row["OrderDate"]);
                }
                else
                {
                    o.OrderDate = null;
                }
                o.CaseID = Convert.ToInt64(row["CaseID"]);
                o.ContractID = Convert.ToInt64(row["ContractID"]);
                o.MilestoneID = Convert.ToInt64(row["MilestoneID"]);
                o.ActivityID = Convert.ToInt64(row["ActivityID"]);
                o.IsExtra = Convert.ToBoolean(row["IsExtra"]);
            }


            public static WaiveCollection GetTopWaivesByCaseID(Int64 CaseID, int WindowSize)
            {
                WaiveCollection c = new WaiveCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("CaseID", CaseID));
                Params.Add(new SqlParameter("Size", WindowSize));


                DataTable dt = Execute.FillDataTable(StoredProcedures.GetTopWaivesByCaseID, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Waive o = new Waive();
                        LoadWaiveByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }
            public static WaiveCollection GetWaivesByCaseID(Int64 CaseID, int Ammount)
            {
                WaiveCollection c = new WaiveCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetWaivesByCaseID, new System.Data.SqlClient.SqlParameter("CaseID", CaseID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Waive o = new Waive();
                        LoadWaiveByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }


            public static WaiveCollection GetWaives(Int64 CaseID, string SearchText, Int64 ContractID, Int64 MilestoneID, Nullable<DateTime> EstOrderDateFrom, Nullable<DateTime> EstOrderDateTo)
            {
                WaiveCollection c = new WaiveCollection();

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("CaseID", CaseID));
                if (ContractID > 0)
                {
                    Params.Add(new SqlParameter("ContractID", ContractID));
                }
                if (MilestoneID > 0)
                {
                    Params.Add(new SqlParameter("MilestoneID", MilestoneID));
                }
                if (EstOrderDateFrom != null && EstOrderDateFrom > new DateTime(1755))
                {
                    Params.Add(new SqlParameter("EstOrderDateFrom", EstOrderDateFrom));
                }
                if (EstOrderDateTo != null)
                {
                    Params.Add(new SqlParameter("EstOrderDateTo", EstOrderDateTo));
                }

                if (SearchText != null)
                {
                    Params.Add(new SqlParameter("SearchText", SearchText));
                }


                DataTable dt = Execute.FillDataTable(StoredProcedures.GetWaives, Params);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Waive o = new Waive();
                        LoadWaiveByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }


            public static bool DeleteWaive(Int64 ID)
            {
                try
                {
                    Execute.NonQuery(StoredProcedures.DeleteWaive, new System.Data.SqlClient.SqlParameter("WaiveID", ID));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }


            public static bool UpdateWaivePackages(Int64 ID)
            {
                try
                {
                    Execute.NonQuery(StoredProcedures.UpdateWaivePackages, new System.Data.SqlClient.SqlParameter("ID", ID));
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public static bool IsWaiveChanged(Int64 waiveID)
            {
                bool isChanged = false;
                DataTable dt = Execute.FillDataTable(StoredProcedures.IsWaiveChanged, new System.Data.SqlClient.SqlParameter("WaiveID", waiveID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        isChanged = true;
                        break;
                    }
                }
                return isChanged;
            }




        }


        public static class StoredProcedures
        {


            public static string IsWaiveChanged
            {
                get
                {
                    return "case_Waives_IsWaiveChanged";
                }
            }

            public static string SaveWaive
            {
                get
                {
                    return "case_Waives_SaveWaive";
                }
            }
            public static string GetWaiveByID
            {
                get
                {
                    return "case_Waives_GetWaiveByID";
                }
            }

            public static string GetWaives
            {
                get
                {
                    return "case_Waives_GetWaives";
                }
            }

            public static string GetWaivesByCaseID
            {
                get
                {
                    return "case_Waives_GetWaivesByCaseID";
                }
            }
            public static string SavePackage
            {
                get
                {
                    return "case_Waives_SavePackage";
                }
            }
            public static string RemovePackage
            {
                get
                {
                    return "case_Waives_RemovePackage";
                }
            }
            public static string AddMaterial
            {
                get
                {
                    return "case_Waives_AddMaterial";
                }
            }
            public static string GetWaivePrice
            {
                get
                {
                    return "case_Waive_GetWaivePrice";
                }
            }

            public static string GetTopWaivesByCaseID
            {
                get
                {
                    return "case_Waives_GetTopWaivesByCaseID";
                }
            }
            public static string DeleteWaive
            {
                get
                {
                    return "case_Waives_DeleteWaive";
                }
            }
            public static string UpdateWaivePackages
            {
                get
                {
                    return "case_Waives_UpdateWaivePackages";
                }
            }








        }
    }
    public class WaiveCollection : List<Waive> { }
}