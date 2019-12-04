using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class StationForm : Form
    {
        public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";
        public StationForm()
        {
            InitializeComponent();
        }

        int SelectItemRow;

        private void btn_add_station_form_Click_1(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(AddNewStation_StationForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                AddNewStation_StationForm AddNewStation_StationForm = new AddNewStation_StationForm();
                AddNewStation_StationForm.ShowDialog();
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

        private void dataGridView_Station_Form_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGridView_Station_Form.Rows[e.RowIndex];
                    string Id = row.Cells["ID"].Value.ToString();
                    SelectItemRow = Convert.ToInt32(Id);
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

        private void StationForm_Load(object sender, EventArgs e)
        {
            try
            {
                string GetStation = "select ID, Name as [Наименование], Code, Code6 from d__Station";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(GetStation);
                dataGridView_Station_Form.DataSource = dataTable;
                dataGridView_Station_Form.Columns[0].Visible = false;
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

        private void btn_refsh_station_form_Click_1(object sender, EventArgs e)
        {
            try
            {
                string GetStation = "select ID, Name as [Наименование], Code, Code6 from d__Station";
                DataTable dTl = new DataTable();
                dTl = DbConnection.DBConnect(GetStation);
                dataGridView_Station_Form.DataSource = dTl;
                dataGridView_Station_Form.Columns[0].Visible = false;
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

        private void btn_dlt_station_form_Click_1(object sender, EventArgs e)
        {
            if (dataGridView_Station_Form.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string DeleteCurrentStation = "delete from d__Station where ID = " + SelectItemRow;
                        DbConnection.DBConnect(DeleteCurrentStation);
                        MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btn_upd_station_form_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(UpdtCurrentStation_StationForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                UpdtCurrentStation_StationForm UpdtCurrentStation_StationForm = new UpdtCurrentStation_StationForm();
                UpdtCurrentStation_StationForm.textBox_Updt_Name_StationForm.Text = dataGridView_Station_Form.CurrentRow.Cells[1].Value.ToString();
                UpdtCurrentStation_StationForm.textBox_Updt_Code_StationForm.Text = dataGridView_Station_Form.CurrentRow.Cells[2].Value.ToString();
                UpdtCurrentStation_StationForm.textBox_Updt_Code6_StationForm.Text = dataGridView_Station_Form.CurrentRow.Cells[3].Value.ToString();
                UpdtCurrentStation_StationForm.SelectStationID_Method = SelectItemRow;
                UpdtCurrentStation_StationForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
