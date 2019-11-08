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
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\СНО Реализ.xlsx";
                    string fileName = ((DataParametr)e.Argument).FileName;
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item("СНО Реализация");
                    app.Visible = false;

                    worksheet.Range["B3"].Value = "в ТОО Казыгурт-Юг реализация СНО за период с " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString();

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 2; j < dataGridView1.Columns.Count; j++)
                        {
                            if (j == 2)
                            {
                                worksheet.Cells[i + 6, j-1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else
                            {
                                if (j > 2 && j <= 6)
                                {
                                    worksheet.Cells[i + 6, j-1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                }
                            }
                            if(j>7)
                            {
                                worksheet.Cells[i + 6, j-2] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                        }

                        worksheet.Cells[dataGridView1.Rows.Count + 6, 1] = "Итог";
                        Excel.Range r1 = worksheet.Cells[dataGridView1.Rows.Count + 6, 3] as Excel.Range; 
                        r1.Formula = String.Format($"=SUM(C{6}:C{dataGridView1.Rows.Count + 5})");

                        Excel.Range r2 = worksheet.Cells[dataGridView1.Rows.Count + 6, 5] as Excel.Range;
                        r2.Formula = String.Format($"=SUM(E{6}:E{dataGridView1.Rows.Count + 5})");

                        Excel.Range r3 = worksheet.Cells[dataGridView1.Rows.Count + 6, 6] as Excel.Range;
                        r3.Formula = String.Format($"=SUM(F{6}:F{dataGridView1.Rows.Count + 5})");

                        Excel.Range range = worksheet.Range[worksheet.Cells[i + 6, 1], worksheet.Cells[dataGridView1.Rows.Count+6, 7]];
                        FormattingExcelCells(range, true, true);

                        backgroundWorker1.ReportProgress(i);
                    }
                    
                    workbook.SaveAs(fileName);
                    app.Quit();
                }
                else
                if(radioButton2.Checked)
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\СНО Приход.xlsx";
                    string fileName = ((DataParametr)e.Argument).FileName;
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item("СНО Приход");
                    app.Visible = false;

                    worksheet.Range["B1"].Value = "Приход СНО в ТОО Казыгурт-Юг за период с " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString();

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

                    worksheet.Cells[dataGridView1.Rows.Count + 4, 1] = "Итог";
                    Excel.Range r1 = worksheet.Cells[dataGridView1.Rows.Count + 4, 2] as Excel.Range;
                    r1.Formula = String.Format($"=SUM(B{4}:C{dataGridView1.Rows.Count + 3})");

                    Excel.Range r2 = worksheet.Cells[dataGridView1.Rows.Count + 4, 3] as Excel.Range;
                    r2.Formula = String.Format($"=SUM(C{4}:C{dataGridView1.Rows.Count + 3})");

                    Excel.Range r3 = worksheet.Cells[dataGridView1.Rows.Count + 4, 4] as Excel.Range;
                    r3.Formula = String.Format($"=SUM(D{4}:D{dataGridView1.Rows.Count + 3})");

                    Excel.Range range = (Excel.Range) worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[dataGridView1.Rows.Count+4, 5]];
                    FormattingExcelCells(range, true, true);

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
            toolStripLabel1.Text = "Обработка строки.. " + e.ProgressPercentage.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Thread.Sleep(1);
                toolStripLabel1.Text = "Данные были успешно экспортированы";
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

        public void FormattingExcelCells(Excel.Range range, bool val1, bool val2)
        {
            //range.EntireColumn.AutoFit();
            range.Font.Name = "Arial Cyr";
            range.Font.Size = 9;
            range.Font.FontStyle = "Bold";
            if (val1 == true)
            {
                Excel.Borders border = range.Borders;
                border.LineStyle = Excel.XlLineStyle.xlContinuous;
                border.Weight = 2d;
            }
            if (val2 == true)
            {
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            }
            else
            {
                range.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            }
        }
    }
}