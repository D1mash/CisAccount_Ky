using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
namespace Учет_цистерн
{
    public partial class ServiceCostForm : Form
    {
        public ServiceCostForm()
        {
            InitializeComponent();
        }

        int SelectItemRow;
        int SelectSeasonID;


        private void ServiceCostForm_Load(object sender, EventArgs e)
        {
            try
            {
                string Reffresh = "exec dbo.GetServiceCost";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(Reffresh);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[2].Visible = false;
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

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            try
            {
                string Reffresh = "exec dbo.GetServiceCost";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(Reffresh);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[2].Visible = false;
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

        private void Btn_Add_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceCostAddForm ServiceCostAddForm = new ServiceCostAddForm();
                ServiceCostAddForm.Show();
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

        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        string Delete = "delete from d__ServiceCost where ID = " + SelectItemRow;
                        DbConnection.DBConnect(Delete);
                        MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Для удаления записи, необходимо выбрать строку!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView1.Rows[
                        e.RowIndex];
                    string Id = row.Cells["ID"].Value.ToString();
                    string SeasonID = row.Cells["SeasonID"].Value.ToString();
                    SelectItemRow = Convert.ToInt32(Id);
                    SelectSeasonID = Convert.ToInt32(SeasonID);
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

        private void Btn_Updt_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceCostUpdtForm ServiceCostUpdtForm = new ServiceCostUpdtForm();
                ServiceCostUpdtForm.SelectID = SelectItemRow;
                ServiceCostUpdtForm.SelectSeasonID = SelectSeasonID;
                ServiceCostUpdtForm.textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                ServiceCostUpdtForm.textBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                ServiceCostUpdtForm.dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                ServiceCostUpdtForm.dateTimePicker2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                ServiceCostUpdtForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
