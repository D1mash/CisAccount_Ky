using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {

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
