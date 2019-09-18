using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Учет_цистерн
{
    public partial class ReportForm : Form
    {
        public ReportForm()
        {
            InitializeComponent();
        }

        BindingSource source = new BindingSource();

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            dateTimePicker1.Value = startDate;
            dateTimePicker2.Value = endDate;

            string Refresh = "dbo.GetReportRenderedServices '" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() +"','" + comboBox2.SelectedValue + "'";
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Refresh);
            source.DataSource = dataTable;
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].Visible = false;
        
        }

        private void ReportForm_Load(object sender, EventArgs e)
        {
            //String Station = "Select * from d__Station";
            //DataTable StationDT = DbConnection.DBConnect(Station);
            //comboBox1.DataSource = StationDT;
            //comboBox1.DisplayMember = "Name";
            //comboBox1.ValueMember = "ID";

            String Owner = "Select * from d__Owner";
            DataTable OwnerDT = DbConnection.DBConnect(Owner);
            comboBox2.DataSource = OwnerDT;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";
        }
    }
}
