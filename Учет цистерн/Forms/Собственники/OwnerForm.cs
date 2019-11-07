using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class OwnerForm : Form
    {
        public OwnerForm()
        {
            InitializeComponent();
        }

        int SelectItemRow;

        //загрузка данных в DataGridView
        private void OwnerForm_Load(object sender, EventArgs e)
        {
            try
            {
                string Reffresh = "SELECT ID,Name [Наименование],FullName [Полное наименование] FROM [Batys].[dbo].[d__Owner]";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(Reffresh);
                dataGVOwner.DataSource = dataTable;
                dataGVOwner.Columns[0].Visible = false;
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

        //Добавление
        private void btnOwnerAdd_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(OwnerAddForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                OwnerAddForm OwnerAddForm = new OwnerAddForm();
                OwnerAddForm.Show();
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

        //Изменение
        private void btnOwnerUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(OwnerUpdtForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                OwnerUpdtForm OwnerUpdtForm = new OwnerUpdtForm();
                OwnerUpdtForm.SelectID = SelectItemRow;
                OwnerUpdtForm.textBox1.Text = dataGVOwner.CurrentRow.Cells[1].Value.ToString();
                OwnerUpdtForm.textBox2.Text = dataGVOwner.CurrentRow.Cells[2].Value.ToString();
                //OwnerUpdtForm.textBox3.Text = dataGVOwner.CurrentRow.Cells[3].Value.ToString();
                OwnerUpdtForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void dataGVOwner_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row = this.dataGVOwner.Rows[
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

        //Обновление
        private void btnOwnerReffresh_Click(object sender, EventArgs e)
        {
            try
            {
                string Reffresh = "SELECT ID,Name [Наименование],FullName [Полное наименование] FROM [Batys].[dbo].[d__Owner]";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(Reffresh);
                dataGVOwner.DataSource = dataTable;
                dataGVOwner.Columns[0].Visible = false;
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

        //Удаление
        private void btnOwnerDelete_Click(object sender, EventArgs e)
        {
            if (dataGVOwner.SelectedRows.Count > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string Delete = "delete from d__Owner where ID = " + SelectItemRow;
                        DbConnection.DBConnect(Delete);
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
    }
}
