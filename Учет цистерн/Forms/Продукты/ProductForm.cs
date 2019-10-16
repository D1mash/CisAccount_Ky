using System;
using System.Data;
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
        int SelectHandlingID;

        private void button1_Click_Add_Product(object sender, EventArgs e)
        {
            addNewCargo addCargo = new addNewCargo();
            addCargo.Show();
        }

        private void button4_Click_Refresh_Table(object sender, EventArgs e)
        {
            string GetProduct = "select dp.ID, qh.ID as [Hangling_id],dp.Name as [Название], qh.Name as [Обработка] from d__Product dp left join qHangling qh on qh.ID = dp.Handling_id";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(GetProduct);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
        }

        private void button2_Click_Update_Product(object sender, EventArgs e)
        {
            try
            {
                UpdateProductForm UpdateProductForm = new UpdateProductForm();
                UpdateProductForm.textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                UpdateProductForm.SelectID = SelectItemRow;
                UpdateProductForm.SelectHandlingID = SelectHandlingID;
                UpdateProductForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick_SelectedRow(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
                string Id = row.Cells["ID"].Value.ToString();
                string HandID = row.Cells["Hangling_id"].Value.ToString();
                SelectItemRow = Convert.ToInt32(Id);
                SelectHandlingID = Convert.ToInt32(HandID);
            }
        }

        private void button3_Click_Delete_Product(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string CheckReference = "select * from d__RenderedServiceBody where Product_ID = " + SelectItemRow;
                string DeleteCurrentProduct = "delete from d__Product where ID = " + SelectItemRow;
                DataTable dt = new DataTable();
                dt = DbConnection.DBConnect(CheckReference);
                if (dt.Rows.Count == 0)
                {
                    DbConnection.DBConnect(DeleteCurrentProduct);
                    MessageBox.Show("Продукт удалён!","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Невозможно удалить, т.к. продукт привязан к таблице Обработанные вагоны", "",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
            }
        }

        private void Form_Product_Load(object sender, EventArgs e)
        {
            string GetProduct = "select dp.ID, qh.ID as [Hangling_id],dp.Name as [Название], qh.Name as [Обработка] from d__Product dp left join qHangling qh on qh.ID = dp.Handling_id";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(GetProduct);
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
        }
    }
}
