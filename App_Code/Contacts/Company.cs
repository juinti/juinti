using Juinti.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Companies
/// </summary>
namespace Juinti.Contacts
{
    public class Company
    {
        public Int64 ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public int CountryID { get; set; }
        public string MembershipUserid { get; set; }


        public Company()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Name = String.Empty;
            this.Email = String.Empty;
            this.Phone = String.Empty;
            this.Address = String.Empty;
            this.Address2 = String.Empty;
            this.Zipcode = String.Empty;
            this.City = String.Empty;
            this.CountryID = 0;
            this.MembershipUserid = String.Empty;

        }
        public Company(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetCompanyByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadCompanyByReader(row, this);
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
                Params.Add(new SqlParameter("Name", this.Name));
                Params.Add(new SqlParameter("Email", this.Email));
                Params.Add(new SqlParameter("Phone", this.Phone));
                Params.Add(new SqlParameter("Address", this.Address));
                Params.Add(new SqlParameter("Address2", this.Address2));
                Params.Add(new SqlParameter("Zipcode", this.Zipcode));
                Params.Add(new SqlParameter("City", this.City));
                Params.Add(new SqlParameter("CountryID", this.CountryID));
                Params.Add(new SqlParameter("MembershipUserid", this.MembershipUserid));

                this.ID = Execute.Scalar(StoredProcedures.SaveCompany, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static class Utils
        {
            public static void LoadCompanyByReader(DataRow row, Company o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Name = row["Name"].ToString();
                o.Email = row["Email"].ToString();
                o.Phone = row["Phone"].ToString();
                o.Address = row["Address"].ToString();
                o.Address2 = row["Address2"].ToString();
                o.Zipcode = row["Zipcode"].ToString();
                o.City = row["City"].ToString();
                o.CountryID = Convert.ToInt32(row["CountryID"]);
                o.MembershipUserid = row["MembershipUserid"].ToString();


            }



            public static CompanyCollection GetCompanies()
            {
                CompanyCollection c = new CompanyCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetCompanies);

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Company o = new Company();
                        LoadCompanyByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }

            public static CompanyCollection GetCompanies(string MembershipUserid)
            {
                CompanyCollection c = new CompanyCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetCompaniesByMembershipUserid, new System.Data.SqlClient.SqlParameter("MembershipUserid", MembershipUserid));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Company o = new Company();
                        LoadCompanyByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }       
            


            public static bool DeleteCompany(Int64 ID)
            {
                try
                {
                    Execute.NonQuery(StoredProcedures.DeleteCompany, new System.Data.SqlClient.SqlParameter("ID", ID));
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
            public static string SaveCompany
            {
                get
                {
                    return "contacts_Companies_SaveCompany";
                }
            }
            public static string GetCompanies
            {
                get
                {
                    return "contacts_Companies_GetCompanies";
                }
            }
            
            public static string GetCompanyByID
            {
                get
                {
                    return "contacts_Companies_GetCompanyByID";
                }
            }
            public static string GetCompaniesByMembershipUserid
            {
                get
                {
                    return "contacts_Companies_GetCompaniesByMembershipUserid";
                }
            }        
            public static string DeleteCompany
            {
                get
                {
                    return "contacts_Companies_DeleteCompany";
                }
            }
        }
    }
    public class CompanyCollection : List<Company> { }
}