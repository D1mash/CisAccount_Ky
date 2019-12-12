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
        BindingSource source = new BindingSource();
        DataTable getserv;
        string UserFIO;

        public ReportForm(string userFIO)
        {
            InitializeComponent();
            this.UserFIO = userFIO;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                if (checkBox1.Checked)
                {
                    dataGridView1.DataSource = null;
                    string Itog_All_Report = "exec dbo.Itog_All_Report '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                    DataTable dt;
                    dt = DbConnection.DBConnect(Itog_All_Report);
                    source.DataSource = dt;
                    dataGridView1.DataSource = source;
                    progressBar.Maximum = TotalRow(dt);
                    toolStripLabel1.Text = TotalRow(dt).ToString();
                }
                else
                {
                    dataGridView1.DataSource = null;
                    string RefreshAll = "exec dbo.GetReportAllRenderedService_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                    DataTable dt;
                    dt = DbConnection.DBConnect(RefreshAll);
                    source.DataSource = dt;
                    dataGridView1.DataSource = source;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[14].Visible = false;
                    dataGridView1.Columns[15].Visible = false;
                    progressBar.Maximum = TotalRow(dt);
                    toolStripLabel1.Text = TotalRow(dt).ToString();

                    string GetCountServiceCost = "exec dbo.Itog_All_Report '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                    getserv = DbConnection.DBConnect(GetCountServiceCost);
                }
                //SUM_Line(true);
            }
            else
            {
                if (checkBox1.Checked)
                {
                    dataGridView1.DataSource = null;
                    string Itog_All_Report = "exec dbo.Itog_Report '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
                    DataTable dt;
                    dt = DbConnection.DBConnect(Itog_All_Report);
                    source.DataSource = dt;
                    dataGridView1.DataSource = source;
                    progressBar.Maximum = TotalRow(dt);
                    toolStripLabel1.Text = TotalRow(dt).ToString();
                }
                else 
                {
                    dataGridView1.DataSource = null;
                    string Refresh = "dbo.GetReportRenderedServices_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
                    DataTable dataTable;
                    dataTable = DbConnection.DBConnect(Refresh);
                    source.DataSource = dataTable;
                    dataGridView1.DataSource = source;
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[14].Visible = false;
                    dataGridView1.Columns[15].Visible = false;
                    progressBar.Maximum = TotalRow(dataTable);
                    toolStripLabel1.Text = TotalRow(dataTable).ToString();

                    string GetCountServiceCost = "exec dbo.Itog_Report  '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
                    getserv = DbConnection.DBConnect(GetCountServiceCost);
                }
                //SUM_Line(true);
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

            SUM_Line(false);
        }

        private void SUM_Line(bool v)
        {
            panel1.Visible = v;
            panel2.Visible = v;
            panel3.Visible = v;
            panel4.Visible = v;
            panel5.Visible = v;
            panel6.Visible = v;
            panel7.Visible = v;
            panel8.Visible = v;
            panel9.Visible = v;
            panel10.Visible = v;
            panel11.Visible = v;
            panel12.Visible = v;
            textBox1.Visible = v;
            textBox2.Visible = v;
        }

        private void Btn_Excel_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows != null && dataGridView1.Rows.Count != 0)
            {
                if (backgroundWorker.IsBusy)
                    return;
                //using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Excel file (*.xlsx)|*.xlsx|All files(*.*)|*.*" })
                //{
                //    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                //    {
                //        //_inputParametr1.FileName = saveFileDialog.FileName;
                else
                {
                    _inputParametr1.owner = comboBox2.Text;
                    progressBar.Minimum = 0;
                    progressBar.Value = 0;
                    backgroundWorker.RunWorkerAsync(_inputParametr1);
                }
                //    }
                //}
            }
            else
            {
                MessageBox.Show("Обновите данные!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        struct DataParametr
        {
            //public string FileName { get; set; }
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

                    for(int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for(int j = 0; j < dataGridView1.Columns.Count; j++)
                        {
                            worksheet.Cells[i+11, j+2] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            backgroundWorker.ReportProgress(i);
                        }
                    }

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
                    //var folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                    //string fileName = ((DataParametr)e.Argument).FileName;
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

                    worksheet.Range["B15:K23"].Cut(worksheet.Cells[dataGridView1.Rows.Count + 16 + getserv.Rows.Count * 2, 2]);

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        worksheet.Cells[i + 10, 1] = i + 1;
                        for (int j = 1; j < dataGridView1.Columns.Count - 2; j++)
                        {
                            // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check.
                            //if (cellRowIndex == 1)
                            //{
                            //    worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Columns[j].HeaderText;
                            //}
                            if (j != 3 && j < 4)
                            {
                                worksheet.Cells[i + 10, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else
                            {
                                if (j == 3)
                                {
                                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "8")
                                    {
                                        worksheet.Cells[i + 10, 5] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                    }
                                    else
                                    {
                                        worksheet.Cells[i + 10, 4] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                                    }
                                }
                            }
                            if (j >= 4 && j <= 5)
                            {
                                worksheet.Cells[i + 10, j + 3] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            else
                            {
                                if (j >= 6 && j <= 9)
                                {
                                    if (dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "True")
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
                                worksheet.Cells[i + 10, j + 3] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                        }

                        //Excel.Range priceRange = worksheet.Range[worksheet.Cells[i + 10, 15], worksheet.Cells[dataGridView1.Rows.Count + 9, 15]];
                        //priceRange.NumberFormat = "0.00";

                        Excel.Range range = worksheet.Range[worksheet.Cells[i + 10, 1], worksheet.Cells[i + 10, dataGridView1.Columns.Count]];
                        FormattingExcelCells(range, true, true);

                        backgroundWorker.ReportProgress(i);

                        cellRowIndex++;
                    }

                    worksheet.Cells[dataGridView1.Rows.Count + 12, 2] = "=C6";

                    if (ownerName == "Все")
                    {
                        worksheet.Cells[dataGridView1.Rows.Count + 14, 2] = "Всего обработано вагонов - цистерн всех собственников по видам операций:";
                    }
                    else
                    {
                        worksheet.Cells[dataGridView1.Rows.Count + 14, 2] = "Всего обработано вагонов - цистерн " + ownerName + " по видам операций:";
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

                    worksheet.Cells[dataGridView1.Rows.Count + 14, 13] = cellRowIndex;

                    worksheet.Cells[dataGridView1.Rows.Count + getserv.Rows.Count * 2 + 16, 13] = totalTOR4;

                    worksheet.Cells[dataGridView1.Rows.Count + getserv.Rows.Count * 2 + 18, 14] = TotalSum();

                    Excel.Range range1 = worksheet.Range[worksheet.Cells[dataGridView1.Rows.Count + 12, 2], worksheet.Cells[dataGridView1.Rows.Count + getserv.Rows.Count * 2 + 19, 14]];
                    FormattingExcelCells(range1, false, false);

                    app.DisplayAlerts = false;
                    workbook.SaveAs(@"D:\Отчеты\Реестр  за арендованных и  собственных вагон-цистерн компании.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    workbook.Close(true, misValue, misValue);
                    app.Quit();
                    appProcess.Kill();

                    Process.Start(@"D:\Отчеты\Реестр  за арендованных и  собственных вагон-цистерн компании.xls");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        //ИТОГО СУММА
        private Double TotalSum()
        {
            Double sum = 0;
            
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                    sum += Convert.ToDouble(dataGridView1.Rows[i].Cells[12].Value);
            }
            return sum;
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

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //int sz = 0;

            //try
            //{
            //    Decimal sum = 0;
            //    int Count = 0;
            //    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            //    {
            //        if (dataGridView1.Rows[i].Cells[12].Value.ToString() != string.Empty)
            //        {
            //            sum += Convert.ToDecimal(this.dataGridView1[12, i].Value);
            //        }
            //        Count = dataGridView1.RowCount;
            //    }

            //    foreach (var scroll in dataGridView1.Controls.OfType<HScrollBar>())
            //    {
            //        if (scroll.Visible)
            //        {
            //            sz = -3;
            //        }
            //        else
            //        {
            //            sz = 15;
            //        }
            //    }
                 
            //    panel1.Width = this.dataGridView1.RowHeadersWidth;
            //    panel1.Location = new Point(5, this.dataGridView1.Height - (panel1.Height - sz));
            //    panel1.Visible = true;

            //    int Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(0, -1, true).Location.X;
            //    panel2.Width = this.dataGridView1.Columns[1].Width + 1;
            //    Xdgvx1 = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;
            //    panel2.Location = new Point(Xdgvx1, this.dataGridView1.Height - (panel2.Height - sz));
            //    panel2.Visible = true;

            //    textBox1.Text = "Всего строк: " + Count.ToString();
            //    int Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(1, -1, true).Location.X;
            //    textBox1.Width = this.dataGridView1.Columns[2].Width + 1;
            //    Xdgvx2 = this.dataGridView1.GetCellDisplayRectangle(2, -1, true).Location.X;
            //    textBox1.Location = new Point(Xdgvx2, this.dataGridView1.Height - (textBox1.Height - sz));
            //    textBox1.Visible = true;

            //    int Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(2, -1, true).Location.X;
            //    panel3.Width = this.dataGridView1.Columns[3].Width + 1;
            //    Xdgvx3 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
            //    panel3.Location = new Point(Xdgvx3, this.dataGridView1.Height - (panel3.Height - sz));
            //    panel3.Visible = true;

            //    int Xdgvx4 = this.dataGridView1.GetCellDisplayRectangle(3, -1, true).Location.X;
            //    panel4.Width = this.dataGridView1.Columns[4].Width + 1;
            //    Xdgvx4 = this.dataGridView1.GetCellDisplayRectangle(4, -1, true).Location.X;
            //    panel4.Location = new Point(Xdgvx4, this.dataGridView1.Height - (panel4.Height - sz));
            //    panel4.Visible = true;

            //    int Xdgvx5 = this.dataGridView1.GetCellDisplayRectangle(4, -1, true).Location.X;
            //    panel5.Width = this.dataGridView1.Columns[5].Width + 1;
            //    Xdgvx5 = this.dataGridView1.GetCellDisplayRectangle(5, -1, true).Location.X;
            //    panel5.Location = new Point(Xdgvx5, this.dataGridView1.Height - (panel5.Height - sz));
            //    panel5.Visible = true;

            //    int Xdgvx6 = this.dataGridView1.GetCellDisplayRectangle(5, -1, true).Location.X;
            //    panel6.Width = this.dataGridView1.Columns[6].Width + 1;
            //    Xdgvx6 = this.dataGridView1.GetCellDisplayRectangle(6, -1, true).Location.X;
            //    panel6.Location = new Point(Xdgvx6, this.dataGridView1.Height - (panel6.Height - sz));
            //    panel6.Visible = true;

            //    int Xdgvx7 = this.dataGridView1.GetCellDisplayRectangle(6, -1, true).Location.X;
            //    panel7.Width = this.dataGridView1.Columns[7].Width + 1;
            //    Xdgvx7 = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
            //    panel7.Location = new Point(Xdgvx7, this.dataGridView1.Height - (panel7.Height - sz));
            //    panel7.Visible = true;

            //    int Xdgvx8 = this.dataGridView1.GetCellDisplayRectangle(7, -1, true).Location.X;
            //    panel8.Width = this.dataGridView1.Columns[8].Width + 1;
            //    Xdgvx8 = this.dataGridView1.GetCellDisplayRectangle(8, -1, true).Location.X;
            //    panel8.Location = new Point(Xdgvx8, this.dataGridView1.Height - (panel8.Height - sz));
            //    panel8.Visible = true;

            //    int Xdgvx9 = this.dataGridView1.GetCellDisplayRectangle(8, -1, true).Location.X;
            //    panel9.Width = this.dataGridView1.Columns[9].Width + 1;
            //    Xdgvx9 = this.dataGridView1.GetCellDisplayRectangle(9, -1, true).Location.X;
            //    panel9.Location = new Point(Xdgvx9, this.dataGridView1.Height - (panel9.Height - sz));
            //    panel9.Visible = true;

            //    int Xdgvx10 = this.dataGridView1.GetCellDisplayRectangle(9, -1, true).Location.X;
            //    panel10.Width = this.dataGridView1.Columns[10].Width + 1;
            //    Xdgvx10 = this.dataGridView1.GetCellDisplayRectangle(10, -1, true).Location.X;
            //    panel10.Location = new Point(Xdgvx10, this.dataGridView1.Height - (panel10.Height - sz));
            //    panel10.Visible = true;

            //    int Xdgvx11 = this.dataGridView1.GetCellDisplayRectangle(10, -1, true).Location.X;
            //    panel11.Width = this.dataGridView1.Columns[11].Width + 1;
            //    Xdgvx11 = this.dataGridView1.GetCellDisplayRectangle(11, -1, true).Location.X;
            //    panel11.Location = new Point(Xdgvx11, this.dataGridView1.Height - (panel11.Height - sz));
            //    panel11.Visible = true;

            //    textBox2.Text = "Сумма: " + sum.ToString();
            //    int Xdgvx12 = this.dataGridView1.GetCellDisplayRectangle(11, -1, true).Location.X;
            //    textBox2.Width = this.dataGridView1.Columns[12].Width + 1;
            //    Xdgvx12 = this.dataGridView1.GetCellDisplayRectangle(12, -1, true).Location.X;
            //    textBox2.Location = new Point(Xdgvx12, this.dataGridView1.Height - (textBox2.Height - sz));
            //    textBox2.Visible = true;

            //    int Xdgvx13 = this.dataGridView1.GetCellDisplayRectangle(12, -1, true).Location.X;
            //    panel12.Width = this.dataGridView1.Columns[13].Width + 1;
            //    Xdgvx13 = this.dataGridView1.GetCellDisplayRectangle(13, -1, true).Location.X;
            //    panel12.Location = new Point(Xdgvx13, this.dataGridView1.Height - (panel12.Height - sz));
            //    panel12.Visible = true;

            //}
            //catch (Exception exp)
            //{
            //    MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
