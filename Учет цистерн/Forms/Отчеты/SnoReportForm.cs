using DevExpress.XtraGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace Учет_цистерн.Forms.Отчеты
{
    public partial class SnoReportForm : Form
    {
        //BindingSource source = new BindingSource();
        string Destination = ConfigurationManager.AppSettings["Dest"].ToString();
        DataTable dataTable;

        public SnoReportForm()
        {
            InitializeComponent();
        }

        private new void Refresh()
        {
            if (radioButton1.Checked)
            {
                try
                {
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();

                    string GetSNO = "exec dbo.GetSNO '" + dateEdit1.DateTime.ToShortDateString() + "', '" + dateEdit2.DateTime.ToShortDateString() + "'";
                    dataTable = DbConnection.DBConnect(GetSNO);

                    gridControl1.DataSource = dataTable;
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns[1].Visible = false;

                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "Счет-фактура", "Кол.во={0}");
                    gridView1.Columns["Счет-фактура"].Summary.Add(item1);

                    GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Сумма, без НДС", "СУМ={0}");
                    gridView1.Columns["Сумма, без НДС"].Summary.Add(item2);

                    GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Объем", "Объем={0}");
                    gridView1.Columns["Объем"].Summary.Add(item3);

                    GridColumnSummaryItem item4 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Сумма, с НДС", "СУМ={0}");
                    gridView1.Columns["Сумма, с НДС"].Summary.Add(item4);
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
            if (radioButton2.Checked)
            {
                try
                {
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();

                    string GetSNO = "exec dbo.GetCurrentSNO '" + dateEdit1.DateTime.ToShortDateString() + "', '" + dateEdit2.DateTime.ToShortDateString() + "'";
                    dataTable = DbConnection.DBConnect(GetSNO);

                    gridControl1.DataSource = dataTable;
                    gridView1.Columns[0].Visible = false;

                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Остаток", "Объем = {0}");
                    gridView1.Columns["Остаток"].Summary.Add(item1);

                    GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Резервуар № 1", "Объем = {0}");
                    gridView1.Columns["Резервуар № 1"].Summary.Add(item2);

                    GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Резервуар № 2", "Объем = {0}");
                    gridView1.Columns["Резервуар № 2"].Summary.Add(item3);
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

            dateEdit1.EditValue = startDate;
            dateEdit2.EditValue = endDate;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (radioButton1.Checked)
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\СНО Реализ.xlsx";
                    //string fileName = ((DataParametr)e.Argument).FileName;

                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item("СНО Реализация");
                    app.Visible = false;
                    object misValue = System.Reflection.Missing.Value;

                    worksheet.Range["B3"].Value = "в ТОО Казыгурт-Юг реализация СНО за период с " + dateEdit1.DateTime.ToShortDateString() + " по " + dateEdit2.DateTime.ToShortDateString();

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 2; j < dataTable.Columns.Count; j++)
                        {
                            if (j >= 2 && j <= 3)
                            {
                                worksheet.Cells[i + 6, j - 1] = dataTable.Rows[i][j].ToString();
                            }
                            else
                            {
                                if (j > 3 && j <= 6)
                                {
                                    worksheet.Cells[i + 6, j - 1] = Convert.ToDecimal(dataTable.Rows[i][j].ToString());
                                    //worksheet.Range[$"C{i+6}:E{i+6}"].NumberFormat = "General";
                                }
                                else
                                if (j == 8)
                                {
                                    worksheet.Cells[i + 6, j - 2] = Convert.ToDecimal(dataTable.Rows[i][j].ToString());
                                }
                                else 
                                if (j == 9)
                                {
                                    worksheet.Cells[i + 6, j - 2] = dataTable.Rows[i][j].ToString();
                                }
                            }
                        }

                        worksheet.Cells[dataTable.Rows.Count + 6, 1] = "Итог";
                        Excel.Range r1 = worksheet.Cells[dataTable.Rows.Count + 6, 3] as Excel.Range;
                        r1.Formula = String.Format($"=SUM(C{6}:C{dataTable.Rows.Count + 5})");

                        Excel.Range r2 = worksheet.Cells[dataTable.Rows.Count + 6, 5] as Excel.Range;
                        r2.Formula = String.Format($"=SUM(E{6}:E{dataTable.Rows.Count + 5})");

                        Excel.Range r3 = worksheet.Cells[dataTable.Rows.Count + 6, 6] as Excel.Range;
                        r3.Formula = String.Format($"=SUM(F{6}:F{dataTable.Rows.Count + 5})");

                        Excel.Range range = worksheet.Range[worksheet.Cells[i + 6, 1], worksheet.Cells[dataTable.Rows.Count + 6, 7]];
                        FormattingExcelCells(range, true, true);

                        backgroundWorker1.ReportProgress(i);
                    }

                    app.DisplayAlerts = false;
                    workbook.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\СНО Реализация.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    workbook.Close(0);
                    app.Quit();

                    releaseObject(workbook);
                    releaseObject(worksheet);
                    releaseObject(app);

                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\СНО Реализация.xlsx");
                }
                else
                if (radioButton2.Checked)
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\СНО Приход.xlsx";
                    Excel.Application app = new Excel.Application();
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item("СНО Приход");
                    app.Visible = false;
                    object misValue = System.Reflection.Missing.Value;

                    worksheet.Range["B1"].Value = "Приход СНО в ТОО Казыгурт-Юг за период с " + dateEdit1.DateTime.ToShortDateString() + " по " + dateEdit2.DateTime.ToShortDateString();

                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        for (int j = 1; j < dataTable.Columns.Count; j++)
                        {
                            if (j == 1)
                            {
                                worksheet.Cells[i + 4, j] = Convert.ToDecimal(dataTable.Rows[i][j].ToString());
                            }
                            else
                            {
                                if (j > 1 && j <4)
                                {
                                    worksheet.Cells[i + 4, j + 1] = Convert.ToDecimal(dataTable.Rows[i][j].ToString());
                                }
                                else
                                {
                                    worksheet.Cells[i + 4, j + 1] = dataTable.Rows[i][j].ToString();
                                }
                            }
                        }
                        worksheet.Cells[i + 4, 2] = $"=C{i + 4} + D{i + 4}";
                        backgroundWorker1.ReportProgress(i);
                    }

                    worksheet.Cells[dataTable.Rows.Count + 4, 1] = "Итог";
                    Excel.Range r1 = worksheet.Cells[dataTable.Rows.Count + 4, 2] as Excel.Range;
                    r1.Formula = String.Format($"=SUM(B{4}:C{dataTable.Rows.Count + 3})");

                    Excel.Range r2 = worksheet.Cells[dataTable.Rows.Count + 4, 3] as Excel.Range;
                    r2.Formula = String.Format($"=SUM(C{4}:C{dataTable.Rows.Count + 3})");

                    Excel.Range r3 = worksheet.Cells[dataTable.Rows.Count + 4, 4] as Excel.Range;
                    r3.Formula = String.Format($"=SUM(D{4}:D{dataTable.Rows.Count + 3})");

                    Excel.Range range = (Excel.Range)worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[dataTable.Rows.Count + 4, 5]];
                    FormattingExcelCells(range, true, true);

                    app.DisplayAlerts = false;
                    workbook.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\СНО Приход.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    workbook.Close(0);
                    app.Quit();

                    releaseObject(workbook);
                    releaseObject(worksheet);
                    releaseObject(app);

                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\СНО Приход.xlsx");
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
            try
            {
                if (radioButton1.Checked || radioButton2.Checked)
                {
                    if (dataTable.Rows.Count > 0)
                    {
                        if (backgroundWorker1.IsBusy)
                            return;
                        else
                        {
                            ProgrBar.Minimum = 0;
                            ProgrBar.Value = 0;
                            backgroundWorker1.RunWorkerAsync();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Обновите данные!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Выберите вид отчета!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void dateEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}