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
    public class Contract
    {
        public Int64 ID { get; set; }
        public string Title { get; set; }
        public Int64 CaseID { get; set; }

        public Contract()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Title = String.Empty;
        }
        public Contract(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetContractByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadContractByReader(row, this);
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
                Params.Add(new SqlParameter("CaseID", this.CaseID));
                this.ID = Execute.Scalar(StoredProcedures.SaveContract, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static class Utils
        {
            public static void LoadContractByReader(DataRow row, Contract o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Title = row["Title"].ToString();
                o.CaseID = Convert.ToInt64(row["CaseID"]);
            }


            public static ContractCollection GetContractsByCaseID(Int64 CaseID)
            {
                ContractCollection c = new ContractCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetContractsByCaseID, new System.Data.SqlClient.SqlParameter("CaseID", CaseID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Contract o = new Contract();
                        LoadContractByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;

            }
        }
        public static class StoredProcedures
        {
            public static string SaveContract
            {
                get
                {
                    return "case_Contracts_SaveContract";
                }
            }
            public static string GetContractByID
            {
                get
                {
                    return "case_Contracts_GetContractByID";
                }
            }
            public static string GetContractsByCaseID
            {
                get
                {
                    return "case_Contracts_GetContractsByCaseID";
                }
            }


        }
    }
    public class ContractCollection : List<Contract> { }
}