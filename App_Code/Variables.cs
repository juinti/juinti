using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Variables
/// </summary>
/// 

namespace Juinti.Variables
{

    public static class FilterQueries
    {
        public static string filterContract
        {
            get
            {
                return "filtercontract";
            }
        }
        public static string filterMilestone
        {
            get
            {
                return "filtermilestone";
            }
        }
        public static string filterText
        {
            get
            {
                return "filtertext";
            }
        }


    }

        public static class Urls
    {
        private static string caseFolderPath = "/case/";
        private static string contactsFolderPath = "/contacts/";

        public static string OverviewUrl
        {
            get
            {
                return caseFolderPath + "case.aspx";
            }
        }

        public static string ActivityUrl
        {
            get
            {
                return caseFolderPath + "caseactivity.aspx";
            }
        }
        public static string ContractUrl
        {
            get
            {
                return caseFolderPath + "casecontract.aspx";
            }
        }

        public static string PartUrl
        {
            get
            {
                return caseFolderPath + "part.aspx";
            }
        }

        public static string PartsUrl
        {
            get
            {
                return caseFolderPath + "parts.aspx";
            }
        }
     
        public static string MilestoneUrl
        {
            get
            {
                return caseFolderPath + "casemilestone.aspx";
            }
        }
        public static string PackageUrl
        {
            get
            {
                return caseFolderPath + "package.aspx";
            }
        }
        public static string PackagesUrl
        {
            get
            {
                return caseFolderPath + "packages.aspx";
            }
        }

        public static string WaivesUrl
        {
            get
            {
                return caseFolderPath + "waives.aspx";
            }
        }
        public static string WaiveUrl
        {
            get
            {
                return caseFolderPath + "waive.aspx";
            }
        }
        public static string WaiveEmailUrl
        {
            get
            {
                return caseFolderPath + "waiveemail.aspx";
            }
        }       

        public static string MaterialsUrl
        {
            get
            {
                return caseFolderPath + "materials.aspx";
            }
        }

        public static string MaterialUrl
        {
            get
            {
                return caseFolderPath + "material.aspx";
            }
        }
        public static string MaterialReportUrl
        {
            get
            {
                return caseFolderPath + "materialreport.aspx";
            }
        }
        public static string ContactsUrl
        {
            get
            {
                return contactsFolderPath + "contacts.aspx";
            }
        }
        public static string ContactPersonUrl
        {
            get
            {
                return contactsFolderPath + "person.aspx";
            }
        }
        public static string ContactCompanyUrl
        {
            get
            {
                return contactsFolderPath + "company.aspx";
            }
        }
        public static string ReportUrl
        {
            get
            {
                return caseFolderPath + "report.aspx";
            }
        }
            public static string StockUrl
        {
            get
            {
                return caseFolderPath + "stock.aspx";
            }
        }
        public static string StructureUrl
        {
            get
            {
                return caseFolderPath + "casestructure.aspx";
            }
        }
    }
}