
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    class DbConnection
    {
        public static string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";
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