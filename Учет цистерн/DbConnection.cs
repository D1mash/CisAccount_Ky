
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    class DbConnection
    {
        //public static string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";

        public static string connectionString = ConfigurationManager.ConnectionStrings["Учет_цистерн.Properties.Settings.BatysConnectionString"].ConnectionString;

        public static DataTable DBConnect(string query)
        {
            SqlConnection conn;
            DataTable dataTable = new DataTable();

            using (conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.Fill(dataTable);
                conn.Close();
                conn.Dispose();
            }
            return dataTable;
        }

        internal static int DatabseConnection(string query)
        {
            SqlConnection con;
            SqlCommand cmd;
            int TotalRecords = 0;
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    con.Open();
                    cmd = new SqlCommand(query, con);
                    TotalRecords = int.Parse(cmd.ExecuteScalar().ToString());
                    con.Close();
                    con.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return TotalRecords;
        }
    }
}