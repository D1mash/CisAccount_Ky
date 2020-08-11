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
        string role;

        public ReportForm(string role)
        {
            InitializeComponent();
            this.role = role;
        }
        
        private new void Refresh()
        {
            if (checkBox1.Checked)
            {
                if (comboBox2.SelectedIndex == 0)
                {
                    string Itog_All_Report = "exec dbo.Itog_All_Report '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                    dt = DbConnection.DBConnect(Itog_All_Report);
                    
                    toolStripLabel1.Text = TotalRow(dt).ToString();
                }
                else
                {
                    string Itog_Report = "exec dbo.Itog_Report  '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
                    dt = DbConnection.DBConnect(Itog_Report);
                    
                    toolStripLabel1.Text = TotalRow(dt).ToString();
                }
            }
            else
            {
                if (checkBox2.Checked)
                {
                    string RefreshAll = "exec dbo.GetReportAllRenderedService_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'," + "@Type = " + 1;
                    dt = DbConnection.DBConnect(RefreshAll);
                    
                    toolStripLabel1.Text = TotalRow(dt).ToString();
                }
                else
                {
                    if (checkBox5.Checked)
                    {
                        string RefreshAll = "exec [dbo].[GetReportAUTN] '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                        dt = DbConnection.DBConnect(RefreshAll);

                        toolStripLabel1.Text = TotalRow(dt).ToString();
                    }
                    else
                    {
                        if (checkBox4.Checked)
                        {
                            string GetSNO = "exec dbo.GetSNO '" + dateTimePicker1.Value.Date.ToShortDateString() + "', '" + dateTimePicker2.Value.Date.ToShortDateString() + "'";
                            dt = DbConnection.DBConnect(GetSNO);
                        }
                        else
                        {
                            if (checkBox6.Checked)
                            {
                                string GetSNO = "exec dbo.GetCurrentSNO '" + dateTimePicker1.Value.Date.ToShortDateString() + "', '" + dateTimePicker2.Value.Date.ToShortDateString() + "'";
                                dt = DbConnection.DBConnect(GetSNO);
                            }
                            else
                            {
                                comboBox2.Enabled = true;

                                if (comboBox2.SelectedIndex == 0)
                                {
                                    string RefreshAll = "exec dbo.GetReportAllRenderedService_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'," + "@Type = " + 1;
                                    dt = DbConnection.DBConnect(RefreshAll);

                                    toolStripLabel1.Text = TotalRow(dt).ToString();

                                    string GetCountServiceCost = "exec dbo.Itog_All_Report '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                                    getserv = DbConnection.DBConnect(GetCountServiceCost);
                                }
                                else
                                {
                                    string Refresh = "dbo.GetReportRenderedServices_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
                                    dt = DbConnection.DBConnect(Refresh);

                                    toolStripLabel1.Text = TotalRow(dt).ToString();

                                    string GetCountServiceCost = "exec dbo.Itog_Report  '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
                                    getserv = DbConnection.DBConnect(GetCountServiceCost);
                                }
                            }
                        }
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
            if(role == "1" || role == "2")
            {
                checkBox4.Enabled = false;
                checkBox6.Enabled = false;
            }
            else
            {
                return;
            }

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

            //Refresh();
            checkBox3_CheckedChanged(null, null);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    Refresh();
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Итог по станции.xlsx";

                    using (SLDocument sl = new SLDocument(path))
                    {
                        sl.SelectWorksheet("Итоговая  справка");

                        sl.SetCellValue("B6", "c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString());

                        var val = dt.Rows.Count * 2 + 11;
                        sl.CopyCell("B13", "H24", "B" + val, true);
                        //worksheet.Range["B13:H24"].Cut(worksheet.Cells[gridView1.RowCount * 2 + 11, 2]);

                        int item = 0;

                        //Кол.во услуг
                        int total = 0;

                        //Сумм * Кол.во
                        double final_sum = 0;

                        for (int i = 0; i < dt.Rows.Count; i++)
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

                            if (i<dt.Rows.Count)
                            {
                                total += int.Parse(dt.Rows[i][1].ToString());
                            }
                            else
                            {
                                continue;
                            }

                            //backgroundWorker.ReportProgress(i);
                            item++;
                        }
                        //Кол.во обработанных
                        sl.SetCellValue("I8", total);

                        //Итоговая сумма
                        sl.SetCellValue(dt.Rows.Count + 12 + item, 10, final_sum);
                        sl.SetCellStyle(dt.Rows.Count + 12 + item, 10, FormattingExcelCells(sl, false));

                        sl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\Итог по станции.xlsx");
                    }

                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\Итог по станции.xlsx");
                }
                else
                {
                    if (checkBox5.Checked)
                    {
                        Refresh();
                        string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Реестр АУТН.xlsx";

                        using (SLDocument sl = new SLDocument(path))
                        {
                            sl.SelectWorksheet("АУТН");

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                for (int j = 0; j < dt.Columns.Count; j++)
                                {
                                    sl.SetCellValue(i + 3, 1, i);
                                    sl.SetCellStyle(i + 3, 1, FormattingExcelCells(sl, true));
                                    if (j == 1)
                                    {
                                        sl.SetCellValue(i + 3, j + 2, Convert.ToInt32(dt.Rows[i][j].ToString()));
                                        sl.SetCellStyle(i + 3, j + 2, FormattingExcelCells(sl, true));
                                    }
                                    else
                                    {
                                        sl.SetCellValue(i + 3, j + 2, dt.Rows[i][j].ToString());
                                        sl.SetCellStyle(i + 3, j + 2, FormattingExcelCells(sl, true));
                                    }
                                }

                                //backgroundWorker.ReportProgress(i);
                            }
                            sl.AutoFitColumn(1, 9);


                            sl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\Реестр АУТН.xlsx");

                        }
                        Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\Реестр АУТН.xlsx");
                    }
                    else
                    {
                        if (checkBox2.Checked)
                        {
                            Refresh();

                            DataTable dataTable, Itog_Rep;
                            string Name;

                            string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx";

                            using (SLDocument sl = new SLDocument(path))
                            {

                                sl.SelectWorksheet("ТОО Казыкурт");
                                sl.RenameWorksheet("ТОО Казыкурт", "Реестр");
                                sl.AddWorksheet("Temp");

                                string RefreshAllCount = "exec dbo.GetReportAllRenderedService_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'," + "@Type = " + 2;
                                dt = DbConnection.DBConnect(RefreshAllCount);

                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    for (int j = 0; j < dt.Columns.Count; j++)
                                    {
                                        if (j == 1)
                                        {
                                            Name = dt.Rows[i][j].ToString();
                                            sl.CopyWorksheet("Реестр", Name);
                                        }
                                    }
                                }

                                sl.SelectWorksheet("Реестр");
                                sl.DeleteWorksheet("Temp");

                                int k = 0;

                                foreach (var name in sl.GetWorksheetNames())
                                {
                                    if (name == "Реестр")
                                    {
                                        string RefreshAll = "exec dbo.GetReportAllRenderedService_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'," + "@Type = " + 1;
                                        dataTable = DbConnection.DBConnect(RefreshAll);

                                        string GetCountServiceCost = "exec dbo.Itog_All_Report '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                                        Itog_Rep = DbConnection.DBConnect(GetCountServiceCost);

                                        sl.SelectWorksheet(name);

                                        int cellRowIndex = 0;
                                        double totalSumCost = 0;
                                        double totalSumTor = 0;

                                        sl.SetCellValue("C6", "в ТОО Казыгурт-Юг c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString());

                                        var val = dataTable.Rows.Count + 16 + Itog_Rep.Rows.Count * 2;
                                        sl.CopyCell("B13", "K20", "B" + val, true);

                                        for (int l = 0; l < dataTable.Rows.Count; l++)
                                        {
                                            sl.SetCellValue(l + 10, 1, l + 1);

                                            for (int j = 1; j < dataTable.Columns.Count; j++)
                                            {
                                                if (j == 1 | j == 2)
                                                {
                                                    if (j == 1)
                                                    {
                                                        sl.SetCellValue(l + 10, j + 1, dataTable.Rows[l][j].ToString());
                                                    }
                                                    else
                                                    {
                                                        sl.SetCellValue(l + 10, j + 1, Convert.ToInt32(dataTable.Rows[l][j].ToString()));
                                                    }
                                                }
                                                else
                                                {
                                                    if (j == 3)
                                                    {
                                                        if (dataTable.Rows[l][j].ToString().Trim() == "8")
                                                        {
                                                            sl.SetCellValue(l + 10, 5, Convert.ToInt32(dataTable.Rows[l][j].ToString()));
                                                        }
                                                        else
                                                        {
                                                            sl.SetCellValue(l + 10, 4, Convert.ToInt32(dataTable.Rows[l][j].ToString()));
                                                        }
                                                    }
                                                }
                                                if (j >= 4 && j <= 5)
                                                {
                                                    if (j != 5)
                                                    {
                                                        sl.SetCellValue(l + 10, j + 3, dataTable.Rows[l][j].ToString());
                                                    }
                                                    else
                                                    {
                                                        sl.SetCellValue(l + 10, j + 3, Convert.ToInt32(dataTable.Rows[l][j].ToString()));
                                                    }
                                                }
                                                else
                                                {
                                                    if (j >= 6 && j <= 12)
                                                    {
                                                        sl.SetCellValue(l + 10, j + 3, dataTable.Rows[l][j].ToString());
                                                    }
                                                }
                                                if (j > 12)
                                                {
                                                    if (j == 13)
                                                    {
                                                        sl.SetCellValue(l + 10, j + 3, dataTable.Rows[l][j].ToString());
                                                    }
                                                    else
                                                    {
                                                        if (j == 14)
                                                        {
                                                            sl.SetCellValue(l + 10, j + 3, dataTable.Rows[l][j].ToString());
                                                        }
                                                        else
                                                        {
                                                            if (j == 15)
                                                            {
                                                                sl.SetCellValue(l + 10, j + 3, Convert.ToDecimal(dataTable.Rows[l][j].ToString()));
                                                                totalSumCost += double.Parse(dataTable.Rows[l][j].ToString());
                                                            }
                                                            else
                                                            {
                                                                if (j == 16)
                                                                {
                                                                    sl.SetCellValue(l + 10, j + 3, Convert.ToDecimal(dataTable.Rows[l][j].ToString()));
                                                                    totalSumTor += double.Parse(dataTable.Rows[l][j].ToString());
                                                                }
                                                                else
                                                                {
                                                                    if (j == 17)
                                                                        sl.SetCellValue(l + 10, j + 3, dataTable.Rows[l][j].ToString());
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            cellRowIndex++;

                                            //backgroundWorker.ReportProgress(iterator);
                                        }

                                        for (int i = 0; i < dataTable.Rows.Count; i++)
                                        {
                                            for (int j = 1; j < dataTable.Columns.Count + 3; j++)
                                            {
                                                sl.SetCellStyle(i + 10, j, FormattingExcelCells(sl, true));
                                            }
                                        }

                                        sl.SetCellValue(dataTable.Rows.Count + 12, 2, "=C6");
                                        sl.SetCellStyle(dataTable.Rows.Count + 12, 2, FormattingExcelCells(sl, false));

                                        sl.SetCellValue(dataTable.Rows.Count + 14, 2, "Всего обработано вагонов - цистерн всех собственников по видам операций:");
                                        sl.SetCellStyle(dataTable.Rows.Count + 14, 2, FormattingExcelCells(sl, false));

                                        //Итоговая сводка
                                        int rowcount = 0;
                                        int total = 0;
                                        for (int i = 0; i < Itog_Rep.Rows.Count; i++)
                                        {
                                            rowcount++;
                                            for (int j = 0; j < Itog_Rep.Columns.Count; j++)
                                            {
                                                if (j == 0)
                                                {
                                                    sl.SetCellValue(i + cellRowIndex + 15 + rowcount, j + 2, Itog_Rep.Rows[i][j].ToString());
                                                    sl.SetCellStyle(i + cellRowIndex + 15 + rowcount, j + 2, FormattingExcelCells(sl, false));
                                                }
                                                else
                                                {
                                                    sl.SetCellValue(i + cellRowIndex + 15 + rowcount, j + 12, Convert.ToDecimal(Itog_Rep.Rows[i][j].ToString()));
                                                    sl.SetCellStyle(i + cellRowIndex + 15 + rowcount, j + 12, FormattingExcelCells(sl, false));
                                                }
                                            }
                                            if (i < Itog_Rep.Rows.Count - 1)
                                            {
                                                total += int.Parse(Itog_Rep.Rows[i][1].ToString());
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }

                                        sl.SetCellValue(dataTable.Rows.Count + 14, 13, total);
                                        sl.SetCellStyle(dataTable.Rows.Count + 14, 13, FormattingExcelCells(sl, false));
                                    

                                        //Итоговая сумма
                                        sl.SetCellValue(dataTable.Rows.Count + Itog_Rep.Rows.Count * 2 + 16, 14, totalSumTor + totalSumCost);
                                        sl.SetCellStyle(dataTable.Rows.Count + Itog_Rep.Rows.Count * 2 + 16, 14, FormattingExcelCells(sl, false));
                                    }
                                    else
                                    {
                                        int cs = Convert.ToInt32(dt.Rows[k][0].ToString());
                                        string Refresh = "dbo.GetReportRenderedServices_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + cs + "'";
                                        dataTable = DbConnection.DBConnect(Refresh);

                                        string GetCountServiceCost = "exec dbo.Itog_Report  '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + cs + "'";
                                        Itog_Rep = DbConnection.DBConnect(GetCountServiceCost);

                                        int cellRowIndex = 0;
                                        double totalSumCost = 0;
                                        double totalSumTor = 0;

                                        Name = dt.Rows[k][1].ToString();
                                        sl.SelectWorksheet(Name);

                                        sl.SetCellValue("C4", dt.Rows[k][1].ToString());

                                        sl.SetCellValue("C6", "в ТОО Казыгурт-Юг c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString());

                                        var val = dataTable.Rows.Count + 16 + Itog_Rep.Rows.Count * 2;
                                        sl.CopyCell("B13", "K20", "B" + val, true);

                                        for (int l = 0; l < dataTable.Rows.Count; l++)
                                        {
                                            sl.SetCellValue(l + 10, 1, l + 1);

                                            for (int j = 1; j < dataTable.Columns.Count; j++)
                                            {
                                                if (j == 1 | j == 2)
                                                {
                                                    if (j == 1)
                                                    {
                                                        sl.SetCellValue(l + 10, j + 1, dataTable.Rows[l][j].ToString());
                                                    }
                                                    else
                                                    {
                                                        sl.SetCellValue(l + 10, j + 1, Convert.ToInt32(dataTable.Rows[l][j].ToString()));
                                                    }
                                                }
                                                else
                                                {
                                                    if (j == 3)
                                                    {
                                                        if (dataTable.Rows[l][j].ToString().Trim() == "8")
                                                        {
                                                            sl.SetCellValue(l + 10, 5, Convert.ToInt32(dataTable.Rows[l][j].ToString()));
                                                        }
                                                        else
                                                        {
                                                            sl.SetCellValue(l + 10, 4, Convert.ToInt32(dataTable.Rows[l][j].ToString()));
                                                        }
                                                    }
                                                }
                                                if (j >= 4 && j <= 5)
                                                {
                                                    if (j != 5)
                                                    {
                                                        sl.SetCellValue(l + 10, j + 3, dataTable.Rows[l][j].ToString());
                                                    }
                                                    else
                                                    {
                                                        sl.SetCellValue(l + 10, j + 3, Convert.ToInt32(dataTable.Rows[l][j].ToString()));
                                                    }
                                                }
                                                else
                                                {
                                                    if (j >= 6 && j <= 12)
                                                    {
                                                        sl.SetCellValue(l + 10, j + 3, dataTable.Rows[l][j].ToString());
                                                    }
                                                }
                                                if (j > 12)
                                                {
                                                    if (j == 13)
                                                    {
                                                        sl.SetCellValue(l + 10, j + 3, dataTable.Rows[l][j].ToString());
                                                    }
                                                    else
                                                    {
                                                        if (j == 14)
                                                        {
                                                            sl.SetCellValue(l + 10, j + 3, dataTable.Rows[l][j].ToString());
                                                        }
                                                        else
                                                        {
                                                            if (j == 15)
                                                            {
                                                                sl.SetCellValue(l + 10, j + 3, Convert.ToDecimal(dataTable.Rows[l][j].ToString()));
                                                                totalSumCost += double.Parse(dataTable.Rows[l][j].ToString());
                                                            }
                                                            else
                                                            {
                                                                if (j == 16)
                                                                {
                                                                    sl.SetCellValue(l + 10, j + 3, Convert.ToDecimal(dataTable.Rows[l][j].ToString()));
                                                                    totalSumTor += double.Parse(dataTable.Rows[l][j].ToString());
                                                                }
                                                                else
                                                                {
                                                                    if (j == 17)
                                                                        sl.SetCellValue(l + 10, j + 3, dataTable.Rows[l][j].ToString());
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            cellRowIndex++;

                                            //backgroundWorker.ReportProgress(iterator);
                                        }

                                        for (int i = 0; i < dataTable.Rows.Count; i++)
                                        {
                                            for (int j = 1; j < dataTable.Columns.Count + 3; j++)
                                            {
                                                sl.SetCellStyle(i + 10, j, FormattingExcelCells(sl, true));
                                            }
                                        }

                                        sl.SetCellValue(dataTable.Rows.Count + 12, 2, "=C6");
                                        sl.SetCellStyle(dataTable.Rows.Count + 12, 2, FormattingExcelCells(sl, false));

                                        sl.SetCellValue(dataTable.Rows.Count + 14, 2, "Всего обработано вагонов - цистерн " + dt.Rows[k][1].ToString() + " по видам операций:");
                                        sl.SetCellStyle(dataTable.Rows.Count + 14, 2, FormattingExcelCells(sl, false));

                                        //Итоговая сводка
                                        int rowcount = 0;
                                        int total = 0;
                                        for (int i = 0; i < Itog_Rep.Rows.Count; i++)
                                        {
                                            rowcount++;
                                            for (int j = 0; j < Itog_Rep.Columns.Count; j++)
                                            {
                                                if (j == 0)
                                                {
                                                    sl.SetCellValue(i + cellRowIndex + 15 + rowcount, j + 2, Itog_Rep.Rows[i][j].ToString());
                                                    sl.SetCellStyle(i + cellRowIndex + 15 + rowcount, j + 2, FormattingExcelCells(sl, false));
                                                }
                                                else
                                                {
                                                    sl.SetCellValue(i + cellRowIndex + 15 + rowcount, j + 12, Convert.ToDecimal(Itog_Rep.Rows[i][j].ToString()));
                                                    sl.SetCellStyle(i + cellRowIndex + 15 + rowcount, j + 12, FormattingExcelCells(sl, false));
                                                }
                                            }

                                            if (i < Itog_Rep.Rows.Count - 1)
                                            {
                                                total += int.Parse(Itog_Rep.Rows[i][1].ToString());
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }

                                        sl.SetCellValue(dataTable.Rows.Count + 14, 13, total);
                                        sl.SetCellStyle(dataTable.Rows.Count + 14, 13, FormattingExcelCells(sl, false));
                                    

                                        sl.SetCellValue(dataTable.Rows.Count + 14, 13, cellRowIndex);

                                        //Итоговая сумма
                                        sl.SetCellValue(dataTable.Rows.Count + Itog_Rep.Rows.Count * 2 + 16, 14, totalSumTor + totalSumCost);
                                        sl.SetCellStyle(dataTable.Rows.Count + Itog_Rep.Rows.Count * 2 + 16, 14, FormattingExcelCells(sl, false));

                                        k++;
                                    }
                                }

                                sl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\Общий Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx");
                            }
                            Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\Общий Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx");
                        }
                        else
                        {
                            if(checkBox4.Checked)
                            {
                                Refresh();

                                string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\СНО Реализ.xlsx";
                                //string fileName = ((DataParametr)e.Argument).FileName;

                                using (SLDocument sl = new SLDocument(path))
                                {
                                    sl.SelectWorksheet("СНО Реализация");
                                    
                                    sl.SetCellValue("B3", "в ТОО Казыгурт-Юг реализация СНО за период с " + dateTimePicker1.Value.Date.ToShortDateString() + " по " + dateTimePicker2.Value.Date.ToShortDateString());

                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        for (int j = 2; j < dt.Columns.Count; j++)
                                        {
                                            if (j >= 2 && j <= 3)
                                            {
                                                sl.SetCellValue(i + 6, j - 1, dt.Rows[i][j].ToString());
                                                sl.SetCellStyle(i + 6, j - 1, FormattingExcelCells(sl, true));
                                            }
                                            else
                                            {
                                                if (j > 3 && j <= 6)
                                                {
                                                    sl.SetCellValue(i + 6, j - 1, Convert.ToDecimal(dt.Rows[i][j].ToString()));
                                                    sl.SetCellStyle(i + 6, j - 1, FormattingExcelCells(sl, true));
                                                    //worksheet.Range[$"C{i+6}:E{i+6}"].NumberFormat = "General";
                                                }
                                                else
                                                if (j == 8)
                                                {
                                                    sl.SetCellValue(i + 6, j - 2, Convert.ToDecimal(dt.Rows[i][j].ToString()));
                                                    sl.SetCellStyle(i + 6, j - 2, FormattingExcelCells(sl, true));
                                                }
                                                else
                                                if (j == 9)
                                                {
                                                    sl.SetCellValue(i + 6, j - 2, dt.Rows[i][j].ToString());
                                                    sl.SetCellStyle(i + 6, j - 2, FormattingExcelCells(sl, true));
                                                }
                                            }
                                        }
                                        
                                        //backgroundWorker1.ReportProgress(i);
                                    }

                                    for (int j = 0; j < dt.Columns.Count-2; j++)
                                    {
                                        sl.SetCellStyle(dt.Rows.Count + 6, j, FormattingExcelCells(sl, true));
                                    }

                                    sl.SetCellValue(dt.Rows.Count + 6, 1, "Итог");

                                    sl.SetCellValue(dt.Rows.Count + 6, 3, $"=SUM(C{6}:C{dt.Rows.Count + 5})");
                                    
                                    sl.SetCellValue(dt.Rows.Count + 6, 5, $"=SUM(E{6}:E{dt.Rows.Count + 5})");
                                    
                                    sl.SetCellValue(dt.Rows.Count + 6, 6, $"=SUM(F{6}:F{dt.Rows.Count + 5})");
                                    
                                    sl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\СНО Реализация.xlsx");
                                }
                                Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\СНО Реализация.xlsx");
                            }
                            else
                            {
                                if (checkBox6.Checked)
                                {
                                    Refresh();

                                    string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\СНО Приход.xlsx";

                                    using (SLDocument sl = new SLDocument(path))
                                    {
                                        sl.SelectWorksheet("СНО Приход");
                                       
                                        sl.SetCellValue("B1", "Приход СНО в ТОО Казыгурт-Юг за период с " + dateTimePicker1.Value.Date.ToShortDateString() + " по " + dateTimePicker2.Value.Date.ToShortDateString());

                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            for (int j = 1; j < dt.Columns.Count; j++)
                                            {
                                                if (j == 1)
                                                {
                                                    sl.SetCellValue(i + 4, j, Convert.ToDecimal(dt.Rows[i][j].ToString()));
                                                    sl.SetCellStyle(i + 4, j, FormattingExcelCells(sl, true));
                                                }
                                                else
                                                {
                                                    sl.SetCellStyle(i + 4, j + 1, FormattingExcelCells(sl, true));
                                                    if (j > 1 && j < 4)
                                                    {
                                                        sl.SetCellValue(i + 4, j + 1, Convert.ToDecimal(dt.Rows[i][j].ToString()));
                                                    }
                                                    else
                                                    {
                                                        sl.SetCellValue(i + 4, j + 1, dt.Rows[i][j].ToString());
                                                    }
                                                }
                                            }
                                            sl.SetCellValue(i + 4, 2, $"=C{i + 4} + D{i + 4}");
                                            sl.SetCellStyle(i + 4, 2, FormattingExcelCells(sl, true));
                                            //backgroundWorker1.ReportProgress(i);
                                        }

                                        for(int j = 0; j < dt.Columns.Count; j++)
                                        {
                                            sl.SetCellStyle(dt.Rows.Count + 4, j+1, FormattingExcelCells(sl, true));
                                        }

                                        sl.SetCellValue(dt.Rows.Count + 4, 1, "Итог");

                                        sl.SetCellValue(dt.Rows.Count + 4, 2, $"=SUM(B{4}:C{dt.Rows.Count + 3})");
                                        
                                        sl.SetCellValue(dt.Rows.Count + 4, 3, $"=SUM(C{4}:C{dt.Rows.Count + 3})");
                                        
                                        sl.SetCellValue(dt.Rows.Count + 4, 4, $"=SUM(D{4}:D{dt.Rows.Count + 3})");
                                        
                                        sl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\СНО Приход.xlsx");
                                    }
                                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\СНО Приход.xlsx");
                                }
                                else
                                {
                                    Refresh();

                                    string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx";

                                    using (SLDocument sl = new SLDocument(path))
                                    {

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
                                        sl.CopyCell("B13", "K20", "B" + val, true);

                                        for (int i = 0; i < dt.Rows.Count; i++)
                                        {
                                            sl.SetCellValue(i + 10, 1, i + 1);

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
                                                sl.SetCellStyle(i + 10, j, FormattingExcelCells(sl, true));
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
                                        int total = 0;

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

                                            if (i < getserv.Rows.Count - 1)
                                            {
                                                total += int.Parse(getserv.Rows[i][1].ToString());
                                            }
                                            else
                                            {
                                                continue;
                                            }
                                        }

                                        sl.SetCellValue(dt.Rows.Count + 14, 13, total);
                                        sl.SetCellStyle(dt.Rows.Count + 14, 13, FormattingExcelCells(sl, false));

                                        //Итоговая сумма
                                        sl.SetCellValue(dt.Rows.Count + getserv.Rows.Count * 2 + 16, 14, totalSumTor + totalSumCost);
                                        sl.SetCellStyle(dt.Rows.Count + getserv.Rows.Count * 2 + 16, 14, FormattingExcelCells(sl, false));

                                        sl.SaveAs(AppDomain.CurrentDomain.BaseDirectory + @"Report\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx");
                                    }
                                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Report\Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        struct DataParametr
        {
            //    public string owner { get; set; }
            //    public int sz { get; set; }
        }

        //DataParametr _inputParametr1;


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

                return style2;
            }
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox2.Checked = false;
                checkBox5.Checked = false;
                checkBox4.Checked = false;
                checkBox6.Checked = false;
                //Refresh();
            }
            else
            {
                checkBox1.Checked = false;
                //Refresh();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Refresh();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Checked)
            {
                //Refresh();
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

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                comboBox2.Enabled = false;
                checkBox5.Checked = false;
                comboBox2.SelectedIndex = 0;
                checkBox4.Checked = false;
                checkBox6.Checked = false;
                //Refresh();
            }
            else
            {
                comboBox2.Enabled = true;
                //Refresh();
                //ReportForm_Load(null, null);
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

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                comboBox2.SelectedIndex = 0;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                comboBox2.Enabled = false;
                checkBox4.Checked = false;
                checkBox6.Checked = false;
                //Refresh();
            }
            else
            {
                checkBox5.Checked = false;
                comboBox2.Enabled = true;
                //Refresh();
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                comboBox2.SelectedIndex = 0;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                comboBox2.Enabled = false;
                checkBox6.Checked = false;
                checkBox5.Checked = false;
            }
            else
            {
                checkBox4.Checked = false;
                comboBox2.Enabled = true;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox6.Checked)
            {
                comboBox2.SelectedIndex = 0;
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                comboBox2.Enabled = false;
                checkBox5.Checked = false;
                checkBox4.Checked = false;
            }
            else
            {
                checkBox5.Checked = false;
                comboBox2.Enabled = true;
            }
        }
    }
}