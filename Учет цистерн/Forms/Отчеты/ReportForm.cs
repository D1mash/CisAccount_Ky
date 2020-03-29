using DevExpress.XtraGrid;
using DocumentFormat.OpenXml.Spreadsheet;
using SpreadsheetLight;
using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        //string Destination = ConfigurationManager.AppSettings["Dest"].ToString();
        DataTable dt;
        DataTable getserv;

        public ReportForm()
        {
            InitializeComponent();
        }
        
        private new void Refresh()
        {
            if (checkBox1.Checked)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();

                    string Itog_All_Report = "exec dbo.Itog_All_Report '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                    dt = DbConnection.DBConnect(Itog_All_Report);

                    gridControl1.DataSource = dt;
                    gridView1.BestFitColumns();

                    GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Кол.во", "Кол.во ={0}");
                    gridView1.Columns["Кол.во"].Summary.Add(item2);

                    ////progressBar.Maximum = TotalRow(dt);
                    toolStripLabel1.Text = TotalRow(dt).ToString();
                }
                else
                {
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();

                    string Itog_Report = "exec dbo.Itog_Report  '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
                    dt = DbConnection.DBConnect(Itog_Report);

                    gridControl1.DataSource = dt;
                    gridView1.BestFitColumns();

                    GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Кол.во", "Кол.во ={0}");
                    gridView1.Columns["Кол.во"].Summary.Add(item2);

                    ////progressBar.Maximum = TotalRow(dt);
                    toolStripLabel1.Text = TotalRow(dt).ToString();
                }
            }
            else
            {
                if (checkBox2.Checked)
                {
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();

                    string RefreshAll = "exec dbo.GetReportAllRenderedService_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'," + "@Type = " + 1;
                    dt = DbConnection.DBConnect(RefreshAll);

                    ////int col = TotalRow(dt);
                    ////progressBar.Value = 0;
                    ////toolStripLabel1.Text = "Всего строк: " + TotalRow(dt).ToString();

                }
                else
                {
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                    comboBox2.Enabled = true;

                    if (comboBox2.SelectedIndex == 0)
                    {
                        string RefreshAll = "exec dbo.GetReportAllRenderedService_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'," + "@Type = " + 1;
                        dt = DbConnection.DBConnect(RefreshAll);
                        //source.DataSource = dt;
                        gridControl1.DataSource = dt;
                        gridView1.Columns[0].Visible = false;
                        gridView1.BestFitColumns();

                        GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Сумма услуг", "СУМ={0}");
                        gridView1.Columns["Сумма услуг"].Summary.Add(item1);

                        GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Стоимость ТОР", "СУМ={0}");
                        gridView1.Columns["Стоимость ТОР"].Summary.Add(item2);

                        GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "№ акта", "Кол.во={0}");
                        gridView1.Columns["№ акта"].Summary.Add(item3);

                        //progressBar.Maximum = TotalRow(dt);
                        toolStripLabel1.Text = TotalRow(dt).ToString();

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

                        //    //progressBar.Maximum = TotalRow(dt);
                        toolStripLabel1.Text = TotalRow(dt).ToString();
                        gridView1.BestFitColumns();

                        GridColumnSummaryItem item1 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Сумма услуг", "СУМ={0}");
                        gridView1.Columns["Сумма услуг"].Summary.Add(item1);

                        GridColumnSummaryItem item2 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "Стоимость ТОР", "СУМ={0}");
                        gridView1.Columns["Стоимость ТОР"].Summary.Add(item2);

                        GridColumnSummaryItem item3 = new GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "№ акта", "Кол.во={0}");
                        gridView1.Columns["№ акта"].Summary.Add(item3);

                        string GetCountServiceCost = "exec dbo.Itog_Report  '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
                        getserv = DbConnection.DBConnect(GetCountServiceCost);
                    }
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
            //var startDate = new DateTime(now.Year, now.Month, 1);
            //var endDate = startDate.AddMonths(1).AddDays(-1);

            var startDate = now;
            var endDate = now;

            dateTimePicker1.Value = startDate;
            dateTimePicker2.Value = endDate;

            checkBox3_CheckedChanged(null, null);
        }

        private void Btn_Excel_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount != 0)
            {
                try
                {
                    if (checkBox1.Checked)
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Итог по станции.xlsx";

                        SLDocument sl = new SLDocument(path);
                        sl.SelectWorksheet("Итоговая  справка");

                        sl.SetCellValue("B6", "c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString());

                        var val = gridView1.RowCount * 2 + 11;
                        sl.CopyCell("B13", "H24", "B"+val, true);
                        //worksheet.Range["B13:H24"].Cut(worksheet.Cells[gridView1.RowCount * 2 + 11, 2]);

                        int item = 0;

                        //Кол.во услуг
                        int total = 0;

                        //Сумм * Кол.во
                        double final_sum = 0;

                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            if (i % 2 == 0)
                            {
                                var k = 11 + item;
                                //Excel.Range range = worksheet.Range[worksheet.Cells[i + k, 2], worksheet.Cells[i + k, 8]];
                                //range.Merge();

                                sl.MergeWorksheetCells(i + k, 2, i + k, 8);
                                
                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    if (j == 0)
                                    {
                                        sl.SetCellValue(i + k, j + 2, dt.Rows[i][j].ToString());
                                        sl.SetCellStyle(i + k, j + 2, FormattingExcelCells(sl, false));
                                    }
                                    else
                                    {
                                        sl.SetCellValue(i + k, j + 8, Convert.ToDecimal(dt.Rows[i][j].ToString()));
                                        sl.SetCellStyle(i + k, j + 8, FormattingExcelCells(sl, false));
                                    }
                                }
                            }
                            else
                            {
                                var k = 11 + item;
                                //Excel.Range range = worksheet.Range[worksheet.Cells[i + k, 2], worksheet.Cells[i + k, 8]];
                                //range.Merge();

                                sl.MergeWorksheetCells(i + k, 2, i + k, 8);

                                //FormattingExcelCells(range, false, false);
                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    if (j == 0)
                                    {
                                        sl.SetCellValue(i + k, j + 2, dt.Rows[i][j].ToString());
                                        sl.SetCellStyle(i + k, j + 2, FormattingExcelCells(sl, false));
                                    }
                                    else
                                    {
                                        sl.SetCellValue(i + k, j + 8, Convert.ToDecimal(dt.Rows[i][j].ToString()));
                                        sl.SetCellStyle(i + k, j + 8, FormattingExcelCells(sl, false));
                                    }
                                }

                            }

                            final_sum += int.Parse(dt.Rows[i][1].ToString()) * double.Parse(dt.Rows[i][2].ToString()); ;
                            total += int.Parse(dt.Rows[i][1].ToString());

                            //backgroundWorker.ReportProgress(i);
                            item++;
                        }
                        //Кол.во обработанных
                        sl.SetCellValue("I8", total);

                        //Итоговая сумма
                        sl.SetCellValue(dt.Rows.Count + 11 + item, 10, final_sum);

                        sl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\Итог по станции.xlsx");

                        Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\Итог по станции.xlsx");
                    }
                    else
                    {
                        string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx";

                        SLDocument sl = new SLDocument(path);
                        
                        sl.SelectWorksheet("ТОО Казыкурт");

                        string ownerName = comboBox2.Text;

                        int cellRowIndex = 0;
                        double totalSumCost = 0;
                        double totalSumTor = 0;

                        if (ownerName == "Все")
                        {
                            sl.SetCellValue("C4", "всех");
                        }
                        else
                        {
                            sl.SetCellValue("C4", ownerName);
                        }

                        sl.SetCellValue("C6", "в ТОО Казыгурт-Юг c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString());

                        var val = dt.Rows.Count + 16 + getserv.Rows.Count * 2;
                        sl.CopyCell("B13", "K20", "B"+val, true);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            sl.SetCellValue(i + 10, 1,  i + 1);

                            for (int j = 1; j < dt.Columns.Count; j++)
                            {
                                if (j == 1 | j == 2)
                                {
                                    if (j == 1)
                                    {
                                        sl.SetCellValue(i + 10, j + 1, dt.Rows[i][j].ToString());
                                    }
                                    else
                                    {
                                        sl.SetCellValue(i + 10, j + 1, Convert.ToInt32(dt.Rows[i][j].ToString()));
                                    }
                                }
                                else
                                {
                                    if (j == 3)
                                    {
                                        if (dt.Rows[i][j].ToString().Trim() == "8")
                                        {
                                            sl.SetCellValue(i + 10, 5, Convert.ToInt32(dt.Rows[i][j].ToString()));
                                        }
                                        else
                                        {
                                            sl.SetCellValue(i + 10, 4, Convert.ToInt32(dt.Rows[i][j].ToString()));
                                        }
                                    }
                                }
                                if (j >= 4 && j <= 5)
                                {
                                    if (j != 5)
                                    {
                                        sl.SetCellValue(i + 10, j + 3, dt.Rows[i][j].ToString());
                                    }
                                    else
                                    {
                                        sl.SetCellValue(i + 10, j + 3, Convert.ToInt32(dt.Rows[i][j].ToString()));
                                    }
                                }
                                else
                                {
                                    if (j >= 6 && j <= 12)
                                    {
                                        sl.SetCellValue(i + 10, j + 3, dt.Rows[i][j].ToString());
                                    }
                                }
                                if (j > 12)
                                {
                                    if (j == 13)
                                    {
                                        sl.SetCellValue(i + 10, j + 3, dt.Rows[i][j].ToString());
                                    }
                                    else
                                    {
                                        if (j == 14)
                                        {
                                            sl.SetCellValue(i + 10, j + 3, dt.Rows[i][j].ToString());
                                        }
                                        else
                                        {
                                            if (j == 15)
                                            {
                                                sl.SetCellValue(i + 10, j + 3, Convert.ToDecimal(dt.Rows[i][j].ToString()));
                                                totalSumCost += double.Parse(dt.Rows[i][j].ToString());
                                            }
                                            else
                                            {
                                                if (j == 16)
                                                {
                                                    sl.SetCellValue(i + 10, j + 3, Convert.ToDecimal(dt.Rows[i][j].ToString()));
                                                    totalSumTor += double.Parse(dt.Rows[i][j].ToString());
                                                }
                                                else
                                                {
                                                    if (j == 17)
                                                        sl.SetCellValue(i + 10, j + 3, dt.Rows[i][j].ToString());
                                                }
                                            }
                                        }
                                    }

                                }
                            }

                            //backgroundWorker.ReportProgress(i);

                            cellRowIndex++;
                        }

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            for (int j = 1; j < dt.Columns.Count + 3; j++)
                            {
                                sl.SetCellStyle(i + 10, j, FormattingExcelCells(sl,true));
                            }
                        }

                        sl.SetCellValue(dt.Rows.Count + 12, 2, "=C6");
                        sl.SetCellStyle(dt.Rows.Count + 12, 2, FormattingExcelCells(sl, false));

                        if (ownerName == "Все")
                        {
                            sl.SetCellValue(dt.Rows.Count + 14, 2, "Всего обработано вагонов - цистерн всех собственников по видам операций:");
                            sl.SetCellStyle(dt.Rows.Count + 14, 2, FormattingExcelCells(sl, false));
                        }
                        else
                        {
                            sl.SetCellValue(dt.Rows.Count + 14, 2, "Всего обработано вагонов - цистерн " + ownerName + " по видам операций:");
                            sl.SetCellStyle(dt.Rows.Count + 14, 2, FormattingExcelCells(sl, false));
                        }

                        ////Итоговая сводка
                        int rowcount = 0;
                        for (int i = 0; i < getserv.Rows.Count; i++)
                        {
                            rowcount++;
                            for (int j = 0; j < getserv.Columns.Count; j++)
                            {
                                if (j == 0)
                                {
                                    sl.SetCellValue(i + cellRowIndex + 15 + rowcount, j + 2, getserv.Rows[i][j].ToString());
                                    sl.SetCellStyle(i + cellRowIndex + 15 + rowcount, j + 2, FormattingExcelCells(sl, false));
                                }
                                else
                                {
                                    sl.SetCellValue(i + cellRowIndex + 15 + rowcount, j + 12, Convert.ToDecimal(getserv.Rows[i][j].ToString()));
                                    sl.SetCellStyle(i + cellRowIndex + 15 + rowcount, j + 12, FormattingExcelCells(sl, false));
                                }
                            }
                        }
                        
                        //Итоговая сумма
                        sl.SetCellValue(dt.Rows.Count + getserv.Rows.Count * 2 + 16, 14, totalSumTor + totalSumCost);

                        sl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx");
                    
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                if (checkBox2.Checked)
                {

                }
            }
        }

        struct DataParametr
        {
            public string owner { get; set; }
            public int sz { get; set; }
        }

        DataParametr _inputParametr1;


        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            //try
            //{
            //    if (checkBox1.Checked)
            //    {
            //        string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Итог по станции.xlsx";
            //        Excel.Application app = new Excel.Application();
            //        Excel.Workbook workbook = app.Workbooks.Open(path);
            //        Excel.Worksheet worksheet = workbook.Worksheets.get_Item("Итоговая  справка");
            //        app.Visible = false;
            //        object misValue = System.Reflection.Missing.Value;

            //        worksheet.Range["B6"].Value = "c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString();

            //        worksheet.Range["B13:H24"].Cut(worksheet.Cells[gridView1.RowCount * 2 + 11, 2]);

            //        int item = 0;

            //        //Кол.во услуг
            //        int total = 0;

            //        //Сумм * Кол.во
            //        double final_sum = 0;

            //        for (int i = 0; i < gridView1.RowCount; i++)
            //        {
            //            if (i % 2 == 0)
            //            {
            //                var k = 11 + item;
            //                Excel.Range range = worksheet.Range[worksheet.Cells[i + k, 2], worksheet.Cells[i + k, 8]];
            //                range.Merge();
            //                FormattingExcelCells(range, false, false);
            //                for (int j = 0; j < dt.Columns.Count; j++)
            //                {
            //                    if (j == 0)
            //                    {
            //                        worksheet.Cells[i + k, j + 2] = dt.Rows[i][j].ToString();
            //                    }
            //                    else
            //                    {
            //                        worksheet.Cells[i + k, j + 8] = Convert.ToDecimal(dt.Rows[i][j].ToString());
            //                    }

            //                }
            //            }
            //            else
            //            {
            //                var k = 11 + item;
            //                Excel.Range range = worksheet.Range[worksheet.Cells[i + k, 2], worksheet.Cells[i + k, 8]];
            //                range.Merge();
            //                FormattingExcelCells(range, false, false);
            //                for (int j = 0; j < dt.Columns.Count; j++)
            //                {
            //                    if (j == 0)
            //                    {
            //                        worksheet.Cells[i + k, j + 2] = dt.Rows[i][j].ToString();
            //                    }
            //                    else
            //                    {
            //                        worksheet.Cells[i + k, j + 8] = Convert.ToDecimal(dt.Rows[i][j].ToString());
            //                    }
            //                }

            //            }

            //            final_sum += int.Parse(dt.Rows[i][1].ToString()) * double.Parse(dt.Rows[i][2].ToString()); ;
            //            total += int.Parse(dt.Rows[i][1].ToString());

            //            //backgroundWorker.ReportProgress(i);
            //            item++;
            //        }
            //        //Кол.во обработанных
            //        worksheet.Range["I8"].Value = total;

            //        //Итоговая сумма
            //        worksheet.Cells[dt.Rows.Count + 11 + item, 10] = final_sum;

            //        app.DisplayAlerts = false;
            //        workbook.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\Итог по станции.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //        workbook.Close(0);
            //        app.Quit();

            //        releaseObject(workbook);
            //        releaseObject(worksheet);
            //        releaseObject(app);

            //        Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\Итог по станции.xlsx");
            //    }
            //    else
            //    {
            //        if (checkBox2.Checked)
            //        {
            //            DataTable dataTable, Itog_Rep;

            //            string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx";

            //            Excel.Application app = new Excel.Application();
            //            Excel.Workbook workbook = app.Workbooks.Open(path);
            //            Excel.Worksheet xlworksheet = workbook.Worksheets.get_Item("ТОО Казыкурт");
            //            xlworksheet.Name = "Реестр";
            //            object misValue = System.Reflection.Missing.Value;

            //            string RefreshAllCount = "exec dbo.GetReportAllRenderedService_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'," + "@Type = " + 2;
            //            dt = DbConnection.DBConnect(RefreshAllCount);

            //            for (int i = 0; i < dt.Rows.Count; i++)
            //            {
            //                xlworksheet.Copy(Type.Missing, xlworksheet);
            //            }

            //            int k = 0;

            //            //int iterator = 0;

            //            foreach (Excel.Worksheet worksheet in workbook.Worksheets)
            //            {
            //                if (worksheet.Name == "Реестр")
            //                {
            //                    string RefreshAll = "exec dbo.GetReportAllRenderedService_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'," + "@Type = " + 1;
            //                    dataTable = DbConnection.DBConnect(RefreshAll);

            //                    string GetCountServiceCost = "exec dbo.Itog_All_Report '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
            //                    Itog_Rep = DbConnection.DBConnect(GetCountServiceCost);

            //                    int cellRowIndex = 0;
            //                    double totalSumCost = 0;
            //                    double totalSumTor = 0;

            //                    worksheet.Range["C6"].Value = "в ТОО Казыгурт-Юг c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString();

            //                    FormattingExcelCells(worksheet.Range["C6"], false, false);

            //                    worksheet.Range["B13:K22"].Cut(worksheet.Cells[dataTable.Rows.Count + 16 + Itog_Rep.Rows.Count * 2, 2]);

            //                    worksheet.Range["P:P"].NumberFormat = "@";

            //                    for (int l = 0; l < dataTable.Rows.Count; l++)
            //                    {
            //                        worksheet.Cells[l + 10, 1] = l + 1;

            //                        for (int j = 1; j < dataTable.Columns.Count; j++)
            //                        {
            //                            if (j != 3 && j < 4)
            //                            {
            //                                worksheet.Cells[l + 10, j + 1] = dataTable.Rows[l][j].ToString();
            //                            }
            //                            else
            //                            {
            //                                if (j == 3)
            //                                {
            //                                    if (dataTable.Rows[l][j].ToString().Trim() == "8")
            //                                    {
            //                                        worksheet.Cells[l + 10, 5] = dataTable.Rows[l][j].ToString();
            //                                    }
            //                                    else
            //                                    {
            //                                        worksheet.Cells[l + 10, 4] = dataTable.Rows[l][j].ToString();
            //                                    }
            //                                }
            //                            }
            //                            if (j >= 4 && j <= 5)
            //                            {
            //                                worksheet.Cells[l + 10, j + 3] = dataTable.Rows[l][j].ToString();
            //                            }
            //                            else
            //                            {
            //                                if (j >= 6 && j <= 12)
            //                                {
            //                                    worksheet.Cells[l + 10, j + 3] = dataTable.Rows[l][j].ToString();
            //                                }
            //                            }
            //                            if (j > 12)
            //                            {
            //                                if (j == 13)
            //                                {
            //                                    worksheet.Cells[l + 10, j + 3] = dataTable.Rows[l][j].ToString();
            //                                }
            //                                else
            //                                {
            //                                    if (j == 14)
            //                                    {
            //                                        worksheet.Cells[l + 10, j + 3] = dataTable.Rows[l][j].ToString();
            //                                    }
            //                                    else
            //                                    {
            //                                        if (j == 15)
            //                                        {
            //                                            worksheet.Cells[l + 10, j + 3] = Convert.ToDecimal(dataTable.Rows[l][j].ToString());
            //                                            totalSumCost += double.Parse(dataTable.Rows[l][j].ToString());
            //                                        }
            //                                        else
            //                                        {
            //                                            if (j == 16)
            //                                            {
            //                                                worksheet.Cells[l + 10, j + 3] = Convert.ToDecimal(dataTable.Rows[l][j].ToString());
            //                                                totalSumTor += double.Parse(dataTable.Rows[l][j].ToString());
            //                                            }
            //                                            else
            //                                            {
            //                                                if (j == 17)
            //                                                    worksheet.Cells[l + 10, j + 3] = dataTable.Rows[l][j].ToString();
            //                                            }
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                        }

            //                        Excel.Range range = worksheet.Range[worksheet.Cells[l + 10, 1], worksheet.Cells[l + 10, dataTable.Columns.Count + 2]];
            //                        FormattingExcelCells(range, true, true);

            //                        cellRowIndex++;
            //                        //iterator++;

            //                        //backgroundWorker.ReportProgress(iterator);
            //                    }

            //                    worksheet.Cells[dataTable.Rows.Count + 12, 2] = "=C6";

            //                    worksheet.Cells[dataTable.Rows.Count + 14, 2] = "Всего обработано вагонов - цистерн всех собственников по видам операций:";

            //                    ////Итоговая сводка
            //                    int rowcount = 0;
            //                    for (int l = 0; l < Itog_Rep.Rows.Count; l++)
            //                    {
            //                        rowcount++;
            //                        for (int j = 0; j < Itog_Rep.Columns.Count; j++)
            //                        {
            //                            if (j == 0)
            //                            {
            //                                worksheet.Cells[l + cellRowIndex + 15 + rowcount, j + 2] = Itog_Rep.Rows[l][j].ToString();
            //                            }
            //                            else
            //                            {
            //                                worksheet.Cells[l + cellRowIndex + 15 + rowcount, j + 12] = Convert.ToDecimal(Itog_Rep.Rows[l][j].ToString());
            //                            }
            //                        }
            //                    }

            //                    worksheet.Cells[dataTable.Rows.Count + 14, 13] = cellRowIndex;

            //                    ////Итоговая сумма
            //                    worksheet.Cells[dataTable.Rows.Count + Itog_Rep.Rows.Count * 2 + 16, 14] = totalSumTor + totalSumCost;

            //                    Excel.Range range1 = worksheet.Range[worksheet.Cells[dataTable.Rows.Count + 12, 2], worksheet.Cells[dataTable.Rows.Count + Itog_Rep.Rows.Count * 2 + 19, 14]];
            //                    FormattingExcelCells(range1, false, false);
            //                }
            //                else
            //                {
            //                    string Refresh = "dbo.GetReportRenderedServices_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + dt.Rows[k][0].ToString() + "'";
            //                    dataTable = DbConnection.DBConnect(Refresh);

            //                    string GetCountServiceCost = "exec dbo.Itog_Report  '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + dt.Rows[k][0].ToString() + "'";
            //                    Itog_Rep = DbConnection.DBConnect(GetCountServiceCost);

            //                    int cellRowIndex = 0;
            //                    double totalSumCost = 0;
            //                    double totalSumTor = 0;

            //                    worksheet.Name = dt.Rows[k][1].ToString().Trim();

            //                    worksheet.Range["C4"].Value = dt.Rows[k][1].ToString();

            //                    worksheet.Range["C6"].Value = "в ТОО Казыгурт-Юг c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString();

            //                    FormattingExcelCells(worksheet.Range["C6"], false, false);

            //                    worksheet.Range["B13:K22"].Cut(worksheet.Cells[dataTable.Rows.Count + 16 + Itog_Rep.Rows.Count * 2, 2]);

            //                    worksheet.Range["P:P"].NumberFormat = "@";

            //                    for (int l = 0; l < dataTable.Rows.Count; l++)
            //                    {
            //                        worksheet.Cells[l + 10, 1] = l + 1;

            //                        for (int j = 1; j < dataTable.Columns.Count; j++)
            //                        {
            //                            if (j != 3 && j < 4)
            //                            {
            //                                worksheet.Cells[l + 10, j + 1] = dataTable.Rows[l][j].ToString();
            //                            }
            //                            else
            //                            {
            //                                if (j == 3)
            //                                {
            //                                    if (dataTable.Rows[l][j].ToString().Trim() == "8")
            //                                    {
            //                                        worksheet.Cells[l + 10, 5] = dataTable.Rows[l][j].ToString();
            //                                    }
            //                                    else
            //                                    {
            //                                        worksheet.Cells[l + 10, 4] = dataTable.Rows[l][j].ToString();
            //                                    }
            //                                }
            //                            }
            //                            if (j >= 4 && j <= 5)
            //                            {
            //                                worksheet.Cells[l + 10, j + 3] = dataTable.Rows[l][j].ToString();
            //                            }
            //                            else
            //                            {
            //                                if (j >= 6 && j <= 12)
            //                                {
            //                                    worksheet.Cells[l + 10, j + 3] = dataTable.Rows[l][j].ToString();
            //                                }
            //                            }
            //                            if (j > 12)
            //                            {
            //                                if (j == 13)
            //                                {
            //                                    worksheet.Cells[l + 10, j + 3] = dataTable.Rows[l][j].ToString();
            //                                }
            //                                else
            //                                {
            //                                    if (j == 14)
            //                                    {
            //                                        worksheet.Cells[l + 10, j + 3] = dataTable.Rows[l][j].ToString();
            //                                    }
            //                                    else
            //                                    {
            //                                        if (j == 15)
            //                                        {
            //                                            worksheet.Cells[l + 10, j + 3] = Convert.ToDecimal(dataTable.Rows[l][j].ToString());
            //                                            totalSumCost += double.Parse(dataTable.Rows[l][j].ToString());
            //                                        }
            //                                        else
            //                                        {
            //                                            if (j == 16)
            //                                            {
            //                                                worksheet.Cells[l + 10, j + 3] = Convert.ToDecimal(dataTable.Rows[l][j].ToString());
            //                                                totalSumTor += double.Parse(dataTable.Rows[l][j].ToString());
            //                                            }
            //                                            else
            //                                            {
            //                                                if (j == 17)
            //                                                    worksheet.Cells[l + 10, j + 3] = dataTable.Rows[l][j].ToString();
            //                                            }
            //                                        }
            //                                    }
            //                                }
            //                            }
            //                        }

            //                        Excel.Range range = worksheet.Range[worksheet.Cells[l + 10, 1], worksheet.Cells[l + 10, dataTable.Columns.Count + 2]];
            //                        FormattingExcelCells(range, true, true);

            //                        cellRowIndex++;

            //                        //iterator++;

            //                        //backgroundWorker.ReportProgress(iterator);
            //                    }

            //                    worksheet.Cells[dataTable.Rows.Count + 12, 2] = "=C6";

            //                    worksheet.Cells[dataTable.Rows.Count + 14, 2] = "Всего обработано вагонов - цистерн " + dt.Rows[k][1].ToString() + " по видам операций:";

            //                    ////Итоговая сводка
            //                    int rowcount = 0;
            //                    for (int l = 0; l < Itog_Rep.Rows.Count; l++)
            //                    {
            //                        rowcount++;
            //                        for (int j = 0; j < Itog_Rep.Columns.Count; j++)
            //                        {
            //                            if (j == 0)
            //                            {
            //                                worksheet.Cells[l + cellRowIndex + 15 + rowcount, j + 2] = Itog_Rep.Rows[l][j].ToString();
            //                            }
            //                            else
            //                            {
            //                                worksheet.Cells[l + cellRowIndex + 15 + rowcount, j + 12] = Convert.ToDecimal(Itog_Rep.Rows[l][j].ToString());
            //                            }
            //                        }
            //                    }

            //                    worksheet.Cells[dataTable.Rows.Count + 14, 13] = cellRowIndex;

            //                    ////Итоговая сумма
            //                    worksheet.Cells[dataTable.Rows.Count + Itog_Rep.Rows.Count * 2 + 16, 14] = totalSumTor + totalSumCost;

            //                    Excel.Range range1 = worksheet.Range[worksheet.Cells[dataTable.Rows.Count + 12, 2], worksheet.Cells[dataTable.Rows.Count + Itog_Rep.Rows.Count * 2 + 19, 14]];
            //                    FormattingExcelCells(range1, false, false);

            //                    k++;
            //                }
            //            }

            //            app.DisplayAlerts = false;

            //            workbook.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\Общий Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //            workbook.Close(0);
            //            app.Quit();

            //            releaseObject(workbook);
            //            releaseObject(xlworksheet);
            //            releaseObject(app);

            //            Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\Общий Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx");
            //        }
            //        else
            //        {
            //            string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx";

            //            string ownerName = ((DataParametr)e.Argument).owner;

            //            Excel.Application app = new Excel.Application();
            //            Excel.Workbook workbook = app.Workbooks.Open(path);
            //            Excel.Worksheet worksheet = workbook.Worksheets.get_Item("ТОО Казыкурт");
            //            app.Visible = false;
            //            object misValue = System.Reflection.Missing.Value;

            //            int cellRowIndex = 0;
            //            double totalSumCost = 0;
            //            double totalSumTor = 0;

            //            if (ownerName == "Все")
            //            {
            //                worksheet.Range["C4"].Value = "всех";
            //            }
            //            else
            //            {
            //                worksheet.Range["C4"].Value = ownerName;
            //            }

            //            worksheet.Range["C6"].Value = "в ТОО Казыгурт-Юг c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString();

            //            FormattingExcelCells(worksheet.Range["C6"], false, false);

            //            worksheet.Range["B13:K22"].Cut(worksheet.Cells[dt.Rows.Count + 16 + getserv.Rows.Count * 2, 2]);

            //            worksheet.Range["P:P"].NumberFormat = "@";

            //            for (int i = 0; i < dt.Rows.Count; i++)
            //            {
            //                worksheet.Cells[i + 10, 1] = i + 1;

            //                for (int j = 1; j < dt.Columns.Count; j++)
            //                {
            //                    if (j != 3 && j < 4)
            //                    {
            //                        worksheet.Cells[i + 10, j + 1] = dt.Rows[i][j].ToString();
            //                    }
            //                    else
            //                    {
            //                        if (j == 3)
            //                        {
            //                            if (dt.Rows[i][j].ToString().Trim() == "8")
            //                            {
            //                                worksheet.Cells[i + 10, 5] = dt.Rows[i][j].ToString();
            //                            }
            //                            else
            //                            {
            //                                worksheet.Cells[i + 10, 4] = dt.Rows[i][j].ToString();
            //                            }
            //                        }
            //                    }
            //                    if (j >= 4 && j <= 5)
            //                    {
            //                        worksheet.Cells[i + 10, j + 3] = dt.Rows[i][j].ToString();
            //                    }
            //                    else
            //                    {
            //                        if (j >= 6 && j <= 12)
            //                        {
            //                            worksheet.Cells[i + 10, j + 3] = dt.Rows[i][j].ToString();
            //                        }
            //                    }
            //                    if (j > 12)
            //                    {
            //                        if (j == 13)
            //                        {
            //                            worksheet.Cells[i + 10, j + 3] = dt.Rows[i][j].ToString();
            //                        }
            //                        else
            //                        {
            //                            if (j == 14)
            //                            {
            //                                worksheet.Cells[i + 10, j + 3] = dt.Rows[i][j].ToString();
            //                            }
            //                            else
            //                            {
            //                                if (j == 15)
            //                                {
            //                                    worksheet.Cells[i + 10, j + 3] = Convert.ToDecimal(dt.Rows[i][j].ToString());
            //                                    totalSumCost += double.Parse(dt.Rows[i][j].ToString());
            //                                }
            //                                else
            //                                {
            //                                    if (j == 16)
            //                                    {
            //                                        worksheet.Cells[i + 10, j + 3] = Convert.ToDecimal(dt.Rows[i][j].ToString());
            //                                        totalSumTor += double.Parse(dt.Rows[i][j].ToString());
            //                                    }
            //                                    else
            //                                    {
            //                                        if (j == 17)
            //                                            worksheet.Cells[i + 10, j + 3] = dt.Rows[i][j].ToString();
            //                                    }
            //                                }
            //                            }
            //                        }

            //                    }
            //                }

            //                Excel.Range range = worksheet.Range[worksheet.Cells[i + 10, 1], worksheet.Cells[i + 10, dt.Columns.Count + 2]];
            //                FormattingExcelCells(range, true, true);

            //                //backgroundWorker.ReportProgress(i);

            //                cellRowIndex++;
            //            }

            //            ////worksheet.Range[dt.Rows.Count+10, 13].NumberFormat = "#,##0.00";

            //            worksheet.Cells[dt.Rows.Count + 12, 2] = "=C6";

            //            if (ownerName == "Все")
            //            {
            //                worksheet.Cells[dt.Rows.Count + 14, 2] = "Всего обработано вагонов - цистерн всех собственников по видам операций:";
            //            }
            //            else
            //            {
            //                worksheet.Cells[dt.Rows.Count + 14, 2] = "Всего обработано вагонов - цистерн " + ownerName + " по видам операций:";
            //            }

            //            ////Итоговая сводка
            //            int rowcount = 0;
            //            for (int i = 0; i < getserv.Rows.Count; i++)
            //            {
            //                rowcount++;
            //                for (int j = 0; j < getserv.Columns.Count; j++)
            //                {
            //                    if (j == 0)
            //                    {
            //                        worksheet.Cells[i + cellRowIndex + 15 + rowcount, j + 2] = getserv.Rows[i][j].ToString();
            //                    }
            //                    else
            //                    {
            //                        worksheet.Cells[i + cellRowIndex + 15 + rowcount, j + 12] = Convert.ToDecimal(getserv.Rows[i][j].ToString());
            //                    }
            //                }
            //            }

            //            worksheet.Cells[dt.Rows.Count + 14, 13] = cellRowIndex;

            //            ////Итоговая сумма
            //            worksheet.Cells[dt.Rows.Count + getserv.Rows.Count * 2 + 16, 14] = totalSumTor + totalSumCost;

            //            Excel.Range range1 = worksheet.Range[worksheet.Cells[dt.Rows.Count + 12, 2], worksheet.Cells[dt.Rows.Count + getserv.Rows.Count * 2 + 19, 14]];
            //            FormattingExcelCells(range1, false, false);

            //            app.DisplayAlerts = false;

            //            workbook.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx", Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //            workbook.Close(0);
            //            app.Quit();

            //            releaseObject(workbook);
            //            releaseObject(worksheet);
            //            releaseObject(app);

            //            Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        public SLStyle FormattingExcelCells(SLDocument sl, bool val)
        {
            SLStyle style1 = sl.CreateStyle();

            if (val == true)
            {
                style1.SetBottomBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                style1.SetTopBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                style1.SetLeftBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                style1.SetRightBorder(BorderStyleValues.Thin, System.Drawing.Color.Black);
                style1.Font.FontName = "Arial Cyr";
                style1.Font.FontSize = 9;
                style1.Font.Bold = true;
                style1.Alignment.Horizontal = HorizontalAlignmentValues.Center;

                return style1;
            }
            else
            {
                SLStyle style2 = sl.CreateStyle();
                style2.Font.FontName = "Arial Cyr";
                style2.Font.FontSize = 9;
                style2.Font.Bold = true;
                //style2.Alignment.Horizontal = HorizontalAlignmentValues.Center;

                return style2;
            }
        }

        private void BackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            //if (checkBox2.Checked)
            //{

            //}
            //else
            //{
            //    //progressBar.Value = e.ProgressPercentage;
            //    //LblStatus.Text = "Обработка строки.. " + e.ProgressPercentage.ToString() /*+ " из " + TotalRow()*/;
            //}
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                //Thread.Sleep(1);
                toolStripLabel1.Text = "Данные были успешно экспортированы";
                Thread.Sleep(1);
                toolStripLabel1.Text = "...";
                //progressBar.Value = 0;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                Refresh();
            }
            else
            {
                //ReportForm_Load(null, null);
                Refresh();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Checked)
            {
                Refresh();
                if (checkBox3.Checked)
                {
                    return;
                }
                else
                {
                    dateTimePicker2.Value = dateTimePicker1.Value;
                }
            }
            else
            {
                return;
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if(dateTimePicker2.Checked)
            {
                Refresh();
            }
            else
            {
                return;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                checkBox1.Checked = false;
                comboBox2.Enabled = false;
                comboBox2.SelectedIndex = 0;
                Refresh();
            }
            else
            {
                comboBox2.Enabled = true;
                Refresh();
                //ReportForm_Load(null, null);
            }

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

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                dateTimePicker2.Enabled = (checkBox3.CheckState == CheckState.Checked);
            }
            else
            {
                dateTimePicker2.Enabled = false;
                dateTimePicker2.Value = dateTimePicker1.Value;
            }
        }
    }
}