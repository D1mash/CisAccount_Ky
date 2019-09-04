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
    public partial class ServiceCostUpdtForm : Form
    {
        public ServiceCostUpdtForm()
        {
            InitializeComponent();
            FillCombobox();
        }

        int selectID;

        public int SelectID
        {
            get { return SelectID; }
            set { selectID = value; }
        }

        public int SelectServiceID { get; set; }
        public int SelectSeasonID { get; set; }

        private void ServiceCostUpdtForm_Load(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;
            textBox1.Enabled = false;
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = (checkBox1.CheckState == CheckState.Checked);
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = (checkBox2.CheckState == CheckState.Checked);
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            dateTimePicker2.Enabled = (checkBox3.CheckState == CheckState.Checked);
        }

        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = (checkBox4.CheckState == CheckState.Checked);
        }

        private void checkBox5_CheckStateChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = (checkBox5.CheckState == CheckState.Checked);
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
            comboBox1.DataBindings.Add("SelectedValue", this, "SelectServiceID", true, DataSourceUpdateMode.OnPropertyChanged);

            comboBox2.DataSource = dTs;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";
            comboBox2.DataBindings.Add("SelectedValue", this, "SelectSeasonID", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Update = "exec dbo.UpdateServiceCost " + comboBox1.SelectedValue.ToString() + ",'" + dateTimePicker1.Value.Date.ToString() + "','" + dateTimePicker2.Value.Date.ToString() + "'," + textBox1.Text.Replace(",", ".") + "," + comboBox2.SelectedValue.ToString()+","+selectID;
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Update);
            this.Close();
            MessageBox.Show("Запись изменена!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
