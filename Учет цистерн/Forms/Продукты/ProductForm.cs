using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class Form_Product : Form
    {
        public Form_Product()
        {
            InitializeComponent();
        }

        int SelectItemRow;
        int SelectHandlingID;

        private void button1_Click_Add_Product(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(addNewCargo))
                    {
                        form.Activate();
                        return;
                    }
                }
                addNewCargo addCargo = new addNewCargo();
                addCargo.ShowDialog();
                
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click_Refresh_Table(object sender, EventArgs e)
        {
            try
            {
                string GetProduct = "select dp.ID, qh.ID as [Hangling_id],dp.Name as [Название], qh.Name as [Обработка] from d__Product dp left join qHangling qh on qh.ID = dp.Handling_id";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(GetProduct);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_Update_Product(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(UpdateProductForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                UpdateProductForm UpdateProductForm = new UpdateProductForm();
                UpdateProductForm.textBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                UpdateProductForm.SelectID = SelectItemRow;
                UpdateProductForm.SelectHandlingID = SelectHandlingID;
                UpdateProductForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void dataGridView1_CellClick_SelectedRow(object sender, DataGridViewCellEventArgs e)
        {
            try
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
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click_Delete_Product(object sender, EventArgs e)
        {
            if(this.dataGridView1.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string DeleteCurrentProduct = "delete from d__Product where ID = " + SelectItemRow;
                        DbConnection.DBConnect(DeleteCurrentProduct);
                        MessageBox.Show("Продукт удалён!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show(exp.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Для удаления записи, необходимо выбрать строку полностью!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Form_Product_Load(object sender, EventArgs e)
        {
            try
            {
                string GetProduct = "select dp.ID, qh.ID as [Hangling_id],dp.Name as [Название], qh.Name as [Обработка] from d__Product dp left join qHangling qh on qh.ID = dp.Handling_id";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(GetProduct);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
