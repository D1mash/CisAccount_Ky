using DevExpress.XtraGrid;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace Учет_цистерн
{
    public partial class ReportForm : Form
    {
        //BindingSource source = new BindingSource();
        DataTable dt;
        DataTable getserv;
        string UserFIO;

        public ReportForm(string userFIO)
        {
            InitializeComponent();
            this.UserFIO = userFIO;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();

                comboBox2.Enabled = false;
                comboBox2.SelectedIndex = 0;
                string Itog_All_Report = "exec dbo.Itog_All_Report '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                dt = DbConnection.DBConnect(Itog_All_Report);
                //source.DataSource = dt;

                gridControl1.DataSource = dt;

                GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Цена", "SUM={0}");
                gridView1.Columns["Цена"].Summary.Add(item1);

                progressBar.Maximum = TotalRow(dt);
                toolStripLabel1.Text = TotalRow(dt).ToString();
            }
            else
            {
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                comboBox2.Enabled = true;

                if (comboBox2.SelectedIndex == 0)
                {
                    string RefreshAll = "exec dbo.GetReportAllRenderedService_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                    dt = DbConnection.DBConnect(RefreshAll);
                    //source.DataSource = dt;
                    gridControl1.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns[14].Visible = false;
                    gridView1.Columns[15].Visible = false;

                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Цена", "SUM={0}");
                    gridView1.Columns["Цена"].Summary.Add(item1);

                    progressBar.Maximum = TotalRow(dt);
                    toolStripLabel1.Text = TotalRow(dt).ToString();

                    gridView1.Columns["Гор"].Width = 30;
                    gridView1.Columns["Хол"].Width = 30;
                    gridView1.Columns["ТОР"].Width = 30;

                    gridView1.Columns["Гор"].OptionsColumn.FixedWidth = true;
                    gridView1.Columns["Хол"].OptionsColumn.FixedWidth = true;
                    gridView1.Columns["ТОР"].OptionsColumn.FixedWidth = true;

                    string GetCountServiceCost = "exec dbo.Itog_All_Report '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                    getserv = DbConnection.DBConnect(GetCountServiceCost);
                }
                else
                {
                    string Refresh = "dbo.GetReportRenderedServices_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
                    dt = DbConnection.DBConnect(Refresh);
                    //source.DataSource = dataTable;
                    gridControl1.DataSource = dt;
                    gridView1.Columns[0].Visible = false;
                    gridView1.Columns[14].Visible = false;
                    gridView1.Columns[15].Visible = false;
                    progressBar.Maximum = TotalRow(dt);
                    toolStripLabel1.Text = TotalRow(dt).ToString();

                    GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Цена", "SUM={0}");
                    gridView1.Columns["Цена"].Summary.Add(item1);

                    gridView1.Columns["Гор"].Width = 30;
                    gridView1.Columns["Хол"].Width = 30;
                    gridView1.Columns["ТОР"].Width = 30;

                    gridView1.Columns["Гор"].OptionsColumn.FixedWidth = true;
                    gridView1.Columns["Хол"].OptionsColumn.FixedWidth = true;
                    gridView1.Columns["ТОР"].OptionsColumn.FixedWidth = true;

                    string GetCountServiceCost = "exec dbo.Itog_Report  '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
                    getserv = DbConnection.DBConnect(GetCountServiceCost);

                }
            }
        }

        private int TotalRow(DataTable dataTable)
        {
            int i = 0;
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

            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            dateTimePicker1.Value = startDate;
            dateTimePicker2.Value = endDate;

        }

        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount != 0)
            {
                if (backgroundWorker.IsBusy)
                    return;
                else
                {
                    _inputParametr1.owner = comboBox2.Text;
                    progressBar.Minimum = 0;
                    progressBar.Value = 0;
                    backgroundWorker.RunWorkerAsync(_inputParametr1);
                }

            }
            else
            {
                MessageBox.Show("Обновите данные!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        struct DataParametr
        {
            public string owner { get; set; }
            public int sz { get; set; }
        }

        DataParametr _inputParametr1;

        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

        static Process GetExcelProcess(Excel.Application excelApp)
        {
            GetWindowThreadProcessId(excelApp.Hwnd, out int id);
            return Process.GetProcessById(id);
        }

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Итог по станции.xlsx";
                    Excel.Application app = new Excel.Application();
                    Process appProcess = GetExcelProcess(app);
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item("Итоговая  справка");
                    app.Visible = false;
                    object misValue = System.Reflection.Missing.Value;

                    int sum_uslug = 0;


                    worksheet.Range["B6"].Value = "c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString();

                    worksheet.Range["B13:H24"].Cut(worksheet.Cells[gridView1.RowCount * 2 + 11, 2]);
                    int item = 0;
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        if (i % 2 == 0)
                        {
                            var k = 11 + item;
                            Excel.Range range = worksheet.Range[worksheet.Cells[i + k, 2], worksheet.Cells[i + k, 8]];
                            range.Merge();
                            FormattingExcelCells(range, true, false);
                            for (int j = 0; j < gridView1.RowCount; j++)
                            {
                                if (j == 0)
                                {
                                    worksheet.Cells[i + k, j + 2] = dt.Rows[j][j].ToString();
                                }
                                else
                                {
                                    worksheet.Cells[i + k, j + 8] = dt.Rows[i][j].ToString();
                                }
                            }
                        }
                        else
                        {
                            var k = 11 + item;
                            Excel.Range range = worksheet.Range[worksheet.Cells[i + k, 2], worksheet.Cells[i + k, 8]];
                            range.Merge();
                            FormattingExcelCells(range, true, false);
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                if (j == 0)
                                {
                                    worksheet.Cells[i + k, j + 2] = dt.Rows[i][j].ToString();
                                }
                                else
                                {
                                    worksheet.Cells[i + k, j + 8] = dt.Rows[i][j].ToString();
                                }
                            }

                        }
                        backgroundWorker.ReportProgress(i);
                        sum_uslug++;
                        item++;
                    }

                    worksheet.Range["I8"].Value = sum_uslug;

                    app.DisplayAlerts = false;
                    workbook.SaveAs(@"D:\Отчеты\Итог по станции.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    workbook.Close(true, misValue, misValue);
                    app.Quit();
                    appProcess.Kill();

                    Process.Start(@"D:\Отчеты\Итог по станции.xls");
                }
                else
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx";

                    string ownerName = ((DataParametr)e.Argument).owner;

                    Excel.Application app = new Excel.Application();
                    Process appProcess = GetExcelProcess(app);
                    Excel.Workbook workbook = app.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.Worksheets.get_Item("ТОО Казыкурт");
                    app.Visible = false;
                    object misValue = System.Reflection.Missing.Value;

                    int cellRowIndex = 0;
                    int totalTOR4 = 0;

                    if (ownerName == "Все")
                    {
                        worksheet.Range["C4"].Value = "всех";
                    }
                    else
                    {
                        worksheet.Range["C4"].Value = ownerName;
                    }

                    worksheet.Range["C6"].Value = "в ТОО Казыгурт-Юг c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString();

                    FormattingExcelCells(worksheet.Range["C6"], false, false);

                    worksheet.Range["K21"].Value = UserFIO;

                    worksheet.Range["B15:K23"].Cut(worksheet.Cells[dt.Rows.Count + 16 + getserv.Rows.Count * 2, 2]);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        worksheet.Cells[i + 10, 1] = i + 1;
                        for (int j = 1; j < dt.Columns.Count - 2; j++)
                        {
                            if (j != 3 && j < 4)
                            {
                                worksheet.Cells[i + 10, j + 1] = dt.Rows[i][j].ToString();
                            }
                            else
                            {
                                if (j == 3)
                                {
                                    if (dt.Rows[i][j].ToString().Trim() == "8")
                                    {
                                        worksheet.Cells[i + 10, 5] = dt.Rows[i][j].ToString();
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 10, 4] = dt.Rows[i][j].ToString();
                                    }
                                }
                            }
                            if (j >= 4 && j <= 5)
                            {
                                worksheet.Cells[i + 10, j + 3] = dt.Rows[i][j].ToString();
                            }
                            else
                            {
                                if (j >= 6 && j <= 9)
                                {
                                    if (dt.Rows[i][j].ToString().Trim() == "True")
                                    {
                                        worksheet.Cells[i + 10, j + 3] = "✓";
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 10, j + 3] = " ";
                                    }
                                }
                            }
                            if (j > 9)
                            {
                                worksheet.Cells[i + 10, j + 3] = dt.Rows[i][j].ToString();
                            }
                        }

                        Excel.Range range = worksheet.Range[worksheet.Cells[i + 10, 1], worksheet.Cells[i + 10, dt.Columns.Count]];
                        FormattingExcelCells(range, true, true);

                        backgroundWorker.ReportProgress(i);

                        cellRowIndex++;
                    }

                    worksheet.Cells[dt.Rows.Count + 12, 2] = "=C6";

                    if (ownerName == "Все")
                    {
                        worksheet.Cells[dt.Rows.Count + 14, 2] = "Всего обработано вагонов - цистерн всех собственников по видам операций:";
                    }
                    else
                    {
                        worksheet.Cells[dt.Rows.Count + 14, 2] = "Всего обработано вагонов - цистерн " + ownerName + " по видам операций:";
                    }

                    int rowcount = 0;
                    for (int i = 0; i < getserv.Rows.Count; i++)
                    {
                        rowcount++;
                        for (int j = 0; j < getserv.Columns.Count; j++)
                        {
                            if (j == 0)
                            {
                                worksheet.Cells[i + cellRowIndex + 15 + rowcount, j + 2] = getserv.Rows[i][j].ToString();
                            }
                            else
                            {
                                worksheet.Cells[i + cellRowIndex + 15 + rowcount, j + 12] = getserv.Rows[i][j].ToString();
                            }
                        }
                    }

                    worksheet.Cells[dt.Rows.Count + 14, 13] = cellRowIndex;

                    worksheet.Cells[dt.Rows.Count + getserv.Rows.Count * 2 + 16, 13] = totalTOR4;

                    worksheet.Cells[dt.Rows.Count + getserv.Rows.Count * 2 + 18, 14] = TotalSum();

                    Excel.Range range1 = worksheet.Range[worksheet.Cells[dt.Rows.Count + 12, 2], worksheet.Cells[dt.Rows.Count + getserv.Rows.Count * 2 + 19, 14]];
                    FormattingExcelCells(range1, false, false);

                    app.DisplayAlerts = false;
                    workbook.SaveAs(@"D:\Отчеты\Реестр  за арендованных и  собственных вагон-цистерн компании.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    workbook.Close(true, misValue, misValue);
                    app.Quit();
                    appProcess.Kill();

                    Process.Start(@"D:\Отчеты\Реестр  за арендованных и  собственных вагон-цистерн компании.xls");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void FormattingExcelCells(Excel.Range range, bool val1, bool val2)
        {
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

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            LblStatus.Text = "Обработка строки.. " + e.ProgressPercentage.ToString() /*+ " из " + TotalRow()*/;
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Thread.Sleep(1);
                LblStatus.Text = "Данные были успешно экспортированы";
                progressBar.Value = 0;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Button3_Click(null, null);
        }

        private Double TotalSum()
        {
            Double sum = 0;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sum += Convert.ToDouble(dt.Rows[i][12].ToString());
            }
            return sum;
        }
    }
}
