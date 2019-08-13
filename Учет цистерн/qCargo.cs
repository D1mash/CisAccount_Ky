using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Учет_цистерн
{
    public partial class qCargo : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";
        public qCargo()
        {
            InitializeComponent();
            this.Text = "Справочник грузов";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addNewCargo addCargo = new addNewCargo();
            addCargo.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select Name as [Название],Handling as [Обработка] from dbo.qCargo";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter sa = new SqlDataAdapter(cmd);
                sa.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            con.Close();
        }
    }
}
