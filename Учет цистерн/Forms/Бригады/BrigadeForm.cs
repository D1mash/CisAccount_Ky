using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Учет_цистерн.Forms;

namespace Учет_цистерн
{
    public partial class BrigadeForm : Form
    {
        string role;
        public BrigadeForm(string role)
        {
            InitializeComponent();
            this.role = role;
        }

        int SelectItemRow;
        int Active;

        private void BtnBrigadeAdd_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(BrigadeAddForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                BrigadeAddForm brigadeAddForm = new BrigadeAddForm();
                brigadeAddForm.ShowDialog();
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

                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(BrigadeUpdateForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                BrigadeUpdateForm brigadeUpdateForm = new BrigadeUpdateForm
                {
                    SelectID = SelectItemRow
                };
                brigadeUpdateForm.textBox1.Text = gridView1.GetFocusedDataRow()[1].ToString();
                brigadeUpdateForm.textBox2.Text = gridView1.GetFocusedDataRow()[2].ToString();
                brigadeUpdateForm.textBox3.Text = gridView1.GetFocusedDataRow()[3].ToString();
                if (Active == 1)
                {
                    brigadeUpdateForm.checkBox1.Checked = true;
                }
                else
                {
                    brigadeUpdateForm.checkBox1.Checked = false;
                }
                brigadeUpdateForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! "+ex.Message, "Исключение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnBrigadeDelete_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string Delete = "delete from d__Brigade where ID = " + SelectItemRow;
                        DbConnection.DBConnect(Delete);
                        MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Refresh();
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
            else
            {
                MessageBox.Show("Для удаления записи, необходимо выбрать строку полностью!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Refresh()
        {
            try
            {
                string Reffresh = "SELECT ID,Name,Surname,Lastname,FIO,Active FROM [Batys].[dbo].[d__Brigade]";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(Reffresh);
                gridControl1.DataSource = dataTable;
                gridView1.Columns[0].Visible = false;
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

        private void BtnBrigadeReffresh_Click(object sender, EventArgs e)
        {
            Refresh();
        }


        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                string Id = gridView1.GetFocusedDataRow()[0].ToString();
                SelectItemRow = Convert.ToInt32(Id);
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
                if (role == "1")
                {
                    btnBrigadeAdd.Enabled = true;
                    btnBrigadeUpdate.Enabled = true;
                    btnBrigadeDelete.Enabled = true;
                    btnBrigadeReffresh.Enabled = true;
                }
                else
                {
                    if (role == "2")
                    {
                        btnBrigadeAdd.Enabled = true;
                        btnBrigadeUpdate.Enabled = false;
                        btnBrigadeDelete.Enabled = false;
                        btnBrigadeReffresh.Enabled = true;
                    }
                    else
                    {
                        btnBrigadeAdd.Enabled = false;
                        btnBrigadeUpdate.Enabled = false;
                        btnBrigadeDelete.Enabled = false;
                        btnBrigadeReffresh.Enabled = false;
                    }
                }

                Refresh();
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
