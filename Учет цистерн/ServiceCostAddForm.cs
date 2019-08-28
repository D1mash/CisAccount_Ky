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
    public partial class ServiceCostAddForm : Form
    {
        public ServiceCostAddForm()
        {
            InitializeComponent();
            FillCombobox();
        }

        private void FillCombobox()
        {
            string Service = "Select * from d__Service";
            string Season = "select * from d__Season";
            DataTable dT = DbConnection.DBConnect(Service);
            DataTable dTs = DbConnection.DBConnect(Season);
            comboBox1.DataSource = dT;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "ID";

            comboBox2.DataSource = dTs;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string FillServiceCost = "exec dbo.FillServiceCost " + comboBox1.SelectedValue.ToString() + ",'" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'," + textBox1.Text.Replace(",", ".") + "," + comboBox2.SelectedValue.ToString();
            DbConnection.DBConnect(FillServiceCost);
            this.Close();
            MessageBox.Show("Запись добавлена!");
        }
    }
}
