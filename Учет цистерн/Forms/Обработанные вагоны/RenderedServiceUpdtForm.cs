using System;
using System.Data;
using System.Windows.Forms;
using Учет_цистерн.Forms.Оповещения;

namespace Учет_цистерн
{
    public partial class RenderedServiceUpdtForm : Form
    {
        public RenderedServiceUpdtForm()
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

        public int SelectBrigadeID { get; set; }
        public int SelectCarriageID { get; set; }
        public int SelectServiceID { get; set; }
        public int SelectProductID { get; set; }
        public int SelectStationID { get; set; }


        private void FillCombobox()
        {
            String Carriage = "Select * from d__Carriage";
            DataTable CarDT = DbConnection.DBConnect(Carriage);
            comboBox1.DataSource = CarDT;
            comboBox1.DisplayMember = "CarNumber";
            comboBox1.ValueMember = "ID";
            comboBox1.DataBindings.Add("SelectedValue", this, "SelectCarriageID", true, DataSourceUpdateMode.OnPropertyChanged);

            String Owner = "Select * from d__Owner";
            DataTable OwnerDT = DbConnection.DBConnect(Owner);
            comboBox2.DataSource = OwnerDT;
            comboBox2.DisplayMember = "Name";
            comboBox2.ValueMember = "ID";
            //comboBox2.DataBindings.Add("SelectedValue", this, "SelectOwnerID", true, DataSourceUpdateMode.OnPropertyChanged);

            String Brigade = "Select * from d__Brigade";
            DataTable BrigadeDT = DbConnection.DBConnect(Brigade);
            comboBox3.DataSource = BrigadeDT;
            comboBox3.DisplayMember = "FIO";
            comboBox3.ValueMember = "ID";
            comboBox3.DataBindings.Add("SelectedValue", this, "SelectBrigadeID", true, DataSourceUpdateMode.OnPropertyChanged);

            String Product = "Select * from d__Product";
            DataTable ProductDT = DbConnection.DBConnect(Product);
            comboBox4.DataSource = ProductDT;
            comboBox4.DisplayMember = "Name";
            comboBox4.ValueMember = "ID";
            comboBox4.DataBindings.Add("SelectedValue", this, "SelectProductID", true, DataSourceUpdateMode.OnPropertyChanged);

            String Service = "Select * from d__ServiceCost";
            DataTable ServiceDT = DbConnection.DBConnect(Service);
            comboBox5.DataSource = ServiceDT;
            comboBox5.DisplayMember = "ServiceName";
            comboBox5.ValueMember = "ID";
            comboBox5.DataBindings.Add("SelectedValue", this, "SelectServiceID", true, DataSourceUpdateMode.OnPropertyChanged);

            String Station = "Select * from d__Station";
            DataTable StationDT = DbConnection.DBConnect(Station);
            comboBox6.DataSource = StationDT;
            comboBox6.DisplayMember = "Name";
            comboBox6.ValueMember = "ID";
            comboBox6.DataBindings.Add("SelectedValue", this, "SelectStationID", true, DataSourceUpdateMode.OnPropertyChanged);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Update = "exec dbo.UpdateRenderedService " + selectID + ",'" + dateTimePicker1.Value.Date.ToString() + "'," + comboBox3.SelectedValue.ToString() + "," + comboBox1.SelectedValue.ToString() + "," + textBox2.Text.Trim() + ",'" + textBox1.Text.Trim() + "'," + comboBox6.SelectedValue.ToString() + "," + comboBox4.SelectedValue.ToString() + "," + comboBox5.SelectedValue.ToString();
            DataTable dataTable = new DataTable();
            dataTable = DbConnection.DBConnect(Update);
            this.Close();
            OkForm ok = new OkForm();
            ok.label1.Text = "Запись изменена!";
            ok.Show();
            //MessageBox.Show("Запись изменена!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RenderedServiceUpdtForm_Load(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            comboBox3.Enabled = false;
            comboBox4.Enabled = false;
            comboBox5.Enabled = false;
            comboBox6.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            dateTimePicker1.Enabled = false;
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = (checkBox1.CheckState == CheckState.Checked);
        }

        private void checkBox2_CheckStateChanged(object sender, EventArgs e)
        {
            comboBox6.Enabled = (checkBox2.CheckState == CheckState.Checked);
        }

        private void checkBox3_CheckStateChanged(object sender, EventArgs e)
        {
            comboBox2.Enabled = (checkBox3.CheckState == CheckState.Checked);
        }

        private void checkBox4_CheckStateChanged(object sender, EventArgs e)
        {
            textBox1.Enabled = (checkBox4.CheckState == CheckState.Checked);
        }

        private void checkBox5_CheckStateChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = (checkBox5.CheckState == CheckState.Checked);
        }

        private void checkBox6_CheckStateChanged(object sender, EventArgs e)
        {
            comboBox3.Enabled = (checkBox6.CheckState == CheckState.Checked);
        }

        private void checkBox7_CheckStateChanged(object sender, EventArgs e)
        {
            comboBox4.Enabled = (checkBox7.CheckState == CheckState.Checked);
        }

        private void checkBox8_CheckStateChanged(object sender, EventArgs e)
        {
            comboBox5.Enabled = (checkBox8.CheckState == CheckState.Checked);
        }

        private void checkBox9_CheckStateChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = (checkBox9.CheckState == CheckState.Checked);
        }
    }
}
