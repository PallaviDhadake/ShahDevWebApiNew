using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ShahDevWebApiNew.Models
{
    public class iClass
    {

        public string OpenConnection()
        {
            return System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ShahDev"].ConnectionString;
        }


        public DataTable GetDataTable(string strQuery)
        {
            try
            {
                SqlConnection con = new SqlConnection(OpenConnection());

                SqlDataAdapter da = new SqlDataAdapter(strQuery, con);
                DataTable dt = new DataTable();

                da.Fill(dt);

                con.Close();
                con = null;

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public void ExecuteQuery(string strQuery)
        {
            try
            {
                SqlConnection con = new SqlConnection(OpenConnection());
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strQuery;
                cmd.CommandTimeout = 3000000; // seconds
                cmd.ExecuteNonQuery();
                cmd.Dispose();
                con.Close();
                con = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}