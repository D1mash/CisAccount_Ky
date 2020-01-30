using DevExpress.XtraGrid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Windows.Forms;
using TradeWright.UI.Forms;

namespace Учет_цистерн
{
    public partial class CarriageForm : Form
    {
        int SelectItemRow;
        int SelectOwnerID;
        int Rows;
        string role;

        public CarriageForm(string role)
        {
            InitializeComponent();
            this.role = role;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(CarriageAddForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                CarriageAddForm carriageAddForm = new CarriageAddForm();
                carriageAddForm.ShowDialog();
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

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            CarriageForm_Load(null, null);
        }

        private void Refresh()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();

            string GetCarriage = "Select dc.ID, dc.CarNumber [№ Вагона],dc.AXIS [Осность],do.ID [OwnerID], do.Name [Наименование],do.FullName [Полное наименование], dc.Current_owner[Текущий собственник] From d__Carriage dc Left Join d__Owner do on do.ID = dc.Owner_ID";
            DataTable dataTable = DbConnection.DBConnect(GetCarriage);
            gridControl1.DataSource = dataTable;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[3].Visible = false;

            GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Количество", "Кол.во={0}");
            gridView1.Columns["№ Вагона"].Summary.Add(item1);
        }

        private void gridView1_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {
                string Id = gridView1.GetFocusedDataRow()[0].ToString();
                string OwnerID = gridView1.GetFocusedDataRow()[3].ToString();
                SelectItemRow = Convert.ToInt32(Id);
                SelectOwnerID = Convert.ToInt32(OwnerID);
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if(gridView1.SelectedRowsCount > 0)
                {
                    if (MessageBox.Show("Вы действительно хотите удалить эту запись?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        string Delete = "delete from d__Carriage where ID = " + SelectItemRow;
                        DbConnection.DBConnect(Delete);
                        MessageBox.Show("Запись удалена!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Для удаления записи, необходимо выбрать строку полностью!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType() == typeof(CarriageUpdateForm))
                    {
                        form.Activate();
                        return;
                    }
                }
                CarriageUpdateForm carriageUpdateForm = new CarriageUpdateForm();
                carriageUpdateForm.SelectID = SelectItemRow;
                carriageUpdateForm.SelectOwnerID = SelectOwnerID;
                carriageUpdateForm.textBox1.Text = gridView1.GetFocusedDataRow()[1].ToString();
                carriageUpdateForm.textBox2.Text = gridView1.GetFocusedDataRow()[2].ToString();
                carriageUpdateForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Для редактирования записи, необходимо указать строку!" + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CarriageForm_Load(object sender, EventArgs e)
        {
            if (role == "1")
            {
                btnAdd.Enabled = true;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnRefresh.Enabled = true;
            }
            else
            {
                if (role == "2")
                {
                    btnAdd.Enabled = true;
                    btnUpdate.Enabled = false;
                    btnDelete.Enabled = false;
                    btnRefresh.Enabled = true;
                }
            }

            Refresh();
        }
    }
}
