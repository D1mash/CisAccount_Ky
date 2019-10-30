using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Учет_цистерн.Forms;

namespace Учет_цистерн
{
    public partial class BrigadeForm : Form
    {
        public BrigadeForm()
        {
            InitializeComponent();
        }

        int SelectItemRow;
        int Active;

        private void BtnBrigadeAdd_Click(object sender, EventArgs e)
        {
            try
            {
                BrigadeAddForm brigadeAddForm = new BrigadeAddForm();
                brigadeAddForm.Show();
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

        private void BtnBrigadeUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string GetActiveBrigade = "select Active from d__Brigade where ID = " + SelectItemRow;
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(GetActiveBrigade);
                Active = Convert.ToInt32(dataTable.Rows[0][0]);

                BrigadeUpdateForm brigadeUpdateForm = new BrigadeUpdateForm
                {
                    SelectID = SelectItemRow
                };
                brigadeUpdateForm.textBox1.Text = dataGVBrigade.CurrentRow.Cells[1].Value.ToString();
                brigadeUpdateForm.textBox2.Text = dataGVBrigade.CurrentRow.Cells[2].Value.ToString();
                brigadeUpdateForm.textBox3.Text = dataGVBrigade.CurrentRow.Cells[3].Value.ToString();
                if (Active == 1)
                {
                    brigadeUpdateForm.checkBox1.Checked = true;
                }
                else
                {
                    brigadeUpdateForm.checkBox1.Checked = false;
                }
                brigadeUpdateForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! "+ex.Message, "Исключение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnBrigadeDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string Delete = "delete from d__Brigade where ID = " + SelectItemRow;
                    DbConnection.DBConnect(Delete);
                    MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnBrigadeReffresh_Click(object sender, EventArgs e)
        {
            try
            {
                string Reffresh = "SELECT ID,Name [Имя],Surname [Фамилия],Lastname [Отчество],FIO [ФИО],Active [Активный] FROM [Batys].[dbo].[d__Brigade]";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(Reffresh);
                dataGVBrigade.DataSource = dataTable;
                dataGVBrigade.Columns[0].Visible = false;
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

        private void DataGVBrigade_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGVBrigade.Rows[
                        e.RowIndex];
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

        private void BrigadeForm_Load(object sender, EventArgs e)
        {
            try
            {
                string Reffresh = "SELECT ID,Name [Имя],Surname [Фамилия],Lastname [Отчество],FIO [ФИО],Active [Активный] FROM [Batys].[dbo].[d__Brigade]";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(Reffresh);
                dataGVBrigade.DataSource = dataTable;
                dataGVBrigade.Columns[0].Visible = false;
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
