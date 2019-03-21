using Juinti.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MaterialUnit
/// </summary>
/// 

namespace Juinti.Case
{
    public class MaterialUnit
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }    // 1=length, 2=square, 3=cubic, 4=weight, 5=piece, 6=pipe, 7=nails_and_screws, 8=diameter

        public MaterialUnit()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.ID = 0;
            this.Title = String.Empty;
            this.Type = 0;
        }
        public MaterialUnit(int ID)
        {
            Initialize();
            DataTable dt = Execute.FillDataTable(StoredProcedures.GetMaterialUnitByID, new SqlParameter("ID", ID));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Utils.LoadMaterialUnitByReader(row, this);
                }
            }
        }

        //public bool Save()
        //{
        //    try
        //    {
        //        SqlCommand cmd = new SqlCommand();
        //        SqlParameterCollection Params = cmd.Parameters;

        //        Params.Add(new SqlParameter("ID", this.ID));
        //        Params.Add(new SqlParameter("Title", this.Title));
        //        Params.Add(new SqlParameter("Type", this.Type));

        //        this.ID = Execute.Scalar(StoredProcedures.SaveMaterialUnit, Params);

        //        return true;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}
        public static class Utils
        {
            public static void LoadMaterialUnitByReader(DataRow row, MaterialUnit o)
            {
                o.ID = Convert.ToInt32(row["ID"]);
                o.Title = row["Title"].ToString();
                o.Type = Convert.ToInt32(row["Type"]);
            }

            public static MaterialUnitCollection GetMaterialUnitsByLanguageISO(string LanguageISO)
            {
                MaterialUnitCollection c = new MaterialUnitCollection();

                DataTable dt = Execute.FillDataTable(StoredProcedures.GetMaterialUnitsByLanguageISO, new System.Data.SqlClient.SqlParameter("LanguageISO", LanguageISO));

                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        MaterialUnit o = new MaterialUnit();
                        LoadMaterialUnitByReader(row, o);
                        c.Add(o);
                    }
                }

                return c;
            }
        }
        public static class StoredProcedures
        {
            //public static string SaveMaterialUnit
            //{
            //    get
            //    {
            //        return "case_MaterialUnits_SaveMaterialUnit";
            //    }
            //}
            public static string GetMaterialUnitByID
            {
                get
                {
                    return "case_MaterialUnits_GetMaterialUnitByID";
                }
            }
            public static string GetMaterialUnitsByLanguageISO
            {
                get
                {
                    return "case_MaterialUnits_GetMaterialUnitsByLanguageISO";
                }
            }
        }       
    }
    public class MaterialUnitCollection : List<MaterialUnit> { }
}