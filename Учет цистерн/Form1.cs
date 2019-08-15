using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Учет_цистерн
{
    public partial class Form_Product : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";
        public Form_Product()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addNewCargo addCargo = new addNewCargo();
            addCargo.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string GetProduct = "select dp.ID,dp.Name as [Название], qh.Name as [Обработка] from d__Product dp left join qHangling qh on qh.ID = dp.Handling_id";
            SqlDataAdapter sda = new SqlDataAdapter(GetProduct, con);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            dataGridView1.DataSource = dtbl;
            dataGridView1.Columns[0].Visible = false;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            foreach (DataGridViewRow dgRow in dataGridView1.Rows)
            {
                string Id = dgRow.Cells[0].Value.ToString();
                string GetProduct = "delete from d__Product where ID = " +Id;
                SqlDataAdapter sda = new SqlDataAdapter(GetProduct, con);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                dataGridView1.DataSource = dtbl;
            }
            con.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //DataGridView row = dataGridView1.Rows[e.RowIndex];


            }
        }
    }
}
