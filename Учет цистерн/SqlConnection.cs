using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Учет_цистерн
{
    public class SqlConnection
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";


        

        public SqlConnection(string connectionString)
        {
            this.connectionString = connectionString;

            sqlConnection con = new  SqlConnection(connectionString);
            try
            {
                con.Open();
                con.Close();
            }
            catch (Exception ex)
            {
                
            }

            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select * from dbo.Users";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sa = new SqlDataAdapter(cmd);
                sa.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            
        }
    }
}
