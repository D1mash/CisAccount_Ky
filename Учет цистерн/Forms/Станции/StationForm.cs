using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class StationForm : Form
    {
        //public string connectionString = "Data Source=POTITPC-01\\PLMLOCAL;Initial Catalog=Batys;User ID=sa;Password=!sql123;";
        string role;

        public StationForm(string role)
        {
            InitializeComponent();
            this.role = role;
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
                AddNewStation_StationForm.Owner = this;
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

        private void StationForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (role == "1")
                {
                    btn_add_station_form.Enabled = true;
                    btn_dlt_station_form.Enabled = true;
                    btn_upd_station_form.Enabled = true;
                    btn_refsh_station_form.Enabled = true;
                }
                else
                {
                    if (role == "2")
                    {
                        btn_add_station_form.Enabled = true;
                        btn_dlt_station_form.Enabled = false;
                        btn_upd_station_form.Enabled = true;
                        btn_refsh_station_form.Enabled = true;
                    }
                    else
                    {
                        btn_add_station_form.Enabled = true;
                        btn_dlt_station_form.Enabled = true;
                        btn_upd_station_form.Enabled = true;
                        btn_refsh_station_form.Enabled = true;
                    }
                }
                string GetStation = "select ID, Name, Code, Code6 from d__Station";
                DataTable dataTable = new DataTable();
                dataTable = DbConnection.DBConnect(GetStation);
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

        public void btn_refsh_station_form_Click_1(object sender, EventArgs e)
        {
            try
            {
                string GetStation = "select ID, Name, Code, Code6 from d__Station";
                DataTable dTl = new DataTable();
                dTl = DbConnection.DBConnect(GetStation);
                gridControl1.DataSource = dTl;
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

        private void btn_dlt_station_form_Click_1(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string DeleteCurrentStation = "delete from d__Station where ID = " + SelectItemRow;
                        DbConnection.DBConnect(DeleteCurrentStation);
                        MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btn_refsh_station_form_Click_1(null,null);
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
                UpdtCurrentStation_StationForm.textBox_Updt_Name_StationForm.Text = gridView1.GetFocusedDataRow()[1].ToString();
                UpdtCurrentStation_StationForm.textBox_Updt_Code_StationForm.Text = gridView1.GetFocusedDataRow()[2].ToString();
                UpdtCurrentStation_StationForm.textBox_Updt_Code6_StationForm.Text = gridView1.GetFocusedDataRow()[3].ToString();
                UpdtCurrentStation_StationForm.SelectStationID_Method = SelectItemRow;
                UpdtCurrentStation_StationForm.Owner = this;
                UpdtCurrentStation_StationForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;

            if (View.IsRowSelected(e.RowHandle))
            {
                e.Appearance.ForeColor = Color.DarkBlue;
                e.Appearance.BackColor = Color.LightBlue;
                //e.HighPriority = true;
            }
        }
    }
}
