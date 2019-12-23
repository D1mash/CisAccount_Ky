using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraGrid;

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
                gridControl1.DataSource = dataTable;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[2].Visible = false;

                GridColumnSummaryItem Service = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Услуга", "Кол-во: {0}");
                GridColumnSummaryItem Cost = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Цена", "{0}");
                gridView1.Columns["Услуга"].Summary.Add(Service);
                gridView1.Columns["Цена"].Summary.Add(Cost);
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
                gridControl1.DataSource = dataTable;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[2].Visible = false;
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
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(ServiceCostAddForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                ServiceCostAddForm ServiceCostAddForm = new ServiceCostAddForm();
                ServiceCostAddForm.ShowDialog();
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
            if (gridView1.SelectedRowsCount > 0)
            {
                if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string Delete = "delete from d__ServiceCost where ID = " + SelectItemRow;
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
        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                string Id = gridView1.GetFocusedDataRow()[0].ToString();
                string SeasonID = gridView1.GetFocusedDataRow()[2].ToString();
                SelectItemRow = Convert.ToInt32(Id);
                SelectSeasonID = Convert.ToInt32(SeasonID);
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
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(ServiceCostUpdtForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                ServiceCostUpdtForm ServiceCostUpdtForm = new ServiceCostUpdtForm();
                ServiceCostUpdtForm.SelectID = SelectItemRow;
                ServiceCostUpdtForm.SelectSeasonID = SelectSeasonID;
                ServiceCostUpdtForm.textBox2.Text = gridView1.GetFocusedDataRow()[1].ToString();
                ServiceCostUpdtForm.textBox1.Text = gridView1.GetFocusedDataRow()[5].ToString();
                ServiceCostUpdtForm.dateTimePicker1.Text = gridView1.GetFocusedDataRow()[3].ToString();
                ServiceCostUpdtForm.dateTimePicker2.Text = gridView1.GetFocusedDataRow()[4].ToString();
                ServiceCostUpdtForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку! " + ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
    }
}
