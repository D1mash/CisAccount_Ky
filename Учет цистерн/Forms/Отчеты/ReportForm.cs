using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Учет_цистерн.Forms.Оповещения;

namespace Учет_цистерн
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        BindingSource source = new BindingSource();
        
        private void Button3_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            dateTimePicker1.Value = startDate;
            dateTimePicker2.Value = endDate;

            if (comboBox2.SelectedIndex == 0)
            {
                string RefreshAll = "exec dbo.GetReportAllRenderedService '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                DataTable dt;
                dt = DbConnection.DBConnect(RefreshAll);
                source.DataSource = dt;
                dataGridView1.DataSource = source;
                dataGridView1.Columns[0].Visible = false;
                progressBar.Maximum = TotalRow(dt);
                toolStripLabel1.Text = TotalRow(dt).ToString();
            }
            else
            {
                string Refresh = "dbo.GetReportRenderedServices '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
                DataTable dataTable;
                dataTable = DbConnection.DBConnect(Refresh);
                source.DataSource = dataTable;
                dataGridView1.DataSource = source;
                dataGridView1.Columns[0].Visible = false;
                progressBar.Maximum = TotalRow(dataTable);
                toolStripLabel1.Text = TotalRow(dataTable).ToString();
            }
        }

        private int TotalRow(DataTable dataTable)
        {
            int i = 1;
            foreach (DataRow dr in dataTable.Rows)
            {
                i++;
            }
            return i;
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            String Owner = "Select * from d__Owner";
            DataTable OwnerDT = DbConnection.DBConnect(Owner);
            var dr = OwnerDT.NewRow();
            dr["Id"] = -1;
            dr["Name"] = "Все";
            OwnerDT.Rows.InsertAt(dr, 0);
            comboBox2.DataSource = OwnerDT;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";
        }

        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows != null && dataGridView1.Rows.Count != 0)
            {
                if (backgroundWorker.IsBusy)
                    return;
                using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Excel file (*.xlsx)|*.xlsx|All files(*.*)|*.*" })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _inputParametr.FileName = saveFileDialog.FileName;
                        progressBar.Minimum = 0;
                        progressBar.Value = 0;
                        backgroundWorker.RunWorkerAsync(_inputParametr);
                    }
                }
            }
            else
            {
                ExceptionForm exceptionForm = new ExceptionForm();
                exceptionForm.label1.Text = "Обновите данные!";
                exceptionForm.Show();
            }
        }

        struct DataParametr
        {
            public string FileName { get; set; }
        }

        DataParametr _inputParametr;

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string path = "D:/Project/mmmadi/CisAccount/Учет цистерн/Forms/ReportTemplates/Реестр  за арендованных и  собственных вагон-цистерн компании.xls";
            string fileName = ((DataParametr)e.Argument).FileName;
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = workbook.ActiveSheet;

            app.Visible = false;

            int cellRowIndex = 1;
            int cellColumnIndex = 1;

            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dataGridView1.Columns.Count; j++)
                {
                    // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check.
                    if (cellRowIndex == 1)
                    {
                        worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Columns[j].HeaderText;
                    }
                    else
                    {
                        worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                    }
                    cellColumnIndex++;
                }
                backgroundWorker.ReportProgress(cellRowIndex);
                cellColumnIndex = 1;
                cellRowIndex++;
            }

            workbook.SaveAs(fileName);
            app.Quit();

        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            LblStatus.Text = "Обработка строки.. " + e.ProgressPercentage.ToString() /*+ " из " + TotalRow()*/;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if(e.Error == null)
            {
                Thread.Sleep(1);
                LblStatus.Text = "Данные были успешно экспортированы";
                progressBar.Value = 0;
            }
        }
    }
}
