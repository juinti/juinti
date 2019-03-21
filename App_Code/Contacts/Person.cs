using Juinti.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Persons
/// </summary>
namespace Juinti.Contacts
{
    public class Person
    {
        public Int64 ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public Int64 CompanyID { get; set; }
        public string MembershipUserid { get; set; }
        public string Note { get; set; }


        public Person()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Name = String.Empty;
            this.Email = String.Empty;
            this.Phone = String.Empty;
            this.Phone2 = String.Empty;
            this.CompanyID = 0;
            this.MembershipUserid = String.Empty;
            this.Note = String.Empty;

        }
        public Person(Int64 ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetPersonByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadPersonByReader(row, this);
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
                Params.Add(new SqlParameter("Phone2", this.Phone2));
                Params.Add(new SqlParameter("CompanyID", this.CompanyID));
                Params.Add(new SqlParameter("MembershipUserid", this.MembershipUserid));
                Params.Add(new SqlParameter("Note", this.Note));

                this.ID = Execute.Scalar(StoredProcedures.SavePerson, Params);

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static class Utils
        {
            public static void LoadPersonByReader(DataRow row, Person o)
            {
                o.ID = Convert.ToInt64(row["ID"]);
                o.Name = row["Name"].ToString();
                o.Email = row["Email"].ToString();
                o.Phone = row["Phone"].ToString();
                o.Phone2 = row["Phone"].ToString();
                o.CompanyID = Convert.ToInt64(row["CompanyID"]);
                o.MembershipUserid = row["MembershipUserid"].ToString();
                o.Note = row["Note"].ToString();
            }

            public static PersonCollection GetPersonsByCompanyID(Int64 CompanyID)
            {
                PersonCollection c = new PersonCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetPersonsByCompanyID, new System.Data.SqlClient.SqlParameter("CompanyID", CompanyID));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Person o = new Person();
                        LoadPersonByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }  

            public static bool DeletePerson(Int64 ID)
            {
                try
                {
                    Execute.NonQuery(StoredProcedures.DeletePerson, new System.Data.SqlClient.SqlParameter("ID", ID));
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
            public static string SavePerson
            {
                get
                {
                    return "contacts_Persons_SavePerson";
                }
            }
            public static string GetPersonByID
            {
                get
                {
                    return "contacts_Persons_GetPersonByID";
                }
            }
            public static string GetPersonsByCompanyID
            {
                get
                {
                    return "contacts_Persons_GetPersonsByCompanyID";
                }
            }        
            public static string DeletePerson
            {
                get
                {
                    return "contacts_Persons_DeletePerson";
                }
            }
        }
    }
    public class PersonCollection : List<Person> { }
}