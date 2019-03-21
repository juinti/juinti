using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Database
/// </summary>
/// 

namespace Juinti.Database
{
    public class Database
    {
        public Database()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class Execute
    {
        public static DataTable FillDataTable(string Procedure)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection;
                sqlConnection.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();

                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }

                    return dt;
                }
            }
        }

        public static DataTable FillDataTable(string Procedure, SqlParameter Param)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection;
                cmd.Parameters.Add(new SqlParameter(Param.ParameterName, Param.Value));
                sqlConnection.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();

                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }

                    return dt;
                }
            }

        }

        public static DataTable FillDataTable(string Procedure, SqlParameterCollection Params)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = sqlConnection;
                foreach (SqlParameter param in Params)
                {
                    cmd.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                }
                sqlConnection.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    DataTable dt = new DataTable();

                    if (dr.HasRows)
                    {
                        dt.Load(dr);
                    }

                    return dt;
                }
            }

        }


        public static Int64 Scalar(string Procedure, SqlParameterCollection Params)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Procedure;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection = sqlConnection;
                    foreach (SqlParameter param in Params)
                    {
                        cmd.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                    }
                    sqlConnection.Open();
                    return Convert.ToInt64(cmd.ExecuteScalar());
                }
            }
        }
        public static Decimal ScalarDecimal(string Procedure, SqlParameterCollection Params)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Procedure;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection = sqlConnection;
                    foreach (SqlParameter param in Params)
                    {
                        cmd.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                    }
                    sqlConnection.Open();
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
            }
        }

        public static void NonQuery(string Procedure, SqlParameter Param)
        {

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Procedure;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection = sqlConnection;

                    cmd.Parameters.Add(new SqlParameter(Param.ParameterName, Param.Value));

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void NonQuery(string Procedure, SqlParameterCollection Params)
        {

            using (SqlConnection sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = Procedure;
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Connection = sqlConnection;
                    foreach (SqlParameter param in Params)
                    {
                        cmd.Parameters.Add(new SqlParameter(param.ParameterName, param.Value));
                    }
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                }

            }
        }
    }
}