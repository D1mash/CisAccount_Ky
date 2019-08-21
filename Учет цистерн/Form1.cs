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


namespace Учет_цистерн
{
    public partial class Form_Product : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";
        public Form_Product()
        {
            InitializeComponent();
        }

        int SelectItemRow;

        private void button1_Click_Add_Product(object sender, EventArgs e)
        {
            addNewCargo addCargo = new addNewCargo();
            addCargo.Show();
        }

        private void button4_Click_Refresh_Table(object sender, EventArgs e)
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

        private void button2_Click_Update_Product(object sender, EventArgs e)
        {
            UpdateProductForm UpdateProductForm = new UpdateProductForm();
            UpdateProductForm.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            UpdateProductForm.SelectID = SelectItemRow;
            UpdateProductForm.Show();
        }

        private void dataGridView1_CellClick_SelectedRow(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                string Id = row.Cells["ID"].Value.ToString();
                SelectItemRow = Convert.ToInt32(Id);
            }
        }

        private void button3_Click_Delete_Product(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            string DeleteCurrentProduct = "delete from d__Product where ID = "+SelectItemRow;
            SqlDataAdapter sda = new SqlDataAdapter(DeleteCurrentProduct, con);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);
            con.Close();
            MessageBox.Show("Продукт удалён!");
        }

        private void Form_Product_Load(object sender, EventArgs e)
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
    }
}
