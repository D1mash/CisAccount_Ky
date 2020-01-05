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
        private TabControlExtra tabControlExtra;
        private ToolStripProgressBar progBar;
        private ToolStripLabel TlStpLabel;
        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btn4;
        private Button btn6;
        private Button btn7;
        int SelectItemRow;
        int SelectOwnerID;
        int Rows;
        string role;

        BindingSource source = new BindingSource();

        public CarriageForm(ToolStripProgressBar toolStripProgressBar1, ToolStripLabel toolStripLabel1, Button button1, Button button2, Button button3, Button button4, Button button6, TradeWright.UI.Forms.TabControlExtra tabControl1, Button button7, string role)
        {
            InitializeComponent();
            this.progBar = toolStripProgressBar1;
            this.TlStpLabel = toolStripLabel1;
            this.btn1 = button1;
            this.btn2 = button2;
            this.btn3 = button3;
            this.btn4 = button4;
            this.btn6 = button6;
            this.btn7 = button7;
            this.tabControlExtra = tabControl1;
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
            progBar.Visible = false;

            if (!backgroundWorker1.IsBusy)
            {
                progBar.Visible = true;
                progBar.Maximum = GetTotalRecords();
                string GetCarriage = "Select dc.ID, dc.CarNumber [№ Вагона],dc.AXIS [Осность],do.ID [OwnerID], do.Name [Наименование],do.FullName [Полное наименование], dc.Current_owner[Текущий собственник] From d__Carriage dc Left Join d__Owner do on do.ID = dc.Owner_ID";
                btn1.Enabled = false;
                btn2.Enabled = false;
                btn3.Enabled = false;
                btn4.Enabled = false;
                btn6.Enabled = false;
                btn7.Enabled = false;
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnRefresh.Enabled = false;
                tabControlExtra.DisplayStyleProvider.ShowTabCloser = false;
                backgroundWorker1.RunWorkerAsync(GetCarriage);
            }
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


        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Application.UseWaitCursor = true; //keeps waitcursor even when the thread ends.
            System.Windows.Forms.Cursor.Current = Cursors.WaitCursor;
            string Query = (string)e.Argument;
            //string Refresh = "dbo.GetRenderedService '" + dateTimePicker2.Value.Date.ToString() + "','" + dateTimePicker4.Value.Date.ToString() + "'";

            int i = 1;
            try
            {

                DataTable dataTable = DbConnection.DBConnect(Query);
                foreach (DataRow dr in dataTable.Rows)
                {
                    backgroundWorker1.ReportProgress(i);
                    i++;
                }
                e.Result = dataTable;

                if (backgroundWorker1.CancellationPending)
                {
                    // Set the e.Cancel flag so that the WorkerCompleted event
                    // knows that the process was cancelled.
                    e.Cancel = true;
                    backgroundWorker1.ReportProgress(0);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BackgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (!backgroundWorker1.CancellationPending)
            {
                progBar.Value = e.ProgressPercentage;
                TlStpLabel.Text = "Обработка строки.. " + e.ProgressPercentage.ToString() + " из " + GetTotalRecords();
            }
        }

        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                TlStpLabel.Text = "Готов";
            }
            else if (e.Error != null)
            {
                TlStpLabel.Text = "Ошибка" + e.Error.Message;
            }
            else
            {
                Application.UseWaitCursor = false;
                System.Windows.Forms.Cursor.Current = Cursors.Default;

                source.DataSource = e.Result;
                gridControl1.DataSource = source;
                gridView1.Columns[0].Visible = false;
                gridView1.Columns[3].Visible = false;

                GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Количество", "Кол.во={0}");
                gridView1.Columns["№ Вагона"].Summary.Add(item1);

                progBar.Visible = false;
                btn1.Enabled = true;
                btn2.Enabled = true;
                btn3.Enabled = true;
                btn4.Enabled = true;
                btn6.Enabled = true;
                btn7.Enabled = true;
                btnAdd.Enabled = true;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
                btnRefresh.Enabled = true;

                tabControlExtra.DisplayStyleProvider.ShowTabCloser = true;
                TlStpLabel.Text = "Данные загружены...";
            }
        }

        public int GetTotalRecords()
        {
            try
            {
                string TotalRow = "TotalRowCarriage";
                Rows = DbConnection.DatabseConnection(TotalRow);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Rows;
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
        }
    }
}
