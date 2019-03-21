using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

/// <summary>
/// Summary description for Utils
/// </summary>
/// 

namespace Juinti.User
{
    public class Utils
    {
        public Utils()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void LoginUser(string UserName, string UserPassword, Page page, string Url)
        {
            if (Membership.ValidateUser(UserName, UserPassword))
            {
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(UserName, true, 120);
                string encTicket = FormsAuthentication.Encrypt(ticket);

                page.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                page.Response.Redirect(Url);
            }
            else
            {
                page.Response.Redirect("~/login.aspx?=loginErr");
            }
        }
    }
}