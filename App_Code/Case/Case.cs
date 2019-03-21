using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Juinti.Database;

/// <summary>
/// Summary description for Case
/// </summary>
/// 
namespace Juinti.Case
{
    public class Case
    {
        public Int64 ID { get; set; }
        public Int64 CustomerID { get; set; }
        public Int64 UserID { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ImgUrl { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool Active { get; set; }


        private void Initialize()
        {
            this.ID = 0;
            this.CustomerID = 0;
            this.UserID = 0;
            this.Name = String.Empty;
            this.Number = String.Empty;
            this.Street = String.Empty;
            this.ZipCode = String.Empty;
            this.City = String.Empty;
            this.Country = String.Empty;
            this.ImgUrl = String.Empty;
            this.StartDate = null;
            this.EndDate = null;
            this.Active = false;
        }

        public Case()
        {
            Initialize();
        }


        public Case(Int64 ID)
        {
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetCaseByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadCaseByReader(row, this);
                }
            }
        }

        public Case(Int64 ID, int CustomerID)
        {

            SqlCommand cmd = new SqlCommand();
            SqlParameterCollection Params = cmd.Parameters;

            Params.Add(new SqlParameter("ID", ID));
            Params.Add(new SqlParameter("CustomerID", CustomerID));

            DataTable dt = Execute.FillDataTable(StoredProcedures.GetCaseByID_and_CustomerID, Params);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadCaseByReader(row, this);
                }
            }
        }

        public bool Delete()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Execute.NonQuery(StoredProcedures.DeleteCase, new SqlParameter("ID", this.ID));
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



        public bool Save()
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;

                Params.Add(new SqlParameter("ID", this.ID));
                Params.Add(new SqlParameter("CustomerID", this.CustomerID));
                Params.Add(new SqlParameter("UserID", this.UserID));
                Params.Add(new SqlParameter("Name", this.Name));
                Params.Add(new SqlParameter("Number", this.Number));
                Params.Add(new SqlParameter("Street", this.Street));
                Params.Add(new SqlParameter("ZipCode", this.ZipCode));
                Params.Add(new SqlParameter("City", this.City));
                Params.Add(new SqlParameter("Country", this.Country));
                Params.Add(new SqlParameter("ImgUrl", this.ImgUrl));
                if (this.StartDate == null)
                {
                    Params.Add(new SqlParameter("StartDate", DBNull.Value));
                }
                else
                {
                    Params.Add(new SqlParameter("StartDate", this.StartDate));
                }
                if (this.EndDate == null)
                {
                    Params.Add(new SqlParameter("EndDate", DBNull.Value));
                }
                else
                {
                    Params.Add(new SqlParameter("EndDate", this.EndDate));
                }
                Params.Add(new SqlParameter("Active", this.Active));

                this.ID = Execute.Scalar(StoredProcedures.SaveCase, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    public static class Utils
    {
        public static void LoadCaseByReader(DataRow row, Case o)
        {
            o.ID = Convert.ToInt64(row["ID"]);
            o.CustomerID = Convert.ToInt32(row["CustomerID"]);
            o.UserID = Convert.ToInt64(row["UserID"]);
            o.Name = row["Name"].ToString();
            o.Number = row["Number"].ToString();
            o.Street = row["Street"].ToString();
            o.ZipCode = row["ZipCode"].ToString();
            o.City = row["City"].ToString();
            o.Country = row["Country"].ToString();
            o.ImgUrl = row["ImgUrl"].ToString();
            if (row["StartDate"] != DBNull.Value)
            {
                o.StartDate = Convert.ToDateTime(row["StartDate"]);
            }
            if (row["EndDate"] != DBNull.Value)
            {
                o.EndDate = Convert.ToDateTime(row["EndDate"]);
            }
            o.Active = Convert.ToBoolean(row["Active"]);
        }

        public static CaseCollection GetCasesByCustomerID(Int64 CustomerID)
        {
            CaseCollection c = new CaseCollection();

            DataTable dt = Execute.FillDataTable(StoredProcedures.GetCasesByCustomerID, new System.Data.SqlClient.SqlParameter("CustomerID", CustomerID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Case o = new Case();
                    LoadCaseByReader(row, o);
                    c.Add(o);
                }
            }

            return c;
        }
    }

    public static class StoredProcedures
    {
        public static string SaveCase
        {
            get
            {
                return "case_Cases_SaveCase";
            }
        }
        public static string GetCaseByID_and_CustomerID
        {
            get
            {
                return "case_Cases_GetCaseByID_and_CustomerID";
            }
        }

        public static string GetCasesByCustomerID
        {
            get
            {
                return "case_Cases_GetCasesByCustomerID";
            }
        }

        public static string GetCaseByID
        {
            get
            {
                return "case_Cases_GetCaseByID";
            }
        }
        public static string DeleteCase
        {
            get
            {
                return "case_Cases_DeleteCase";
            }
        }
    }

    public class CaseCollection : List<Case> { }
}