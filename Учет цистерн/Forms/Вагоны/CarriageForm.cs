using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Учет_цистерн.Forms.Оповещения;

namespace Учет_цистерн
{
    public partial class CarriageForm : Form
    {
        private ToolStripProgressBar progBar;
        private ToolStripLabel TlStpLabel;
        private Button btn1;
        private Button btn2;
        private Button btn3;
        private Button btn4;
        private Button btn6;
        int SelectItemRow;
        int SelectOwnerID;
        int Rows;

        public CarriageForm(ToolStripProgressBar toolStripProgressBar1, ToolStripLabel toolStripLabel1, Button button1, Button button2, Button button3, Button button4, Button button6)
        {
            InitializeComponent();
            progBar = toolStripProgressBar1;
            TlStpLabel = toolStripLabel1;
            btn1 = button1;
            btn2 = button2;
            btn3 = button3;
            btn4 = button4;
            btn6 = button6;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            CarriageAddForm carriageAddForm = new CarriageAddForm();
            carriageAddForm.Show();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //string GetCarriage = "Select dc.ID, dc.CarNumber [Номер вагона],dc.AXIS [Осность],do.ID [OwnerID], do.Name [Собственник],do.FullName [Собственник полное наименование] From d__Carriage dc Left Join d__Owner do on do.ID = dc.Owner_ID";
            //DataTable dataTable = new DataTable();
            //dataTable = DbConnection.DBConnect(GetCarriage);
            //dataGridView1.DataSource = dataTable;
            //dataGridView1.Columns[0].Visible = false;
            //dataGridView1.Columns[3].Visible = false;
            progBar.Visible = false;

            if (!backgroundWorker1.IsBusy)
            {
                progBar.Visible = true;
                progBar.Maximum = GetTotalRecords();
                string GetCarriage = "Select dc.ID, dc.CarNumber [Номер вагона],dc.AXIS [Осность],do.ID [OwnerID], do.Name [Собственник],do.FullName [Собственник полное наименование] From d__Carriage dc Left Join d__Owner do on do.ID = dc.Owner_ID";
                btn1.Enabled = false;
                btn2.Enabled = false;
                btn3.Enabled = false;
                btn4.Enabled = false;
                btn6.Enabled = false;
                backgroundWorker1.RunWorkerAsync(GetCarriage);
            }
        }


        //Пока убрал, т.к. 15к вагонов, лучше пусть через Обновить смотрит
        //private void CarriageForm_Load(object sender, EventArgs e)
        //{
        //    string GetCarriage = "Select dc.ID, dc.CarNumber [Номер вагона],dc.AXIS [Осность], do.ID [OwnerID],do.Name [Собственник],do.FullName [Собственник полное наименование] From d__Carriage dc Left Join d__Owner do on do.ID = dc.Owner_ID";
        //    DataTable dataTable = new DataTable();
        //    dataTable = DbConnection.DBConnect(GetCarriage);
        //    dataGridView1.DataSource = dataTable;
        //    dataGridView1.Columns[0].Visible = false;
        //    dataGridView1.Columns[3].Visible = false;
        //}

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[
                    e.RowIndex];
                string Id = row.Cells["ID"].Value.ToString();
                string OwnerID = row.Cells["OwnerID"].Value.ToString();
                SelectItemRow = Convert.ToInt32(Id);
                SelectOwnerID = Convert.ToInt32(OwnerID);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            string message = "Вы действительно хотите удалить эту запись?";
            string title = "Удаление";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, title, buttons);
            if (result == DialogResult.OK)
            {
                string CheckReference = "select count(*) from d__RenderedService where Carriage = " + SelectItemRow;
                string Delete = "delete from d__Carriage where ID = " + SelectItemRow;
                DataTable dt = new DataTable();
                dt = DbConnection.DBConnect(CheckReference);
                if (dt.Rows.Count == 0)
                {
                    DbConnection.DBConnect(Delete);
                    MessageBox.Show("Запись удалена!");
                }
                else
                {
                    ExceptionForm exf = new ExceptionForm();
                    exf.label1.Text = "Невозможно удалить, т.к. вагон привязан в таблице Обработанные вагоны";
                    exf.Show();
                }
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                CarriageUpdateForm carriageUpdateForm = new CarriageUpdateForm();
                carriageUpdateForm.SelectID = SelectItemRow;
                carriageUpdateForm.SelectOwnerID = SelectOwnerID;
                carriageUpdateForm.textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                carriageUpdateForm.textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                carriageUpdateForm.Show();
            }
            catch (Exception ex)
            {
                ExceptionForm exf = new ExceptionForm();
                exf.label1.Text = "Для редактирования записи, необходимо указать строку!" + ex.Message;
                exf.Show();
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
                    Thread.Sleep(2);
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
                MessageBox.Show(ex.Message);
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

                dataGridView1.DataSource = e.Result;
                dataGridView1.RowHeadersWidth = 15;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[3].Visible = false;

                progBar.Visible = false;
                btn1.Enabled = true;
                btn2.Enabled = true;
                btn3.Enabled = true;
                btn4.Enabled = true;
                btn6.Enabled = true;

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
    }
}
