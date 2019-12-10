using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
    public partial class AUTNReportForm : Form
    {
        BindingSource source = new BindingSource();
        DataTable getserv;

        public AUTNReportForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string RefreshAll = "exec [dbo].[GetReportAUTN]";
            DataTable dt;
            dt = DbConnection.DBConnect(RefreshAll);
            source.DataSource = dt;
            dataGridView1.DataSource = source;
            progressBar.Maximum = TotalRow(dt);
            toolStripLabel1.Text = TotalRow(dt).ToString();
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

        private void btn_Excel_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows != null && dataGridView1.Rows.Count != 0)
            {
                if (backgroundWorker.IsBusy)
                    return;
                else
                {
                    progressBar.Minimum = 0;
                    progressBar.Value = 0;
                    backgroundWorker.RunWorkerAsync();
                }
            }
            else
            {
                MessageBox.Show("Обновите данные!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        [DllImport("user32.dll")]
        static extern int GetWindowThreadProcessId(int hWnd, out int lpdwProcessId);

        static Process GetExcelProcess(Excel.Application excelApp)
        {
            GetWindowThreadProcessId(excelApp.Hwnd, out int id);
            return Process.GetProcessById(id);
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\Реестр АУТН.xlsx";
                Excel.Application app = new Excel.Application();
                Process appProcess = GetExcelProcess(app);
                Excel.Workbook workbook = app.Workbooks.Open(path);
                Excel.Worksheet worksheet = workbook.Worksheets.get_Item("АУТН");
                app.Visible = false;
                object misValue = System.Reflection.Missing.Value;

                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 3, 1] = i;
                        if (j < 8)
                        {
                            worksheet.Cells[i + 3, j + 2] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            if (j > 9 && j < 18)
                            {
                                if (dataGridView1.Rows[i].Cells[j].Value.ToString() == "1")
                                {
                                    worksheet.Cells[i + 3, j + 2] = "Да";
                                }
                                else
                                {
                                    worksheet.Cells[i + 3, j + 2] = "Нет";
                                }
                            }
                            else
                            {
                                worksheet.Cells[i + 3, j + 2] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                        }

                    }
                    backgroundWorker.ReportProgress(i);
                }

                app.DisplayAlerts = false;
                workbook.SaveAs(/*fileName*/@"D:\Отчеты\Реестр АУТН.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                workbook.Close(true, misValue, misValue);
                app.Quit();
                appProcess.Kill();

                Process.Start(@"D:\Отчеты\Реестр АУТН.xls");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
            LblStatus.Text = "Обработка строки.. " + e.ProgressPercentage.ToString() /*+ " из " + TotalRow()*/;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Thread.Sleep(1);
                LblStatus.Text = "Данные были успешно экспортированы";
                progressBar.Value = 0;
            }
        }
    }
}