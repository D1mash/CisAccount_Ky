using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Учет_цистерн.Forms.Отчеты
{
    public partial class SnoReportForm : Form
    {
        BindingSource source = new BindingSource();

        public SnoReportForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                try
                {
                    string GetSNO = "exec dbo.GetSNO";
                    DataTable dataTable = new DataTable();
                    dataTable = DbConnection.DBConnect(GetSNO);
                    source.DataSource = dataTable;
                    dataGridView1.DataSource = source;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
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
            else
            if(radioButton2.Checked)
            { 
                try
                {
                    string GetSNO = "exec dbo.GetCurrentSNO";
                    DataTable dataTable = new DataTable();
                    dataTable = DbConnection.DBConnect(GetSNO);
                    source.DataSource = dataTable;
                    dataGridView1.DataSource = source;
                    dataGridView1.Columns[0].Visible = false;
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

        private void SnoReportForm_Load(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            dateTimePicker1.Value = startDate;
            dateTimePicker2.Value = endDate;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if(radioButton1.Checked)
                {
                    string path = "D:/Project/CisAccount/Учет цистерн/Forms/ReportTemplates/СНО Реализ.xlsx";
                    string fileName = ((DataParametr)e.Argument).FileName;
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item("СНО Реализация");
                    app.Visible = false;

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 1; j < dataGridView1.Columns.Count; j++)
                        {
                                worksheet.Cells[i + 4, j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        backgroundWorker1.ReportProgress(i);
                    }
                    workbook.SaveAs(fileName);
                    app.Quit();
                }
                else
                if(radioButton2.Checked)
                {
                    string path = "D:/Project/CisAccount/Учет цистерн/Forms/ReportTemplates/СНО Приход.xlsx";
                    string fileName = ((DataParametr)e.Argument).FileName;
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item("СНО Приход");
                    app.Visible = false;

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 1; j < dataGridView1.Columns.Count; j++)
                        {
                            if (j == 1)
                            {
                                worksheet.Cells[i + 4, j] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else
                            {
                                if (j > 1)
                                    worksheet.Cells[i + 4, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                        }
                        worksheet.Cells[i + 4, 2] = $"=C{i + 4} + D{i + 4}";
                        backgroundWorker1.ReportProgress(i);
                    }
                    workbook.SaveAs(fileName);
                    app.Quit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgrBar.Value = e.ProgressPercentage;
            label1.Text = "Обработка строки.. " + e.ProgressPercentage.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Thread.Sleep(1);
                label1.Text = "Данные были успешно экспортированы";
                ProgrBar.Value = 0;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows != null && dataGridView1.Rows.Count != 0)
            {
                if (backgroundWorker1.IsBusy)
                    return;
                using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Excel file (*.xlsx)|*.xlsx|All files(*.*)|*.*" })
                {
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        _inputParametr.FileName = saveFileDialog.FileName;
                        ProgrBar.Minimum = 0;
                        ProgrBar.Value = 0;
                        backgroundWorker1.RunWorkerAsync(_inputParametr);
                    }
                }
            }
            else
            {
                MessageBox.Show("Обновите данные!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        struct DataParametr
        {
            public string FileName { get; set; }
        }

        DataParametr _inputParametr;
    }
}