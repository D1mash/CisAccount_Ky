using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Учет_цистерн
{
    class DbConnection
    {
        public static string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";
        public static DataTable DBConnect(string query)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dataTable = new DataTable();

            try
            {
                conn.Open();
                da.Fill(dataTable);
                conn.Close();
                conn.Dispose();
            }
            catch(Exception ex) {
                
            }
            return dataTable;
            }
    }
}
