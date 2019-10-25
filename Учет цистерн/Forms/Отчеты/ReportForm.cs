using System;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


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
            if (comboBox2.SelectedIndex == 0)
            {
                string RefreshAll = "exec dbo.GetReportAllRenderedService_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'";
                DataTable dt;
                dt = DbConnection.DBConnect(RefreshAll);
                source.DataSource = dt;
                dataGridView1.DataSource = source;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[14].Visible = false;
                progressBar.Maximum = TotalRow(dt);
                toolStripLabel1.Text = TotalRow(dt).ToString();
            }
            else
            {
                string Refresh = "dbo.GetReportRenderedServices_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
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

            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            dateTimePicker1.Value = startDate;
            dateTimePicker2.Value = endDate;
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
                MessageBox.Show("Обновите данные!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        struct DataParametr
        {
            public string FileName { get; set; }
        }

        DataParametr _inputParametr;

        private void BackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            try
            {

                string path = "D:/Project2/CisAccount/Учет цистерн/Forms/ReportTemplates/Реестр  за арендованных и  собственных вагон-цистерн компании.xlsx";
                string fileName = ((DataParametr)e.Argument).FileName;

                Excel.Application app = new Excel.Application();
                Excel.Workbook workbook = app.Workbooks.Open(path);
                Excel.Worksheet worksheet = workbook.Worksheets.get_Item("Test");

                app.Visible = false;

                int cellRowIndex = 0;
                int totalTOR4 = 0;
                int totalNaliv4sv = 0;
                int totalNaliv4tem = 0;
                //int cellColumnIndex = 1;

                worksheet.Range["B12:K34"].Cut(worksheet.Cells[dataGridView1.Rows.Count + 12, 2]);
                //Excel.Range cellRange = (Excel.Range)worksheet.Cells[dataGridView1.Rows.Count + 12, 2];
                //cellRange.set_Value(Missing.Value, worksheet.get_Range("B14:C34"));
                //Excel.Range rowRange = cellRange.EntireRow;
                //rowRange.Insert(Excel.XlInsertShiftDirection.xlShiftDown);

                worksheet.Range["C6"].Value = "в ТОО Казыгурт-Юг c " + dateTimePicker1.Value.ToShortDateString() + " по " + dateTimePicker2.Value.ToShortDateString();

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    worksheet.Cells[i + 10, 1] = i + 1;
                    for (int j = 1; j < dataGridView1.Columns.Count-1; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check.
                        //if (cellRowIndex == 1)
                        //{
                        //    worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Columns[j].HeaderText;
                        //}
                        if(j!=3 && j<4)
                        {
                            worksheet.Cells[i + 10, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            if(j == 3)
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
                        if(j>=4 && j<=5)
                        {
                            worksheet.Cells[i + 10, j + 3] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            
                            if (j == 4 && dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "св/св" && dataGridView1.Rows[i].Cells[6].Value.ToString().Trim() == "True")
                            {
                                totalNaliv4sv++;
                            }
                            else if(j == 5 && dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "тем/тем" && dataGridView1.Rows[i].Cells[6].Value.ToString().Trim() == "True")
                            {
                                totalNaliv4tem++;
                            }
                        }
                        else
                        {
                            if(j>=6 && j <= 9)
                            {
                                if(dataGridView1.Rows[i].Cells[j].Value.ToString().Trim() == "True")
                                {
                                    worksheet.Cells[i + 10, j + 3] = "✓";
                                    if (j == 8 && dataGridView1.Rows[i].Cells[3].Value.ToString().Trim() == "4")
                                    {
                                        totalTOR4++;
                                    }
                                }
                                else
                                {
                                    worksheet.Cells[i + 10, j + 3] = " ";
                                }
                            }
                        }
                        if(j>9)
                        {
                            worksheet.Cells[i + 10, j + 3] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        //cellColumnIndex++;
                    }

                    Excel.Range priceRange = worksheet.Range[worksheet.Cells[i + 10, 15], worksheet.Cells[dataGridView1.Rows.Count + 9, 15]];
                    priceRange.NumberFormat = "0.00";

                    Excel.Range range = worksheet.Range[worksheet.Cells[i + 10, 1], worksheet.Cells[i + 10, dataGridView1.Columns.Count + 1]];
                    //range.EntireColumn.AutoFit();
                    range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                    Excel.Borders border = range.Borders;
                    border.LineStyle = Excel.XlLineStyle.xlContinuous;
                    border.Weight = 2d;

                    backgroundWorker.ReportProgress(i);
                    //cellColumnIndex = 1;
                    cellRowIndex++;
                }

                    worksheet.Cells[cellRowIndex + 14, 13] = cellRowIndex;

                    worksheet.Cells[cellRowIndex + 16, 13] = totalNaliv4sv;
                
                    worksheet.Cells[cellRowIndex + 18, 13] = totalNaliv4tem;

                    worksheet.Cells[cellRowIndex + 26, 13] = totalTOR4;

                    workbook.SaveAs(fileName);
                    app.Quit();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
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
    }
}
