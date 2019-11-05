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

        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgrBar.Value = e.ProgressPercentage;
            label1.Text = "Обработка строки.. " + e.ProgressPercentage.ToString()
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
