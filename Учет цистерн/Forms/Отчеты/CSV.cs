using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн.Forms.Отчеты
{
    public partial class CSV : Form
    {
        public CSV()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string Refresh = "dbo.GetReportRenderedServices_v1 '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "','" + comboBox2.SelectedValue + "'";
            DataTable dataTable = DbConnection.DBConnect(Refresh);
            
            string strFilePath = AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\CSV_Test.csv"; ;




            //System.IO.StreamWriter sw = new System.IO.StreamWriter(strFilePath, false);

            ////First we will write the headers.

            //int iColCount = dataTable.Columns.Count;

            //for (int i = 0; i < iColCount; i++)
            //{
            //    sw.Write(dataTable.Columns[i]);
            //    if (i < iColCount - 1)
            //    {
            //        sw.Write(",");
            //    }
            //}
            //sw.Write(sw.NewLine);

            //// Now write all the rows.

            //foreach (DataRow dr in dataTable.Rows)
            //{
            //    for (int i = 0; i < iColCount; i++)
            //    {
            //        if (!Convert.IsDBNull(dr[i]))
            //        {
            //            sw.Write(dr[i].ToString());
            //        }
            //        if (i < iColCount - 1)

            //        {
            //            sw.Write(",");
            //        }
            //    }
            //    sw.Write(sw.NewLine);
            //}
            //sw.Close();

            //Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"ReportTemplates\CSV_Test.csv");
        }

        private void CSV_Load(object sender, EventArgs e)
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
        }
    }
}
