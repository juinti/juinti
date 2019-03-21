using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Juinti.Database;
using System.Web.Security;
using System.Web.UI;
using System.Web;

/// <summary>
/// Summary description for Customer
/// </summary>
/// 

namespace Juinti.User.Customer
{
    public class Customer
    {

        public MembershipUser User { get; set; }

        private string Rolename = "Customer";

        public Int64 ID { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string Phonenumber { get; set; }
        public string MembershipUserid { get; set; }

        private void Initialize()
        {
            this.ID = 0;
            this.Name = String.Empty;
            this.ContactPerson = String.Empty;
            this.Phonenumber = String.Empty;
            this.MembershipUserid = String.Empty;
        }

        public Customer()
        {
            Initialize();
        }

        public static Customer Load(string ProviderUserKey)
        {
            Guid guid = new Guid(ProviderUserKey);
            return Customer.Load(guid);
        }

        public static Customer Load(Guid ProviderUserKey)
        {
            Customer oCustomer = new Customer();

            DataTable dt = Execute.FillDataTable(StoredProcedures.GetCustomerByMembershipUserid, new SqlParameter("@MembershipUserid", ProviderUserKey));
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadCustomerByReader(row, oCustomer);
                }
            }

            oCustomer.User = Membership.GetUser(ProviderUserKey);

            return oCustomer;
        }


        //public bool SaveCustomer(string Email)
        //{
        //    return SaveCustomer(, Email);
        //}

        public bool SaveCustomer(string Email, string Password)
        {
            try
            {
                
                Membership.CreateUser(Email, Password, Email);
                
                User = Membership.GetUser(Email);

                if (!Roles.RoleExists(Rolename))
                {
                    Roles.CreateRole(Rolename);
                }
                Roles.AddUserToRole(User.UserName, Rolename);

                return true;

                SqlCommand cmd = new SqlCommand();
                SqlParameterCollection Params = cmd.Parameters;
                Params.Add(new SqlParameter("ID", this.ID));
                Params.Add(new SqlParameter("Name", this.Name));
                Params.Add(new SqlParameter("ContactPerson", this.ContactPerson));
                Params.Add(new SqlParameter("Phonenumber", this.Phonenumber));
                Params.Add(new SqlParameter("MembershipUserid", User.ProviderUserKey));

                this.ID = Execute.Scalar(StoredProcedures.SaveCustomer, Params);

               

            }
            catch (Exception e)
            {
                return false;
            }
        }
    }

    public static class Utils
    {
        public static void LoadCustomerByReader(DataRow row, Customer o)
        {
            o.ID = Convert.ToInt32(row["ID"]);
            o.Name = row["Name"].ToString();
            o.ContactPerson = row["ContactPerson"].ToString();
            o.Phonenumber = row["Phonenumber"].ToString();
            o.MembershipUserid = row["MembershipUserid"].ToString();
        }
    }

    public static class StoredProcedures
    {
        public static string GetCustomerByID
        {
            get
            {
                return "customer_Customers_GetCustomerByID";
            }
        }

        public static string GetCustomerByMembershipUserid
        {
            get
            {
                return "customer_Customers_GetCustomerByMembershipUserid";
            }
        }

        public static string SaveCustomer
        {
            get
            {
                return "customer_Customers_SaveCustomer";
            }
        }


    }
    public class CustomerCollection : List<Customer> { }

}
